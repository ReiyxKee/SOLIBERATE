using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class Linker : MonoBehaviour
{
    public Pieces_Infomation[] Pieces;

    public GameObject Target_1;

    public GameObject mTarget_1;
    public GameObject wTarget_1;

    public Camera ARCam;
    public bool OnTap;
    public Animator anim;
    public bool Paired;

    public bool started;

    public GameObject OuterCanvas;

    public GameObject Tutorial;
    public GameObject UI;
    public GameObject InGame;
    public GameObject EndGame;

    public bool Clear;
    public bool ShowResult;

    public GameObject SolutionParent;

    public bool wrongpair;
    public float wrongtimer;

    public TextMeshProUGUI _TimeSpend;
    public GameObject _TimeSpendPanel;
    public float TimeSpend;

    public TextMeshProUGUI _BestRecord;
    public GameObject _BestRecordPanel;
    public float BestRecord;

    public TextMeshProUGUI Result_Time;
    public TextMeshProUGUI Result_Best;
    public TextMeshProUGUI Result_NewBest;


    // Start is called before the first frame update
    void Start()
    {
        if (OuterCanvas == null && GameObject.Find("/Canvas"))
        {
            OuterCanvas = GameObject.Find("/Canvas");
        }

        _TimeSpendPanel.SetActive(false);
        Tutorial.SetActive(false);
        InGame.SetActive(false);

        if (PlayerPrefs.HasKey("Venus_Record"))
        {
            _BestRecordPanel.SetActive(true);
               BestRecord = PlayerPrefs.GetFloat("Venus_Record");
            _BestRecord.text = "BEST RECORD:\n" + BestRecord.ToString("0.00") + " s";
        }
        else
        {
            _BestRecordPanel.SetActive(false);
            _BestRecord.text = "";
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (ARCam == null && GameObject.Find("AR Camera"))
        {
            ARCam = GameObject.Find("AR Camera").GetComponent<Camera>();
        }

        if (started)
        {
            _TimeSpendPanel.SetActive(true);
            _TimeSpend.text = "Time: " + TimeSpend.ToString("0.00") + " s"; ;

            if (!Clear)
            {
                TimeSpend += Time.deltaTime;
            }

            wTarget_1.SetActive(wrongpair);

            if (wrongpair)
            {
                wrongtimer += Time.deltaTime;
            }

            if (wrongtimer >= 0.5f)
            {
                wrongpair = false;
                wrongtimer = 0;
            }

            if (Target_1 != null)
            {
                mTarget_1.SetActive(true);
                mTarget_1.GetComponent<RectTransform>().position = ARCam.WorldToScreenPoint(Target_1.transform.GetChild(0).transform.position);
                wTarget_1.GetComponent<RectTransform>().position = mTarget_1.GetComponent<RectTransform>().position;
            }
            else
            {
                mTarget_1.SetActive(false);
                wTarget_1.SetActive(wrongpair);
            }

            if (Input.touchCount == 0 && OnTap)
            {
                OnTap = false;
            }

            if (Input.touchCount == 1 && !OnTap)
            {
                OnTap = true;
                if (!IsPointerOverUIObject())
                {
                    Touch screenTouch = Input.GetTouch(0);
                    Ray ray = ARCam.ScreenPointToRay(screenTouch.position);
                    RaycastHit hit;

                    // Create a particle if hit
                    if (Physics.Raycast(ray, out hit))
                    {   //print("Found an object - distance: " + hit.distance);
                        if (hit.transform.gameObject.tag == "Venus_Pieces")
                        {
                            if (Target_1 == null)
                            {
                                //FirstTarget
                                wrongpair = false;
                                Target_1 = hit.transform.gameObject;
                            }
                            else
                            {
                                //Check Second Piece neighbour list 
                                for (int i = 0; i < hit.transform.gameObject.GetComponent<Pieces_Infomation>().thisPieces.Neighbour.Length; i++)
                                {
                                    if (hit.transform.gameObject.gameObject.GetComponent<Pieces_Infomation>().thisPieces.Neighbour[i] == Target_1)
                                    {
                                        Paired = true;
                                        hit.transform.gameObject.GetComponent<MeshCollider>().isTrigger = true;
                                        Target_1.GetComponent<MeshCollider>().isTrigger = true;

                                        hit.transform.gameObject.GetComponent<Pieces_Infomation>().Positioned = true;
                                        Target_1.GetComponent<Pieces_Infomation>().Positioned = true;

                                        if (hit.transform.gameObject.GetComponent<Pieces_Infomation>().SolutionParent == null)
                                        {
                                            GameObject reTarget = Target_1.transform.gameObject;

                                            for (int j = 0; j < 6; j++)
                                            {
                                                if (reTarget.GetComponent<Pieces_Infomation>().SolutionParent == null)
                                                {
                                                    reTarget.GetComponent<Pieces_Infomation>().SolutionParent = hit.transform.gameObject;
                                                    break;
                                                }
                                                else
                                                {
                                                    reTarget = reTarget.GetComponent<Pieces_Infomation>().SolutionParent.gameObject;
                                                }
                                            }

                                        }
                                        else
                                        {
                                            GameObject CurrentParent = hit.transform.gameObject.GetComponent<Pieces_Infomation>().SolutionParent.gameObject;

                                            for (int j = 0; j < 6; j++)
                                            {
                                                if (CurrentParent.GetComponent<Pieces_Infomation>().SolutionParent == null)
                                                {
                                                    Target_1.GetComponent<Pieces_Infomation>().SolutionParent = CurrentParent;
                                                    break;
                                                }
                                                else
                                                {
                                                    CurrentParent = CurrentParent.GetComponent<Pieces_Infomation>().SolutionParent.gameObject;
                                                }
                                            }

                                        }
                                    }
                                    else if(!Paired && i >= hit.transform.gameObject.GetComponent<Pieces_Infomation>().thisPieces.Neighbour.Length - 1)
                                    {
                                        //Wrong pair
                                        wrongpair = true;
                                    }
                                }

                                Target_1 = null;
                                Paired = false;
                            }
                        }
                        else
                        {
                            Target_1 = null;
                        }
                    }
                }
            }

            for (int i = 0; i < Pieces.Length; i++)
            {
                if (!Pieces[i].Positioned)
                {
                    break;
                }
                else
                {
                    if (i == Pieces.Length - 1)
                    {
                        int Count = 0;
                        for (int j = 0; j < Pieces.Length; j++)
                        {
                            if (Pieces[j].SolutionParent == null)
                            {
                                Count++;
                            }
                        }

                        if (Count == 1)
                        {
                            Clear = true;
                        }
                    }
                }
            }

            if (Clear)
            {
                if (!ShowResult)
                {
                    Result_Time.text = TimeSpend.ToString("0.00");

                    if (PlayerPrefs.HasKey("Venus_Record"))
                    {
                        if (TimeSpend < PlayerPrefs.GetFloat("Venus_Record"))
                        {
                            PlayerPrefs.SetFloat("Venus_Record", TimeSpend);
                            Result_NewBest.gameObject.SetActive(true);
                        }
                        else
                        {
                            Result_NewBest.gameObject.SetActive(false);
                        }
                    }
                    else
                    {
                        PlayerPrefs.SetFloat("Venus_Record", TimeSpend);
                        Result_NewBest.gameObject.SetActive(true);
                    }

                    Result_Best.text = PlayerPrefs.GetFloat("Venus_Record").ToString("0.00");

                    UI.SetActive(false);
                    InGame.SetActive(false);
                    EndGame.SetActive(true);
                    for (int i = 0; i < Pieces.Length; i++)
                    {
                        if (Pieces[i].SolutionParent == null)
                        {
                            SolutionParent = Pieces[i].gameObject;
                        }
                    }

                    ShowResult = true;

                }

                SolutionParent.transform.position = Vector3.MoveTowards(this.transform.GetChild(0).transform.position, this.transform.position, 0.01f);
                anim.SetBool("Clear", true);
                
            }
        }
    }


    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public void StartGame()
    {
        started = true;
        UI.SetActive(false);
        InGame.SetActive(true);
        EndGame.SetActive(false);
    }

    public void BackMenu()
    {
        OuterCanvas.GetComponent<Select_Stage>().BackToPrevious();
    }

    public void Restart()
    {
        OuterCanvas.GetComponent<Select_Stage>().CurrentPlanet_Code = 2;
        OuterCanvas.GetComponent<Select_Stage>().EnterStage();
    }
    public void Stay()
    {
        UI.SetActive(false);
        InGame.SetActive(false);

        EndGame.SetActive(false);
    }

    public void ShowTutorial()
    {
        Tutorial.SetActive(true);
    }
}


[System.Serializable]
public class VenusPieces
{
    public GameObject[] Neighbour;
    public bool[] NeighbourLinked;
    VenusPieces(int Amt)
    {
        Neighbour = new GameObject[Amt];
        NeighbourLinked = new bool[Amt];
    }
}