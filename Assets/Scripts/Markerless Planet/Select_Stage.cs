using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class Select_Stage : MonoBehaviour
{
    public Placement placement;

    public OnScreenTarget targeter;

    public bool Guide_On;

    public Camera ARCam;
    public bool InStage;
    public bool OnTap;
    public GameObject GameParent;
    public GameObject[] Planets;
    public GameObject[] PlanetsPrefabs;
    public GameObject CurrentPlanet;
    public int CurrentUI;
    public int CurrentPlanet_Code;

    public GameObject UI_StageSelection;
    public GameObject UI_Info;
    public GameObject UI_System;
    public GameObject UI_Calibration;
    //public GameObject UI_InStage;
    //public GameObject UI_PreviewStage;
    //public GameObject UI_DescPrev;

    public GameObject Button_Pause;
    public GameObject Button_Setting;

    public bool Pause_Menu;
    public GameObject UI_PauseMenu;

    public bool Setting_Menu;
    public GameObject UI_SettingMenu;

    public TextMeshProUGUI Instruction;
    public TextMeshProUGUI[] PlanetName;
    public TextMeshProUGUI Enter;
    public TextMeshProUGUI Enter_2;

    public GameObject PlanetInfo;
    public Animator InfoExpand;
    public bool InfoExpanded;
    public TextMeshProUGUI Expand;

    public bool Tutorial_On;
    public Tutorial_Script tutorial;
    public GameObject t_AboutStage;
    public GameObject t_EnterStage;
    public GameObject t_Unlockable;
    public float t_un;
    public float t_c;
    public GameObject t_Start;

    public Button ResetAR;

    public TurningPoint TP;

    public int target_x;

    public GameObject Summary;

    //public GameObject InstructionPanel;

    // Start is called before the first frame update
    void Start()
    {
        UI_StageSelection.SetActive(false);
        UI_Info.SetActive(false);
        UI_Calibration.SetActive(true);
        //UI_PreviewStage.SetActive(false);
        Button_Pause.SetActive(false);
        Button_Setting.SetActive(true);
        Setting_Menu = false;
        Pause_Menu = false;
        //UI_DescPrev.SetActive(true);
        if (PlayerPrefs.HasKey("Tutorial"))
        {
            Debug.Log("First Tutorial Found");
            Tutorial_On = PlayerPrefs.GetInt("Tutorial") == 1 ? true : false;
        }
        else
        {
            Debug.Log("First Tutorial Not Found");
            PlayerPrefs.SetInt("Tutorial", 1);
            Tutorial_On = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (InfoExpanded)
        {
            Expand.text = "COLLAPSE";
        }
        else
        {
            Expand.text = "EXPAND";
        }


        if (CurrentPlanet_Code == 0)
        {
            Enter.text = "ENTER STAR";
            Enter_2.text = "ENTER STAR";
        }
        else
        {
            Enter.text = "ENTER PLANET";
            Enter_2.text = "ENTER PLANET";
        }

        UI_PauseMenu.SetActive(Pause_Menu);
        UI_SettingMenu.SetActive(Setting_Menu);

        if (!InStage)
        {
            TouchSelection();
            Button_Setting.SetActive(true);
            Button_Pause.SetActive(false);
        }
        else
        {
            Button_Setting.SetActive(false);
            Button_Pause.SetActive(true);
        }

        if (GameParent == null)
        {
            if (GameObject.Find("Game_SolarSys"))
            {
                GameParent = GameObject.Find("Game_SolarSys/Sun/Childrens").gameObject;
                Planets = new GameObject[GameParent.transform.childCount+1];
                int i = 0;
                Planets[0] = GameObject.Find("Game_SolarSys/Sun").gameObject;
                i++;
                foreach (Transform planet in GameParent.transform)
                {
                    Planets[i] = planet.gameObject;
                    i++;
                }
            }
            else if (GameObject.Find("Game_SolarSys(Clone)"))
            {
                GameParent = GameObject.Find("Game_SolarSys(Clone)/Sun/Childrens").gameObject;
                Planets = new GameObject[GameParent.transform.childCount + 1];
                int i = 0;
                Planets[0] = GameObject.Find("Game_SolarSys(Clone)/Sun").gameObject;
                i++;
                foreach (Transform planet in GameParent.transform)
                {
                    Planets[i] = planet.gameObject;
                    i++;
                }
            }
        }

        if (placement.Calibration_Complete)
        {
            //if (Tutorial_On)
            //{
            //    New_Tutorial();
            //}

            if (CurrentPlanet != null)
            {
                targeter.SelectedTarget = CurrentPlanet;
                PlanetName[0].text = CurrentPlanet.name;
                PlanetName[1].text = CurrentPlanet.name;
            }
            else
            {
                if (!InStage)
                {
                    targeter.SelectedTarget = null;
                    PlanetName[0].text = "";
                    PlanetName[1].text = "";
                    Hide_UI();
                }
            }
        }

    }
    

    public void TouchSelection()
    {
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
                    if (hit.transform.gameObject.tag == "Planet")
                    {

                        CurrentPlanet = hit.transform.gameObject;
                        Show_StageUI();
                    }
                    else
                    {
                        CurrentPlanet = null;
                    }
                }
                else if (!IsPointerOverUIObject())
                {
                    CurrentPlanet = null;
                    ForceShrink();
                }
            }
        }

        if (CurrentPlanet == null)
        {
            CurrentPlanet_Code = -1;
        }
        else
        switch (CurrentPlanet.gameObject.name)
        {
            case "Sun":
                    CurrentPlanet_Code = 0;
                    break;
            case "Mercury":
                    CurrentPlanet_Code = 1;
                    break;
            case "Venus":
                    CurrentPlanet_Code = 2;
                    break;
            case "Earth":
                    CurrentPlanet_Code = 3;
                    break;
            case "Mars":
                    CurrentPlanet_Code = 4;
                    break;
            case "Jupiter":
                    CurrentPlanet_Code = 5;
                    break;
            case "Saturn":
                    CurrentPlanet_Code = 6;
                    break;
            case "Uranus":
                    CurrentPlanet_Code = 7;
                    break;
            case "Neptune":
                    CurrentPlanet_Code = 8;
                    break;
        }

        if (Input.touchCount == 0)
        {
            OnTap = false;
        }
    }

    public void Current_Selection()
    {

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


    public void UI_Next()
    {
        if (CurrentPlanet == null)
        {
            CurrentPlanet_Code = 0;
        }
        else if(CurrentPlanet_Code == Planets.Length-1)
        {
            CurrentPlanet_Code = 0;
        }
        else
        {
            CurrentPlanet_Code++;
        }

        CurrentPlanet = Planets[CurrentPlanet_Code];
    }
    public void UI_Prev()
    {
        if (CurrentPlanet == null)
        {
            CurrentPlanet_Code = 0;
        }
        else if (CurrentPlanet_Code == 0)
        {
            CurrentPlanet_Code = Planets.Length-1;
        }
        else
        {
            CurrentPlanet_Code--;
        }

        CurrentPlanet = Planets[CurrentPlanet_Code];
    }
    public void EnterStage()
    {
        //UI_DescPrev.SetActive(true);
        GameParent = null;
        Instruction.text = "";
        placement.Spawn_Planet(PlanetsPrefabs[CurrentPlanet_Code]);
        UI_StageSelection.SetActive(false);
        UI_Info.SetActive(false);
        UI_Calibration.SetActive(false);
        //////UI_PreviewStage.SetActive(true);
        InStage = true;

        if (GameObject.Find("Preview_UI/Unlockables"))
        {
            if (GameObject.Find("Preview_UI/Unlockables").transform.childCount > 0)
            {
                foreach (Transform child in GameObject.Find("Preview_UI/Unlockables").transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
            }
        }
    }

    public void BackToPrevious()
    {
        GameParent = null;
        placement.Spawn_Solar();
        InStage = false;
    }

    public void Show_Info()
    {
        Instruction.text = "";
        UI_StageSelection.SetActive(false);
        UI_Info.SetActive(true);
        UI_Calibration.SetActive(false);
        ////UI_PreviewStage.SetActive(false);
    }

    public void Expand_Info()
    {
        InfoExpanded = !InfoExpanded;
        InfoExpand.SetBool("Expand", InfoExpanded);
    }

    public void ForceShrink()
    {
        InfoExpanded = false;
        InfoExpand.SetBool("Expand", InfoExpanded);
    }

    public void Reset_AR()
    {
        //UI_DescPrev.SetActive(true);
        UI_StageSelection.SetActive(false);
        UI_Info.SetActive(false);
        UI_Calibration.SetActive(true);
        //UI_PreviewStage.SetActive(false);
        //UI_InStage.SetActive(false);
        InStage = false;
        GameParent = null;
    }

    public void Show_StageUI()
    {
        Instruction.text = "";
        UI_StageSelection.SetActive(true);
        UI_Info.SetActive(false);
        UI_Calibration.SetActive(false);
        ////UI_PreviewStage.SetActive(false);
    }

    public void Hide_UI()
    {
        //UI_DescPrev.SetActive(true);
        if (!InStage)
        {
            Instruction.text = "Tap on a Planet to view";
        }
        else
        {
            Instruction.text = "";
        }
        UI_StageSelection.SetActive(false);
        UI_Info.SetActive(false);
        UI_Calibration.SetActive(false);
        //UI_PreviewStage.SetActive(false);
        //UI_InStage.SetActive(false);
    }


    public void SettingMenu()
    {
        Setting_Menu = !Setting_Menu;        
    }
    public void PauseMenu()
    {
        Pause_Menu = !Pause_Menu;        
    }

    public void StartLevel()
    {
        GameObject.Find("PlayerAxis").gameObject.GetComponent<Movement>().MovementRef.SetActive(true);
        //UI_InStage.SetActive(true);
        //UI_PreviewStage.SetActive(false);
        //GameObject.Find("PlanetGame").gameObject.GetComponent<Animator>().SetBool("Arrive", true);
    }

    public void ToggleARReset()
    {
        ResetAR.interactable = !ResetAR.interactable;
    }

    public void ActivateSummary()
    {
        Summary.SetActive(true);
    }

    public void New_Tutorial()
    {
        tutorial.Instruction.text = "";
    }
    /*
    public void Tutorial_Manage()
    {
        if (TP == null && GameObject.Find("Turning_Point_XNeg"))
        {
            TP = GameObject.Find("Turning_Point_XNeg").GetComponent<TurningPoint>();
        }

        //Select Earth
        if (tutorial.Guide_TapWorld_Done == false && tutorial.Guide_TapWorld == false)
        {
                tutorial.Instruction.Reset();

                tutorial.Target = Planets[3].transform.GetChild(0).gameObject;
                tutorial.Guide_TapWorld = true;
            
        }

        if (!tutorial.Guide_TapWorld_Done && tutorial.Guide_TapWorld)
        {
            if(CurrentPlanet_Code == 3)
            {
                tutorial.Guide_TapWorld_Done = true;
                tutorial.Guide_TapWorld = true;
                tutorial.Target = null;
            }
        }

        //About
        if (tutorial.Guide_TapWorld_Done && !tutorial.Guide_TapAboutWorld)
        {
                tutorial.Instruction.Reset();

                tutorial.Target = t_AboutStage;
                tutorial.Guide_TapAboutWorld = true;            
        }

        if (!tutorial.Guide_TapAboutWorld_Done && tutorial.Guide_TapAboutWorld)
        {
            if (!t_AboutStage.gameObject.activeInHierarchy)
            { tutorial.Guide_TapAboutWorld_Done = true;
                tutorial.Guide_TapAboutWorld = true;
                tutorial.Target = null;
            }
        }
        //Enter
        if (tutorial.Guide_TapAboutWorld_Done && !tutorial.Guide_TapEnterWorld)
        {
                tutorial.Instruction.Reset();

                tutorial.Target = t_EnterStage;
                tutorial.Guide_TapEnterWorld = true;            
        }

        if (!tutorial.Guide_TapEnterWorld_Done && tutorial.Guide_TapEnterWorld )
        {
            if(!t_EnterStage.gameObject.activeInHierarchy)
            {
                tutorial.Guide_TapEnterWorld_Done = true;
                tutorial.Guide_TapEnterWorld = true;
                tutorial.Target = null;
            }
        }

        //Explain AR
        if (tutorial.Guide_TapEnterWorld_Done && !tutorial.Guide_AR)
        {
                tutorial.Instruction.Reset();

                tutorial.Guide_AR = true;
                OnTap = false;            
        }

        if (tutorial.Guide_AR && !tutorial.Guide_AR_Done)
        {
            if (Input.touchCount == 1 && !OnTap)
            {
                OnTap = true;
            }
            else if (Input.touchCount == 0)
            {
                OnTap = false;
            }

            t_un += Time.deltaTime;
        }

        if (!tutorial.Guide_AR_Done && tutorial.Guide_AR && t_un > 3.0f && OnTap)
        {
            if (!tutorial.Instruction.Complete)
            {
                tutorial.Instruction.Skip();
                OnTap = false;
                t_un = 2;
            }
            else
            {
                tutorial.Guide_AR_Done = true;
                tutorial.Guide_AR = true;
                tutorial.BoxTarget = null;
            }
        }

        //Explain Unlockable
        if (tutorial.Guide_AR_Done && !tutorial.Guide_Unlockable)
        {
                OnTap = false;
                t_un = 0;
                tutorial.Instruction.Reset();

                tutorial.BoxTarget = t_Unlockable;
                tutorial.Guide_Unlockable = true;
                OnTap = false;            
        }

        if (tutorial.Guide_Unlockable && !tutorial.Guide_Unlockable_Done)
        {
            if (Input.touchCount == 1 && !OnTap)
            {
                OnTap = true;
            }
            else if (Input.touchCount == 0)
            {
                OnTap = false;
            }

            t_un += Time.deltaTime;
        }

        if (!tutorial.Guide_Unlockable_Done && tutorial.Guide_Unlockable && t_un > 3.0f && OnTap)
        {
            if (!tutorial.Instruction.Complete)
            {
                tutorial.Instruction.Skip();
                OnTap = false;
                t_un = 2;
            }
            else
            {
                OnTap = false;
                t_un = 0;
                tutorial.Guide_Unlockable_Done = true;
                tutorial.Guide_Unlockable = true;
                tutorial.BoxTarget = null;
            }
        }

        if (tutorial.Guide_Unlockable_Done && !tutorial.Guide_StartGame)
        {
                tutorial.Instruction.Reset();

                tutorial.Target = t_Start;
                tutorial.Guide_StartGame = true;           
        }

        if (!tutorial.Guide_StartGame_Done && tutorial.Guide_StartGame )
        {
            if (!tutorial.Instruction.Complete && OnTap)
            {
                tutorial.Instruction.Skip();
                OnTap = false;
                t_un = 2;
            }
            else if(!t_Start.gameObject.activeInHierarchy)
            {
                tutorial.Guide_StartGame_Done = true;
                tutorial.Guide_StartGame = true;
                tutorial.Target = null;
            }
        }

        //Guide start 
        if (tutorial.Guide_StartGame_Done && !tutorial.Tutorial_Start)
        {
                tutorial.Instruction.Reset();

                OnTap = false;
                t_un = 0;
                tutorial.Tutorial_Start = true;
                tutorial.CircleTarget = GameObject.Find("PlayerAxis/Player");            
        }

        if (tutorial.Tutorial_Start)
        {
            if (Input.touchCount == 1 && !OnTap)
            {
                OnTap = true;
            }
            else if (Input.touchCount == 0)
            {
                OnTap = false;
            }

            t_un += Time.deltaTime;
        }

        //Guide fuel
        if (!tutorial.Tutorial_Start_Done && tutorial.Tutorial_Start && t_un > 3.0f && OnTap)
        {
            if (!tutorial.Instruction.Complete)
            {
                tutorial.Instruction.Skip();
                OnTap = false;
                t_un = 0;
            }
            else
            {
                tutorial.Instruction.Reset();

                tutorial.CircleTarget = null;
                tutorial.Target = null;
                OnTap = false;
                t_un = 0;
                tutorial.Tutorial_Start_Done = true;
                tutorial.Tutorial_Fuel = true;
            }
        }


        if (tutorial.Tutorial_Fuel)
        {
            if (Input.touchCount == 1 && !OnTap)
            {
                OnTap = true;
            }
            else if (Input.touchCount == 0)
            {
                OnTap = false;
            }

            t_un += Time.deltaTime;
        }

        //Guide Move 1
        if (!tutorial.Tutorial_Fuel_Done && tutorial.Tutorial_Fuel && t_un > 3.0f && OnTap)
        {
            if (!tutorial.Instruction.Complete)
            {
                tutorial.Instruction.Skip();
                OnTap = false;
                t_un = 2;
            }
            else
            {
                tutorial.Instruction.Reset();

                OnTap = false;
                t_un = 0;
                TP.ifTutorial = true;
                tutorial.Tutorial_Fuel_Done = true;
                tutorial.Tutorial_Move_1 = true;
            }
        }

        if (tutorial.Tutorial_Move_1)
        {
            if (Input.touchCount == 1 && !OnTap)
            {
                OnTap = true;
            }
            else if (Input.touchCount == 0)
            {
                OnTap = false;
            }

            t_un += Time.deltaTime;
        }

        //Guide Move 2
        if (!tutorial.Tutorial_Move_1_Done && tutorial.Tutorial_Move_1 && t_un > 3.0f)
        {
            if (TP.swipeData.Direction == SwipeDirection.Up)
            {
                tutorial.Instruction.Reset();
                TP.swipeData.Direction = SwipeDirection.None;
                OnTap = false;
                t_un = 0;
                tutorial.Tutorial_Move_1_Done = true;
                tutorial.Tutorial_Move_2 = true;
                TP.ifTutorial = false;
            }
            else if (OnTap && !tutorial.Instruction.Complete)
            {
                tutorial.Instruction.Skip();
                OnTap = false;
                t_un = 2;
            }

        }


        if (tutorial.Tutorial_Move_2)
        {
            if (Input.touchCount == 1 && !OnTap)
            {
                OnTap = true;
            }
            else if (Input.touchCount == 0)
            {
                OnTap = false;
            }

            t_un += Time.deltaTime;
        }

        //Guide Move 3
        if (!tutorial.Tutorial_Move_2_Done && tutorial.Tutorial_Move_2 && t_un > 3.0f && OnTap)
        {
            if (!tutorial.Instruction.Complete)
            {
                tutorial.Instruction.Skip();
                OnTap = false;
                t_un = 2;
            }
            else
            {
                tutorial.Instruction.Reset();

                tutorial.CircleTarget = GameObject.Find("DirectionMarker");
                OnTap = false;
                t_un = 0;
                tutorial.Tutorial_Move_2_Done = true;
                tutorial.Tutorial_Move_3 = true;
            }
        }


        if (tutorial.Tutorial_Move_3)
        {
            if (Input.touchCount == 1 && !OnTap)
            {
                OnTap = true;
            }
            else if (Input.touchCount == 0)
            {
                OnTap = false;
            }

            t_un += Time.deltaTime;
        }

        //Guide Move 4
        if (!tutorial.Tutorial_Move_3_Done && tutorial.Tutorial_Move_3 && t_un > 3.0f && OnTap)
        {
            if (!tutorial.Instruction.Complete)
            {
                tutorial.Instruction.Skip();
                OnTap = false;
                t_un = 2;
            }
            else
            {
                tutorial.Instruction.Reset();

                tutorial.CircleTarget = GameObject.Find("Station (2)");
                OnTap = false;
                t_un = 0;
                tutorial.Tutorial_Move_3_Done = true;
                tutorial.Tutorial_Move_4 = true;
            }
        }


        if (tutorial.Tutorial_Move_4)
        {
            if (Input.touchCount == 1 && !OnTap)
            {
                OnTap = true;
            }
            else if (Input.touchCount == 0)
            {
                OnTap = false;
            }

            t_un += Time.deltaTime;
        }

        //Guide Move 5
        if (!tutorial.Tutorial_Move_4_Done && tutorial.Tutorial_Move_4 && t_un > 3.0f && OnTap)
        {
            if (!tutorial.Instruction.Complete)
            {
                tutorial.Instruction.Skip();
                OnTap = false;
                t_un = 2;
            }
            else
            {
                tutorial.Instruction.Reset();

                tutorial.CircleTarget = null;
                OnTap = false;
                t_un = 0;
                t_c = 0;
                tutorial.Tutorial_Move_4_Done = true;
                tutorial.Tutorial_Move_5 = true;
            }
        }

        if (tutorial.Tutorial_Move_5)
        {
            if (!tutorial.Tutorial_Move_5_Done)
            {
                if (t_c >= 0.5f)
                {
                    if (target_x < 2)
                    {
                        target_x++;
                    }
                    else
                    {
                        target_x = 0;
                    }
                    tutorial.CircleTarget = GameObject.Find("Station (" + target_x.ToString() + ")");
                    t_c = 0;
                }
            }

            if (Input.touchCount == 1 && !OnTap)
            {
                OnTap = true;
            }
            else if (Input.touchCount == 0)
            {
                OnTap = false;
            }

            t_c += Time.deltaTime;
            t_un += Time.deltaTime;
        }

        //Guide Move 6
        if (!tutorial.Tutorial_Move_5_Done && tutorial.Tutorial_Move_5 && t_un > 3.0f && OnTap)
        {
            if (!tutorial.Instruction.Complete)
            {
                tutorial.Instruction.Skip();
                OnTap = false;
                t_un = 2;
            }
            else
            {
                tutorial.Instruction.Reset();

                tutorial.CircleTarget = null;
                OnTap = false;
                t_un = 0;
                tutorial.Tutorial_Move_5_Done = true;
                tutorial.Tutorial_Move_6 = true;
            }
        }

        if (tutorial.Tutorial_Move_6)
        {
            if (Input.touchCount == 1 && !OnTap)
            {
                OnTap = true;
            }
            else if (Input.touchCount == 0)
            {
                OnTap = false;
            }

            t_un += Time.deltaTime;
        }

        //Guide Move 7
        if (!tutorial.Tutorial_Move_6_Done && tutorial.Tutorial_Move_6 && t_un > 3.0f && OnTap)
        {
            if (!tutorial.Instruction.Complete)
            {
                tutorial.Instruction.Skip();
                OnTap = false;
                t_un = 2;
            }
            else
            {
                tutorial.Instruction.Reset();

                OnTap = false;
                t_un = 0;
                tutorial.Tutorial_Move_6_Done = true;
                tutorial.Tutorial_Move_7 = true;
            }
        }

        if (tutorial.Tutorial_Move_7)
        {
            if (Input.touchCount == 1 && !OnTap)
            {
                OnTap = true;
            }
            else if (Input.touchCount == 0)
            {
                OnTap = false;
            }

            t_un += Time.deltaTime;
        }


        if (!tutorial.Tutorial_Move_7_Done && tutorial.Tutorial_Move_7 && t_un > 3.0f && OnTap)
        {
            if (!tutorial.Instruction.Complete)
            {
                tutorial.Instruction.Skip();
                OnTap = false;
                t_un = 2;
            }
            else
            {
                tutorial.Instruction.Reset();

                OnTap = false;
                t_un = 0;
                tutorial.Tutorial_Move_7_Done = true;
                PlayerPrefs.SetInt("Tutorial", 0);
            }
        }
    }
*/
}
