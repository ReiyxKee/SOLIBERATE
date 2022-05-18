using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Planet_Desc : MonoBehaviour
{
    public Planet_Description[] planets;
    public Select_Stage selectStage;
    public TextMeshProUGUI desc;
    public Scrollbar Scroll;
    // Start is called before the first frame update
    void Start()
    {
        InitSaveDesc();
        LoadDesc();
    }

    // Update is called once per frame
    void Update()
    {
        desc.text = planets[selectStage.CurrentPlanet_Code].Unlocked ? "\n" + planets[selectStage.CurrentPlanet_Code].Desc + "\n\n\n" + planets[selectStage.CurrentPlanet_Code].Detials : "\nCOMPLETE MISSION ON THIS " + (selectStage.CurrentPlanet_Code == 0 ? "STAR" : "PLANET" ) + " TO UNLOCK INFO" + "\n\n\n" + (selectStage.CurrentPlanet_Code == 0 ?"Age: ???\nCore Temperature: ???\nRadius: ???" : "Surface Temperature: ???\nRadius: ???\nOrbiting Days: ???" );
    }

    public void Reset_Pos()
    {
        Scroll.value = 1;
    }

    public void InitSaveDesc()
    {
        if (!PlayerPrefs.HasKey("Planet_" + 0 + "_Desc"))
        {
            for (int i = 0; i < planets.Length; i++)
            {
                PlayerPrefs.SetInt("Planet_" + i.ToString() + "_Desc", 0);
            }
        }
    }

    public void SaveDesc(int PlanetNum, int Value)
    {
        PlayerPrefs.SetInt("Planet_" + PlanetNum.ToString() + "_Desc", Value);
    }
    public void LoadDesc()
    {
        for (int i = 0; i < planets.Length; i++)
        {
            planets[i].Unlocked = PlayerPrefs.GetInt("Planet_" + i.ToString() + "_Desc") == 1 ? true : false;
        }
    }
}

[System.Serializable]
public class Planet_Description
{
    public string Name;

    public bool Unlocked;

    [TextArea]
    public string Desc;
    
    [TextArea]
    public string Detials;
}