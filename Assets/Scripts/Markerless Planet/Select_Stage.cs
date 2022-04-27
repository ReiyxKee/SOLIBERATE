using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class Select_Stage : MonoBehaviour
{
    public Placement placement;

    public OnScreenTarget targeter;

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
    public GameObject UI_PreviewStage;

    public GameObject Button_Pause;
    public GameObject Button_Setting;

    public bool Pause_Menu;
    public GameObject UI_PauseMenu;

    public bool Setting_Menu;
    public GameObject UI_SettingMenu;

    public TextMeshProUGUI Instruction;
    public TextMeshProUGUI[] PlanetName;


    // Start is called before the first frame update
    void Start()
    {
        UI_StageSelection.SetActive(false);
        UI_Info.SetActive(false);
        UI_Calibration.SetActive(true);
        UI_PreviewStage.SetActive(false);
        Button_Pause.SetActive(false);
        Button_Setting.SetActive(true);
        Setting_Menu = false;
        Pause_Menu = false;

    }

    // Update is called once per frame
    void Update()
    {
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
        GameParent = null;
        Instruction.text = "";
        placement.Spawn_Planet(PlanetsPrefabs[CurrentPlanet_Code]);
        UI_StageSelection.SetActive(false);
        UI_Info.SetActive(false);
        UI_Calibration.SetActive(false);
        UI_PreviewStage.SetActive(true);
        InStage = true;
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
        UI_PreviewStage.SetActive(false);
    }

    public void Expand_Info()
    {

    }

    public void Reset_AR()
    {
        UI_StageSelection.SetActive(false);
        UI_Info.SetActive(false);
        UI_Calibration.SetActive(true);
        UI_PreviewStage.SetActive(false);
        InStage = false;
        GameParent = null;
    }

    public void Show_StageUI()
    {
        Instruction.text = "";
        UI_StageSelection.SetActive(true);
        UI_Info.SetActive(false);
        UI_Calibration.SetActive(false);
        UI_PreviewStage.SetActive(false);
    }

    public void Hide_UI()
    {
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
        UI_PreviewStage.SetActive(false);
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
        GameObject.Find("PlanetGame").gameObject.GetComponent<Animator>().SetBool("Arrive", true);

    }
}
