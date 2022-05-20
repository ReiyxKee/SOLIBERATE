using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Grading : MonoBehaviour
{
    public Step_Limit step_ref;
    public Unlockables unlockables_ref;
    public Station_List stations;

    public int Grade;
    public bool GameEnd;
    public bool Summary;

    public GameObject Grade_Display;
    public GameObject Report_List;
    public GameObject TMPro_Prefab;

    public string _Grade;
    public string _Step;
    public string _FuelLeft;
    public string _StationUnlocked;
    public string _UnlockableUnlocked;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Grade_Display == null && GameObject.Find("UI/Summary"))
        {
            Grade_Display = GameObject.Find("UI/Summary");
            Grade_Display.SetActive(false);
        }

        if (Report_List == null && GameObject.Find("Viewport/Reports"))
        {
            Report_List = GameObject.Find("Viewport/Reports");
        }

        if (step_ref == null && GameObject.Find("PlayerAxis"))
        {
            step_ref = GameObject.Find("PlayerAxis").GetComponent<Step_Limit>();
        }

        if (unlockables_ref == null && GameObject.Find("PlanetGame"))
        {
            unlockables_ref = GameObject.Find("PlanetGame").GetComponentInChildren<Unlockables>();
        }

        if (stations == null && GameObject.Find("Stations"))
        {
            stations = GameObject.Find("Stations").GetComponent<Station_List>();
        }

        if (step_ref != null && unlockables_ref != null && stations != null)
        {
            if (!GameEnd)
            {
                if (step_ref.Fuel > 0 && unlockables_ref.All_Unlocked && stations.All_Activated)
                {
                    //Unlock all stations including collected all collectibles before fuel ran out
                    //Debug.Log("Grade S");
                    Grade = 0;
                    GameEnd = true;
                }
                else if (step_ref.Fuel <= 0 && unlockables_ref.All_Unlocked && stations.All_Activated)
                {
                    //Unlock all stations including collected all collectibles but fuel ran out
                    //Debug.Log("Grade A");
                    Grade = 1;
                    GameEnd = true;
                }
                else if (step_ref.Fuel <= 0 && !unlockables_ref.All_Unlocked && (unlockables_ref.Unlockable_detial[0].Unlocked || unlockables_ref.Unlockable_detial[1].Unlocked) && stations.All_Activated)
                {
                    //Unlock all stations, collected partial collectibles, fuel ran out
                    //Debug.Log("Grade B");
                    Grade = 2;
                    GameEnd = true;
                }
                else if (step_ref.Fuel <= 0 /* Fuel Finsihed*/ && ((!unlockables_ref.All_Unlocked && (!unlockables_ref.Unlockable_detial[0].Unlocked && !unlockables_ref.Unlockable_detial[1].Unlocked) /*Unlock None Collectibles*/ && stations.All_Activated /*Activated all station*/ ) || ((unlockables_ref.All_Unlocked /*Unlock all COllectibles*/ && !stations.All_Activated /*Not Activated all station*/ ))))
                {
                    //(Unlock all stations, collected none collectibles) or (Unlock all collectibles, didn't activate all station), fuel ran out
                    //Debug.Log("Grade C");
                    Grade = 3;
                    GameEnd = true;
                }
                else if (step_ref.Fuel <= 0 && !unlockables_ref.All_Unlocked && ((!unlockables_ref.Unlockable_detial[0].Unlocked && !unlockables_ref.Unlockable_detial[1].Unlocked) || (unlockables_ref.Unlockable_detial[0].Unlocked || unlockables_ref.Unlockable_detial[1].Unlocked)) && !stations.All_Activated)
                {
                    //Doesn't all stations, collected none or some collectibles, fuel ran out
                    //Debug.Log("Grade D");
                    Grade = 4;
                    GameEnd = true;
                }
            }
        }

        if (Grade_Display != null)
        {
            Grade_Display.SetActive(GameEnd ? true : false);
        }

        if (GameEnd && !Summary)
        {
            //Grade
            switch (Grade)
            {
                case 0: _Grade = "S"; break;
                case 1: _Grade = "A"; break;
                case 2: _Grade = "B"; break;
                case 3: _Grade = "C"; break;
                case 4: _Grade = "D"; break;
            }


            //Step Used
            _Step = (step_ref.MaxFuel - step_ref.Fuel).ToString();

            //Fuel Percentage Left
            _FuelLeft = ((float)step_ref.Fuel/(float)step_ref.MaxFuel * 100).ToString(".00") + " %";

            //Stations Amt
            int unlocked_station_amt = 0;

            for (int i = 0; i < stations.Stations.Length; i++)
            {
                if (stations.Stations[i].On)
                {
                    unlocked_station_amt++;
                }
            }

            _StationUnlocked = "STATION UNLOCKED: " + unlocked_station_amt.ToString() + " / " + stations.Stations.Length.ToString();

            //Unlockable Amt
            int unlockable_amt = 0;
            for (int i = 0; i < unlockables_ref.Unlockable_detial.Length; i++)
            {
                if (unlockables_ref.Unlockable_detial[i].Unlocked)
                {
                    unlockable_amt++;
                }
            }
            _UnlockableUnlocked = "EXPLORATION: " + unlockable_amt.ToString() + " / " + unlockables_ref.Unlockable_detial.Length.ToString();


            //Assign Display
            if (stations.All_Activated)
            {
                GameObject Stations = Instantiate(TMPro_Prefab);
                Stations.transform.SetParent(Report_List.transform);
                Stations.GetComponent<TextMeshProUGUI>().text = _StationUnlocked;

                if (PlayerPrefs.GetInt("Planet_" + unlockables_ref.ThisPlanet.ToString() + "_Desc") == 0)
                {
                    GameObject Stations_ = Instantiate(TMPro_Prefab);
                    Stations_.transform.SetParent(Report_List.transform);
                    Stations_.GetComponent<TextMeshProUGUI>().text = "Planet Details Updated";
                    PlayerPrefs.SetInt("Planet_" + unlockables_ref.ThisPlanet.ToString() + "_Desc", 1);
                }
            }

            GameObject Spacing = Instantiate(TMPro_Prefab);
            Spacing.transform.SetParent(Report_List.transform);
            Spacing.GetComponent<TextMeshProUGUI>().text = " ";

            if (unlockables_ref.All_Unlocked)
            {
                GameObject Unlock = Instantiate(TMPro_Prefab);
                Unlock.transform.SetParent(Report_List.transform);
                Unlock.GetComponent<TextMeshProUGUI>().text = _UnlockableUnlocked;

                if (!unlockables_ref.Past_All_Unlocked)
                {
                    GameObject Unlock_ = Instantiate(TMPro_Prefab);
                    Unlock_.transform.SetParent(Report_List.transform);
                    Unlock_.GetComponent<TextMeshProUGUI>().text = "Unlockables Completed";
                    unlockables_ref.Past_All_Unlocked = true;
                    unlockables_ref.SaveOverall(unlockables_ref.ThisPlanet, 1);
                }
            }

            switch(unlockables_ref.ThisPlanet)
            {
                case 0:
                    GameObject.Find("Summary/Name/Name_Input").GetComponent<TextMeshProUGUI>().text = "Sun"; 
                    break;
                case 1:
                    GameObject.Find("Summary/Name/Name_Input").GetComponent<TextMeshProUGUI>().text = "Mercury"; 
                    break;
                case 2:
                    GameObject.Find("Summary/Name/Name_Input").GetComponent<TextMeshProUGUI>().text = "Venus"; 
                    break;
                case 3:
                    GameObject.Find("Summary/Name/Name_Input").GetComponent<TextMeshProUGUI>().text = "Earth"; 
                    break;
                case 4:
                    GameObject.Find("Summary/Name/Name_Input").GetComponent<TextMeshProUGUI>().text = "Mars"; 
                    break;
                case 5:
                    GameObject.Find("Summary/Name/Name_Input").GetComponent<TextMeshProUGUI>().text = "Jupiter"; 
                    break;
                case 6:
                    GameObject.Find("Summary/Name/Name_Input").GetComponent<TextMeshProUGUI>().text = "Saturn"; 
                    break;
                case 7:
                    GameObject.Find("Summary/Name/Name_Input").GetComponent<TextMeshProUGUI>().text = "Uranus"; 
                    break;
                case 8:
                    GameObject.Find("Summary/Name/Name_Input").GetComponent<TextMeshProUGUI>().text = "Neptune"; 
                    break;
            }

            GameObject.Find("Summary/Step/Step_Input").GetComponent<TextMeshProUGUI>().text = _Step;

            GameObject.Find("Summary/Grade/Grade_Input").GetComponent<TextMeshProUGUI>().text = _Grade;

            GameObject.Find("Summary/Fuel/Fuel_Input").GetComponent<TextMeshProUGUI>().text = _FuelLeft;


            Grade_Display.SetActive(true);
            

            Summary = true;
        }
    }
}