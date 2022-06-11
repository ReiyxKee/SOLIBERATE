using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;
public class Plate_Manager : MonoBehaviour
{

    public GameObject plateParent_0;
    public GameObject plateParent_1;
    public GameObject plateParent_2;

    public GameObject[] plates_0;
    public GameObject[] plates_1;
    public GameObject[] plates_2;

    //public int Head_0 = -1;
    //public int Head_1 = -1;
    //public int Head_2 = -1;
    
    //public int Tail_0 = -1;
    //public int Tail_1 = -1;
    //public int Tail_2 = -1;


    public Camera ARCam;
    public bool OnTap;
    public bool Started;
    public bool Cleared;

    public GameObject OuterCanvas;

    public GameObject UI;
    public GameObject InGameUI;
    public GameObject EndGameUI;
    public GameObject PrevHighscore;
    public GameObject PrevHighscorePanel;
    public GameObject Tutorial;

    public TextMeshProUGUI Highscore;
    public GameObject NewHighscore;
    public TextMeshProUGUI Result;

    public GameObject Explosion;
    public float ClearTime;
    public TextMeshProUGUI DisplayCurrentTime;
    // Start is called before the first frame update
    void Start()
    {
        Tutorial.SetActive(false);
        InGameUI.SetActive(false);

        if (OuterCanvas == null && GameObject.Find("/Canvas"))
        {
            OuterCanvas = GameObject.Find("/Canvas");
        }
        ClearTime = 0;

        if (!PlayerPrefs.HasKey("Sun_Record"))
        {
            Debug.Log(PlayerPrefs.HasKey("Sun_Record"));
            PrevHighscore.SetActive(false);
            PrevHighscorePanel.SetActive(false);
        }
        else
        {
            Debug.Log(PlayerPrefs.GetFloat("Sun_Record"));
            PrevHighscorePanel.SetActive(true);
            PrevHighscore.SetActive(true);
            PrevHighscore.GetComponent<TextMeshProUGUI>().text = "best record:\n" + PlayerPrefs.GetFloat("Sun_Record").ToString("0.00");
        }
        Started = false;
        UI.SetActive(true);
        EndGameUI.SetActive(false);
        for (int i = 0; i < plates_0.Length; i++)
        {
            plates_0[i].GetComponentInChildren<Tap_On_Plate>().Num = i;
            plates_0[i].GetComponentInChildren<Tap_On_Plate>().Group = 0;
            plates_1[i].GetComponentInChildren<Tap_On_Plate>().Num = i;
            plates_1[i].GetComponentInChildren<Tap_On_Plate>().Group = 1;
            plates_2[i].GetComponentInChildren<Tap_On_Plate>().Num = i;
            plates_2[i].GetComponentInChildren<Tap_On_Plate>().Group = 2;
            plates_0[i].GetComponentInChildren<Tap_On_Plate>().Started = Started;
            plates_1[i].GetComponentInChildren<Tap_On_Plate>().Started = Started;
            plates_2[i].GetComponentInChildren<Tap_On_Plate>().Started = Started;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Started)
        {
            if (plateParent_0.transform.childCount == 0 && plateParent_1.transform.childCount == 0 && plateParent_2.transform.childCount == 0)
            {
                GameClear();
            }
            else
            {
                ClearTime += Time.deltaTime;
            }

            DisplayCurrentTime.text = ClearTime.ToString("0.00") + " s";


            if (ARCam == null && GameObject.Find("AR Camera"))
            {
                ARCam = GameObject.Find("AR Camera").GetComponent<Camera>();
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
                        if (hit.transform.gameObject.tag == "Dyson_Plate")
                        {
                            if (hit.transform.gameObject.GetComponentInChildren<Tap_On_Plate>().CanTap)
                            {
                                int num = hit.transform.gameObject.GetComponentInChildren<Tap_On_Plate>().Num;

                                switch (hit.transform.gameObject.GetComponentInChildren<Tap_On_Plate>().Group)
                                {
                                    case 0:
                                        if(num == 0)
                                        {
                                            if(plates_0[plates_0.Length - 1] != null)
                                            {
                                                GameObject.Destroy(plates_0[plates_0.Length - 1].gameObject);
                                                plates_0[plates_0.Length - 1] = null;
                                                Debug.Log("Destroyed 0 " + (plates_0.Length - 1));
                                            }

                                            if(plates_0[1] != null)
                                            {
                                                GameObject.Destroy(plates_0[1].gameObject);
                                                plates_0[1] = null;
                                                Debug.Log("Destroyed 0 " + 1);
                                            }
                                        }
                                        else if(num == plates_0.Length - 1)
                                        {
                                            if (plates_0[plates_0.Length - 2] != null)
                                            {
                                                GameObject.Destroy(plates_0[plates_0.Length - 2].gameObject);
                                                plates_0[plates_0.Length - 2] = null;
                                                Debug.Log("Destroyed 0 " + (plates_0.Length - 2));
                                            }

                                            if (plates_0[0] != null)
                                            {
                                                GameObject.Destroy(plates_0[0].gameObject);
                                                plates_0[0] = null;
                                                Debug.Log("Destroyed 0 " + (0));
                                            }
                                        }
                                        else
                                        {
                                            if (plates_0[num - 1] != null)
                                            {
                                                GameObject.Destroy(plates_0[num - 1].gameObject);
                                                plates_0[num - 1] = null;
                                                Debug.Log("Destroyed 0 " + (num - 1));
                                            }

                                            if (plates_0[num + 1] != null)
                                            {
                                                GameObject.Destroy(plates_0[num + 1].gameObject);
                                                plates_0[num + 1] = null;
                                                Debug.Log("Destroyed 0 " + (num + 1));
                                            }
                                        }

                                        Instantiate(Explosion, hit.transform.position, hit.transform.rotation);
                                        if (plates_0[num] != null)
                                        {
                                            GameObject.Destroy(plates_0[num].gameObject);
                                            plates_0[num] = null;
                                        }
                                        break;
                                    
                                    case 1:
                                        if(num == 0)
                                        {
                                            if(plates_1[plates_1.Length - 1] != null)
                                            {
                                                GameObject.Destroy(plates_1[plates_1.Length - 1].gameObject);
                                                plates_1[plates_1.Length - 1] = null;
                                                Debug.Log("Destroyed 1 " + (plates_1.Length - 1));
                                            }

                                            if(plates_1[1] != null)
                                            {
                                                GameObject.Destroy(plates_1[1].gameObject);
                                                plates_1[1] = null;
                                                Debug.Log("Destroyed 1 " + 1);
                                            }
                                        }
                                        else if(num == plates_1.Length - 1)
                                        {
                                            if (plates_1[plates_1.Length - 2] != null)
                                            {
                                                GameObject.Destroy(plates_1[plates_1.Length - 2].gameObject);
                                                plates_1[plates_1.Length - 2] = null;
                                                Debug.Log("Destroyed 1 " + (plates_1.Length - 2));
                                            }

                                            if (plates_1[0] != null)
                                            {
                                                GameObject.Destroy(plates_1[0].gameObject);
                                                plates_1[0] = null;
                                                Debug.Log("Destroyed 1 " + (0));
                                            }
                                        }
                                        else
                                        {
                                            if (plates_1[num - 1] != null)
                                            {
                                                GameObject.Destroy(plates_1[num - 1].gameObject);
                                                plates_1[num - 1] = null;
                                                Debug.Log("Destroyed 1 " + (num - 1));
                                            }

                                            if (plates_1[num + 1] != null)
                                            {
                                                GameObject.Destroy(plates_1[num + 1].gameObject);
                                                plates_1[num + 1] = null;
                                                Debug.Log("Destroyed 1 " + (num + 1));
                                            }
                                        }

                                        Instantiate(Explosion, hit.transform.position, hit.transform.rotation);
                                        if (plates_1[num] != null)
                                        {
                                            GameObject.Destroy(plates_1[num].gameObject);
                                            plates_1[num] = null;
                                        }
                                        break;
                                    case 2:
                                        if (num == 0)
                                        {
                                            if (plates_2[plates_2.Length - 1] != null)
                                            {
                                                GameObject.Destroy(plates_2[plates_2.Length - 1].gameObject);
                                                plates_2[plates_2.Length - 1] = null;
                                                Debug.Log("Destroyed 2 " + (plates_2.Length - 1));
                                            }

                                            if (plates_2[1] != null)
                                            {
                                                GameObject.Destroy(plates_2[1].gameObject);
                                                plates_2[1] = null;
                                                Debug.Log("Destroyed 2 " + 1);
                                            }
                                        }
                                        else if (num == plates_2.Length - 1)
                                        {
                                            if (plates_2[plates_2.Length - 2] != null)
                                            {
                                                GameObject.Destroy(plates_2[plates_2.Length - 2].gameObject);
                                                plates_2[plates_2.Length - 2] = null;
                                                Debug.Log("Destroyed 2 " + (plates_2.Length - 2));
                                            }

                                            if (plates_2[0] != null)
                                            {
                                                GameObject.Destroy(plates_2[0].gameObject);
                                                plates_2[0] = null;
                                                Debug.Log("Destroyed 2 " + (0));
                                            }
                                        }
                                        else
                                        {
                                            if (plates_2[num - 1] != null)
                                            {
                                                GameObject.Destroy(plates_2[num - 1].gameObject);
                                                plates_2[num - 1] = null;
                                                Debug.Log("Destroyed 2 " + (num - 1));
                                            }

                                            if (plates_2[num + 1] != null)
                                            {
                                                GameObject.Destroy(plates_2[num + 1].gameObject);
                                                plates_2[num + 1] = null;
                                                Debug.Log("Destroyed 2 " + (num + 1));
                                            }
                                        }

                                        Instantiate(Explosion, hit.transform.position, hit.transform.rotation);
                                        if (plates_2[num] != null)
                                        {
                                            GameObject.Destroy(plates_2[num].gameObject);
                                            plates_2[num] = null;
                                        }
                                        break;


                                }

                            }
                        }
                    }
                }
            }

        }
    }

    public void StartGame()
    {
        Started = true;
        for (int i = 0; i < plates_0.Length; i++)
        {
            plates_0[i].GetComponentInChildren<Tap_On_Plate>().Started = Started;
            plates_1[i].GetComponentInChildren<Tap_On_Plate>().Started = Started;
            plates_2[i].GetComponentInChildren<Tap_On_Plate>().Started = Started;
        }
        UI.SetActive(false);
        InGameUI.SetActive(true);
        EndGameUI.SetActive(false);
        Tutorial.SetActive(false);
    }
    public void ShowTutorial()
    {
        Tutorial.SetActive(true);
    }

    public void GameClear()
    {
        if (!Cleared)
        {
            Debug.Log(PlayerPrefs.GetFloat("Sun_Record"));
            NewHighscore.SetActive(ClearTime < PlayerPrefs.GetFloat("Sun_Record"));
            if (ClearTime < PlayerPrefs.GetFloat("Sun_Record") || !PlayerPrefs.HasKey("Sun_Record"))
            {
                PlayerPrefs.SetFloat("Sun_Record", ClearTime);
            }

            Tutorial.SetActive(false);
            InGameUI.SetActive(false);
            UI.SetActive(false);
            EndGameUI.SetActive(true);
            Result.text = ClearTime.ToString("0.00") + " s";
            Highscore.text = PlayerPrefs.HasKey("Sun_Record") ? PlayerPrefs.GetFloat("Sun_Record").ToString("0.00") + " s" : ClearTime.ToString("0.00") + " s";
            Cleared = true;
        }
    }

    public void BackMenu()
    {
        OuterCanvas.GetComponent<Select_Stage>().BackToPrevious();
    }

    public void Stay()
    {
        UI.SetActive(false);
        InGameUI.SetActive(false);
        EndGameUI.SetActive(false);
    }    
    public void Restart()
    {
        OuterCanvas.GetComponent<Select_Stage>().CurrentPlanet_Code = 0;
        OuterCanvas.GetComponent<Select_Stage>().EnterStage();
    }
    /*
    public void DestroyGroup(GameObject Target)
    {
        int num = Target.GetComponentInChildren<Tap_On_Plate>().Num;

        switch (Target.GetComponentInChildren<Tap_On_Plate>().Group)
        {
            case 0:
                Debug.Log("Tapped " + num);

                if (Head_0 == -1)
                {
                    Head_0 = num;
                }
                else if (Tail_0 == -1)
                {
                    if (Head_0 > num)
                    {
                        Tail_0 = Head_0;
                        Head_0 = num;
                    }
                    else
                    {
                        Tail_0 = num;
                    }

                    if (Tail_0 - Head_0 < Head_0 + plates_0.Length - Tail_0)
                    {
                        for (int i = Head_0; i < Tail_0; i++)
                        {
                            plates_0[i].gameObject.GetComponentInChildren<Tap_On_Plate>().Tapped = true;
                            GameObject.Destroy(plates_0[i].gameObject);
                        }
                    }
                    else
                    {
                        for (int i = Head_0 + plates_0.Length; i > Tail_0; i--)
                        {
                            if (i >= plates_0.Length)
                            {
                                plates_0[i - plates_0.Length].gameObject.GetComponentInChildren<Tap_On_Plate>().Tapped = true;
                                GameObject.Destroy(plates_0[i - plates_0.Length].gameObject);
                            }
                            else
                            {
                                plates_0[i].gameObject.GetComponentInChildren<Tap_On_Plate>().Tapped = true;
                                GameObject.Destroy(plates_0[i].gameObject);
                            }
                        }
                    }
                }
                else
                {
                    if (Head_0 + plates_0.Length - Tail_0 > Tail_0 - Head_0)
                    {
                        if (0 < num && num < Head_0)
                        {
                            if (Head_0 + plates_0.Length - num < num + 20 - Tail_0)
                            {
                                for (int i = Head_0 + plates_0.Length; i > num; i--)
                                {
                                    plates_0[i].gameObject.GetComponentInChildren<Tap_On_Plate>().Tapped = true;
                                    GameObject.Destroy(plates_0[i].gameObject);
                                }

                                Head_0 = num;
                                Debug.Log("Case 1");
                            }
                            else
                            {
                                for (int i = Tail_0; i < num; i++)
                                {
                                    if (i >= plates_0.Length)
                                    {
                                        plates_0[i - plates_0.Length].gameObject.GetComponentInChildren<Tap_On_Plate>().Tapped = true;
                                        GameObject.Destroy(plates_0[i - plates_0.Length].gameObject);
                                    }
                                    else
                                    {
                                        plates_0[i].gameObject.GetComponentInChildren<Tap_On_Plate>().Tapped = true;
                                        GameObject.Destroy(plates_0[i].gameObject);
                                    }
                                }

                                Tail_0 = num;
                                Debug.Log("Case 2");
                            }
                        }
                        else
                        {
                            if (Head_0 + plates_0.Length - num < num - Tail_0)
                            {
                                for (int i = Head_0 + plates_0.Length; i > num; i--)
                                {
                                    if (i >= plates_0.Length)
                                    {
                                        plates_0[i - plates_0.Length].gameObject.GetComponentInChildren<Tap_On_Plate>().Tapped = true;
                                        GameObject.Destroy(plates_0[i - plates_0.Length].gameObject);
                                    }
                                    else
                                    {
                                        plates_0[i].gameObject.GetComponentInChildren<Tap_On_Plate>().Tapped = true;
                                        GameObject.Destroy(plates_0[i].gameObject);
                                    }
                                }

                                Head_0 = num;
                                Debug.Log("Case 3");
                            }
                            else
                            {
                                for (int i = Tail_0; i < num; i++)
                                {
                                    plates_0[i].gameObject.GetComponentInChildren<Tap_On_Plate>().Tapped = true;
                                    GameObject.Destroy(plates_0[i].gameObject);
                                }
                                Tail_0 = num;
                                Debug.Log("Case 4");
                            }
                        }
                    }
                    else
                    {
                        if (Tail_0 - num < num - Head_0)
                        {
                            for (int i = Tail_0; i > num; i--)
                            {
                                plates_0[i].gameObject.GetComponentInChildren<Tap_On_Plate>().Tapped = true;
                                GameObject.Destroy(plates_0[i].gameObject);
                            }

                            Tail_0 = num;
                            Debug.Log("Case 5");
                        }
                        else
                        {
                            for (int i = Head_0; i < num; i++)
                            {
                                plates_0[i].gameObject.GetComponentInChildren<Tap_On_Plate>().Tapped = true;
                                GameObject.Destroy(plates_0[i].gameObject);
                            }

                            Head_0 = num;
                            Debug.Log("Case 6");
                        }
                    }

                    plates_0[num].GetComponentInChildren<Tap_On_Plate>().Tapped = true;
                    GameObject.Destroy(plates_0[num].gameObject);

                    if (Head_0 > Tail_0)
                    {
                        int temp = Tail_0;
                        Tail_0 = Head_0;
                        Head_0 = temp;
                        Debug.Log("swap");
                    }
                }
                Debug.Log(Head_0 + " , " + Tail_0);
                break;
            case 1:
                Debug.Log("Tapped " + num);
                GameObject.Destroy(plates_1[num].gameObject);

                if (Head_1 == -1)
                {
                    Head_1 = num;
                }
                else if (Tail_1 == -1)
                {
                    if (Head_1 > num)
                    {
                        Tail_1 = Head_1;
                        Head_1 = num;
                    }
                    else
                    {
                        Tail_1 = num;
                    }

                    if (Tail_1 - Head_1 < Head_1 + plates_1.Length - Tail_1)
                    {
                        for (int i = Head_1; i < Tail_1; i++)
                        {
                            GameObject.Destroy(plates_1[i].gameObject);
                        }
                    }
                    else
                    {
                        for (int i = Head_1 + plates_1.Length; i > Tail_1; i--)
                        {
                            if (i >= plates_1.Length)
                            {
                                GameObject.Destroy(plates_1[i - plates_1.Length].gameObject);
                            }
                            else
                            {
                                GameObject.Destroy(plates_1[i].gameObject);
                            }
                        }
                    }
                }
                else
                {
                    if (Head_1 + plates_1.Length - Tail_1 > Tail_1 - Head_1)
                    {
                        if (0 < num && num < Head_1)
                        {
                            if (Head_1 + plates_1.Length - num < num + 20 - Tail_1)
                            {
                                for (int i = Head_1 + plates_1.Length; i > num; i--)
                                {
                                    GameObject.Destroy(plates_1[i].gameObject);
                                }

                                Head_1 = num;
                                Debug.Log("Case 1");
                            }
                            else
                            {
                                for (int i = Tail_1; i < num; i++)
                                {
                                    if (i >= plates_1.Length)
                                    {
                                        GameObject.Destroy(plates_1[i - plates_1.Length].gameObject);
                                    }
                                    else
                                    {
                                        GameObject.Destroy(plates_1[i].gameObject);
                                    }
                                }

                                Tail_1 = num;
                                Debug.Log("Case 2");
                            }
                        }
                        else
                        {
                            if (Head_1 + plates_1.Length - num < num - Tail_1)
                            {
                                for (int i = Head_1 + plates_1.Length; i > num; i--)
                                {
                                    if (i >= plates_1.Length)
                                    {
                                        GameObject.Destroy(plates_1[i - plates_1.Length].gameObject);
                                    }
                                    else
                                    {
                                        plates_1[i].gameObject.GetComponentInChildren<Tap_On_Plate>().Tapped = true;
                                        GameObject.Destroy(plates_1[i].gameObject);
                                    }
                                }

                                Head_1 = num;
                                Debug.Log("Case 3");
                            }
                            else
                            {
                                for (int i = Tail_1; i < num; i++)
                                {
                                    plates_1[i].gameObject.GetComponentInChildren<Tap_On_Plate>().Tapped = true;
                                    GameObject.Destroy(plates_1[i].gameObject);
                                }
                                Tail_1 = num;
                                Debug.Log("Case 4");
                            }
                        }
                    }
                    else
                    {
                        if (Tail_1 - num < num - Head_1)
                        {
                            for (int i = Tail_1; i > num; i--)
                            {
                                plates_1[i].gameObject.GetComponentInChildren<Tap_On_Plate>().Tapped = true;
                                GameObject.Destroy(plates_1[i].gameObject);
                            }

                            Tail_1 = num;
                            Debug.Log("Case 5");
                        }
                        else
                        {
                            for (int i = Head_1; i < num; i++)
                            {
                                plates_1[i].gameObject.GetComponentInChildren<Tap_On_Plate>().Tapped = true;
                                GameObject.Destroy(plates_1[i].gameObject);
                            }

                            Head_1 = num;
                            Debug.Log("Case 6");
                        }
                    }


                    if (Head_1 > Tail_1)
                    {
                        int temp = Tail_1;
                        Tail_1 = Head_1;
                        Head_1 = temp;
                        Debug.Log("swap");
                    }
                }
                Debug.Log(Head_1 + " , " + Tail_1);
                break;
            case 2:
                Debug.Log("Tapped " + num);

                if (Head_2 == -1)
                {
                    Head_2 = num;
                }
                else if (Tail_2 == -1)
                {
                    if (Head_2 > num)
                    {
                        Tail_2 = Head_2;
                        Head_2 = num;
                    }
                    else
                    {
                        Tail_2 = num;
                    }

                    if (Tail_2 - Head_2 < Head_2 + plates_2.Length - Tail_2)
                    {
                        for (int i = Head_2; i < Tail_2; i++)
                        {
                            plates_2[i].gameObject.GetComponentInChildren<Tap_On_Plate>().Tapped = true;
                            GameObject.Destroy(plates_2[i].gameObject);
                        }
                    }
                    else
                    {
                        for (int i = Head_2 + plates_2.Length; i > Tail_2; i--)
                        {
                            if (i >= plates_2.Length)
                            {
                                plates_2[i - plates_2.Length].gameObject.GetComponentInChildren<Tap_On_Plate>().Tapped = true;
                                GameObject.Destroy(plates_2[i - plates_2.Length].gameObject);
                            }
                            else
                            {
                                plates_2[i].gameObject.GetComponentInChildren<Tap_On_Plate>().Tapped = true;
                                GameObject.Destroy(plates_2[i].gameObject);
                            }
                        }
                    }
                }
                else
                {
                    if (Head_2 + plates_2.Length - Tail_2 > Tail_2 - Head_2)
                    {
                        if (0 < num && num < Head_2)
                        {
                            if (Head_2 + plates_2.Length - num < num + 20 - Tail_2)
                            {
                                for (int i = Head_2 + plates_2.Length; i > num; i--)
                                {
                                    plates_2[i].gameObject.GetComponentInChildren<Tap_On_Plate>().Tapped = true;
                                    GameObject.Destroy(plates_2[i].gameObject);
                                }

                                Head_2 = num;
                                Debug.Log("Case 1");
                            }
                            else
                            {
                                for (int i = Tail_2; i < num; i++)
                                {
                                    if (i >= plates_2.Length)
                                    {
                                        plates_2[i - plates_2.Length].gameObject.GetComponentInChildren<Tap_On_Plate>().Tapped = true;
                                        GameObject.Destroy(plates_2[i - plates_2.Length].gameObject);
                                    }
                                    else
                                    {
                                        plates_2[i].gameObject.GetComponentInChildren<Tap_On_Plate>().Tapped = true;
                                        GameObject.Destroy(plates_2[i].gameObject);
                                    }
                                }

                                Tail_2 = num;
                                Debug.Log("Case 2");
                            }
                        }
                        else
                        {
                            if (Head_2 + plates_2.Length - num < num - Tail_2)
                            {
                                for (int i = Head_2 + plates_2.Length; i > num; i--)
                                {
                                    if (i >= plates_2.Length)
                                    {
                                        plates_2[i - plates_2.Length].gameObject.GetComponentInChildren<Tap_On_Plate>().Tapped = true;
                                        GameObject.Destroy(plates_2[i - plates_2.Length].gameObject);
                                    }
                                    else
                                    {
                                        plates_2[i].gameObject.GetComponentInChildren<Tap_On_Plate>().Tapped = true;
                                        GameObject.Destroy(plates_2[i].gameObject);
                                    }
                                }

                                Head_2 = num;
                                Debug.Log("Case 3");
                            }
                            else
                            {
                                for (int i = Tail_2; i < num; i++)
                                {
                                    plates_2[i].gameObject.GetComponentInChildren<Tap_On_Plate>().Tapped = true;
                                    GameObject.Destroy(plates_2[i].gameObject);
                                }
                                Tail_2 = num;
                                Debug.Log("Case 4");
                            }
                        }
                    }
                    else
                    {
                        if (Tail_2 - num < num - Head_2)
                        {
                            for (int i = Tail_2; i > num; i--)
                            {
                                plates_2[i].gameObject.GetComponentInChildren<Tap_On_Plate>().Tapped = true;
                                GameObject.Destroy(plates_2[i].gameObject);
                            }

                            Tail_2 = num;
                            Debug.Log("Case 5");
                        }
                        else
                        {
                            for (int i = Head_2; i < num; i++)
                            {
                                plates_2[i].gameObject.GetComponentInChildren<Tap_On_Plate>().Tapped = true;
                                GameObject.Destroy(plates_2[i].gameObject);
                            }

                            Head_2 = num;
                            Debug.Log("Case 6");
                        }
                    }

                    plates_2[num].GetComponentInChildren<Tap_On_Plate>().Tapped = true;
                    GameObject.Destroy(plates_2[num].gameObject);

                    if (Head_2 > Tail_2)
                    {
                        int temp = Tail_2;
                        Tail_2 = Head_2;
                        Head_2 = temp;
                        Debug.Log("swap");
                    }
                }
                Debug.Log(Head_2 + " , " + Tail_2);
                break;
        }
    }
    */

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
