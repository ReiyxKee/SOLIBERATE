using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class Neptune_Parts_Manager : MonoBehaviour
{
    public GameObject OuterCanvas;

    public Neptune_Rotate[] NeptuneParts;

    public bool[] LinkedUp;
    public bool[] LinkedDown;

    public Camera ARCam;

    public bool OnTap;

    public bool PlayParticle;
    public bool PlayParticle_ed;
    public ParticleSystem Explode;

    public Animator anim;

    public GameObject CurrentTarget;
    public Vector2 Initial_TouchPos;

    public bool Unlocked;

    public bool Started;

    public TextMeshProUGUI currentTime;
    public TextMeshProUGUI Highest_Record;

    public float TimeSpend;


    public GameObject Tutorial;
    public GameObject UI;
    public GameObject InGame;
    public GameObject EndGame;

    public TextMeshProUGUI Score;
    public TextMeshProUGUI Highscore;
    public GameObject NewHighScore;

    public bool EndGamed;
    public float CountDown_EndGame;



    // Start is called before the first frame update
    void Start()
    {

        if (OuterCanvas == null && GameObject.Find("/Canvas"))
        {
            OuterCanvas = GameObject.Find("/Canvas");
        }


        if (PlayerPrefs.HasKey("Neptune_Record"))
        {
            Highest_Record.text = "best record:\n" + PlayerPrefs.GetFloat("Neptune_Record").ToString(".00");
        }
        else
        {
            Highest_Record.text = "";
        }

        LinkedUp = new bool[NeptuneParts.Length];
        LinkedDown = new bool[NeptuneParts.Length];
    }

    // Update is called once per frame
    void Update()
    {
        currentTime.text = TimeSpend.ToString(".00");

        if (ARCam == null && GameObject.Find("AR Camera"))
        {
            ARCam = GameObject.Find("AR Camera").GetComponent<Camera>();
        }
        if (Started) 
        {

            if (PlayParticle && !Explode.isPlaying && !PlayParticle_ed)
            {
                Explode.Play();
                PlayParticle_ed = true;
            }

            if (Input.touchCount == 0 && OnTap)
            {
                OnTap = false;
                CurrentTarget = null;
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
                    {
                        if (hit.transform.gameObject.name == "1" || hit.transform.gameObject.name == "2" || hit.transform.gameObject.name == "3" || hit.transform.gameObject.name == "4" || hit.transform.gameObject.name == "5")
                        {
                            CurrentTarget = hit.transform.gameObject;
                            Initial_TouchPos = screenTouch.position;
                        }
                    }
                }
            }

            if (OnTap && CurrentTarget != null)
            {
                if (Input.GetTouch(0).deltaPosition.x < -2.5)
                {
                    CurrentTarget.transform.localEulerAngles = new Vector3(CurrentTarget.transform.localEulerAngles.x, CurrentTarget.transform.localEulerAngles.y + 5, CurrentTarget.transform.localEulerAngles.z);
                }
                else if (Input.GetTouch(0).deltaPosition.x > 2.5)
                {
                    CurrentTarget.transform.localEulerAngles = new Vector3(CurrentTarget.transform.localEulerAngles.x, CurrentTarget.transform.localEulerAngles.y - 5, CurrentTarget.transform.localEulerAngles.z);
                }
            }

            if (Unlocked)
            {
                CountDown_EndGame += Time.deltaTime;
                if (CountDown_EndGame >= 3 && !EndGamed)
                {
                    UI.SetActive(false);
                    InGame.SetActive(false);
                    EndGame.SetActive(true);
                    EndGamed = true;
                }
            }
            else
            {
                CheckCombination();
                TimeSpend += Time.deltaTime;
            }
        } 
    }

    public void CheckCombination()
    {
        for (int i = 0; i < NeptuneParts.Length; i++)
        {
            if (i == 0)
            {
                if (NeptuneParts[i].CurrentAngle % 90 == NeptuneParts[i + 1].CurrentAngle % 90)
                {
                    LinkedUp[i] = true;
                    LinkedDown[i] = true;
                }
                else
                {
                    LinkedUp[i] = false;
                    LinkedDown[i] = false;
                }
            }
            else if( i == NeptuneParts.Length -1)
            {
                if (NeptuneParts[i].CurrentAngle % 90 == NeptuneParts[i - 1].CurrentAngle % 90)
                {
                    LinkedUp[i] = true;
                    LinkedDown[i] = true;
                }
                else
                {
                    LinkedUp[i] = false;
                    LinkedDown[i] = false;
                }
            }
            else
            {
                if (NeptuneParts[i].CurrentAngle % 90 == NeptuneParts[i - 1].CurrentAngle % 90)
                {
                    LinkedUp[i] = true;
                }
                else
                {
                    LinkedUp[i] = false;
                }


                if (NeptuneParts[i].CurrentAngle % 90 == NeptuneParts[i + 1].CurrentAngle % 90)
                {
                    LinkedDown[i] = true;
                }
                else
                {
                    LinkedDown[i] = false;
                }
            }
        }
    }

    public void Unlock()
    {
        for (int i = 0; i < NeptuneParts.Length; i++)
        {
            if (!LinkedDown[i] || !LinkedUp[i])
            {
                anim.SetTrigger("Complete_failed");
                return;
            }

            if (i == NeptuneParts.Length - 1)
            {
                Unlocked = true;
                anim.SetTrigger("Complete_success");

                if(PlayerPrefs.HasKey("Neptune_Record"))
                {
                    if (PlayerPrefs.GetFloat("Neptune_Record") > TimeSpend)
                    {
                        NewHighScore.SetActive(true);
                        PlayerPrefs.SetFloat("Neptune_Record", TimeSpend);                                           
                    }
                }
                else
                {
                    PlayerPrefs.SetFloat("Neptune_Record", TimeSpend);
                }

                Score.text = TimeSpend.ToString(".00");
                Highscore.text = PlayerPrefs.GetFloat("Neptune_Record").ToString(".00");
                InGame.SetActive(false);
            }
        }

    }

    public void StartGame()
    {
        Started = true;
        UI.SetActive(false);
        InGame.SetActive(true);
        EndGame.SetActive(false);
    }
    public void _Tutorial()
    {
        Tutorial.SetActive(true);
    }

    public void BackMenu()
    {
        OuterCanvas.GetComponent<Select_Stage>().BackToPrevious();
    }

    public void Restart()
    {
        OuterCanvas.GetComponent<Select_Stage>().CurrentPlanet_Code = 8;
        OuterCanvas.GetComponent<Select_Stage>().EnterStage();
    }
    public void Stay()
    {
        UI.SetActive(false);
        InGame.SetActive(false);
        EndGame.SetActive(false);
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
}
