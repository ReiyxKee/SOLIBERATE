using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unlockables : MonoBehaviour
{
    public int ThisPlanet;
    //preview ui
    public GameObject Unlockables_P;
    //ingame ui
    public GameObject Unlockables_IG;

    public int Unlockable_Counts;
    public GameObject ButtonPrefab;

    public GameObject[] Unlockables_Button_P;
    public GameObject[] Unlockables_Button_IG;

    public Unlockable_Detial[] Unlockable_detial;

    public GameObject Desc_P;
    public GameObject Desc_IG;

    public bool All_Unlocked;
    public bool Past_All_Unlocked;

    public bool SaveTest;
    public bool LoadTest;
    // Start is called before the first frame update
    void Start()
    {
        InitSave(ThisPlanet);
        Load(ThisPlanet);
    }

    // Update is called once per frame
    void Update()
    {
        if (Desc_P == null && GameObject.Find("Preview_UI/Description"))
        {
            Desc_P = GameObject.Find("Preview_UI/Description");
            Desc_P.SetActive(false);
        }

        if (Desc_IG == null && GameObject.Find("InGame_UI/Description"))
        {
            Desc_IG = GameObject.Find("InGame_UI/Description");
            Desc_IG.SetActive(false);
        }

        if (Unlockables_P == null)
        {
            if (GameObject.Find("Preview_UI/Unlockables"))
            {
                Unlockables_P = GameObject.Find("Preview_UI/Unlockables");
                foreach (Transform child in Unlockables_P.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
            }
        }
        else
        {
            if (Unlockables_P.activeSelf && Unlockables_P.transform.childCount <= 0)
            {
                Unlockables_Button_P = new GameObject[Unlockable_Counts];
                for (int i = 0; i < Unlockable_Counts; i++)
                {
                    GameObject Button;
                    Button = Instantiate(ButtonPrefab, Vector3.zero, Quaternion.identity);
                    Button.name = "Task (" + i.ToString() + ")";
                    Button.transform.SetParent(Unlockables_P.transform);
                    Button.GetComponentInChildren<Unlockable_Detials>().Detials = Unlockable_detial[i];
                    Button.GetComponentInChildren<Unlockable_Detials>().Desc = Desc_P;
                    Button.GetComponentInChildren<Unlockable_Detials>().PrevIG = true;

                    Unlockables_Button_P[i] = Button;
                }
            }
        }

        if (Unlockables_IG == null)
        {
            if (GameObject.Find("InGame_UI/Unlockables"))
            {
                Unlockables_IG = GameObject.Find("InGame_UI/Unlockables");
                foreach (Transform child in Unlockables_IG.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
            }
        }
        else
        {
            if (Unlockables_IG.activeSelf && Unlockables_IG.transform.childCount <= 0)
            {
                Unlockables_Button_IG = new GameObject[Unlockable_Counts];
                for (int i = 0; i < Unlockable_Counts; i++)
                {
                    GameObject Button;
                    Button = Instantiate(ButtonPrefab, Vector3.zero, Quaternion.identity);
                    Button.name = "Task (" + i.ToString() + ")";
                    Button.transform.SetParent(Unlockables_IG.transform);
                    Button.GetComponentInChildren<Unlockable_Detials>().Detials = Unlockable_detial[i];
                    Button.GetComponentInChildren<Unlockable_Detials>().Desc = Desc_IG;
                    Button.GetComponentInChildren<Unlockable_Detials>().PrevIG = false;

                    Unlockables_Button_IG[i] = Button;
                }
            }
        }

        if (Unlockable_detial.Length >= 3)
        {
           if (Unlockable_detial[0].Unlocked && Unlockable_detial[1].Unlocked)
            {
                Unlockable_detial[2].Unlocked = true;
            }
        }

        if (Unlockables_Button_P.Length != 0)
        {
            for (int i = 0; i < Unlockables_Button_P.Length; i++)
            {
                Unlockables_Button_P[i].GetComponentInChildren<Unlockable_Detials>().Detials.Unlocked = Unlockable_detial[i].Unlocked;
                if (Unlockable_detial[i].Unlocked)
                {
                    Unlockables_Button_P[i].GetComponentInChildren<Text>().text = Unlockables_Button_P[i].GetComponentInChildren<Unlockable_Detials>().Detials.Title;
                }
                else
                {
                    Unlockables_Button_P[i].GetComponentInChildren<Text>().text = "EXPLORE PLANET TO UNLOCK";
                }
            }
        }

        if (Unlockables_Button_IG.Length != 0)
        {
            for (int i = 0; i < Unlockables_Button_IG.Length; i++)
            {
                Unlockables_Button_IG[i].GetComponentInChildren<Unlockable_Detials>().Detials.Unlocked = Unlockable_detial[i].Unlocked;

                if (Unlockable_detial[i].Unlocked)
                {
                    Unlockables_Button_IG[i].GetComponentInChildren<Text>().text = Unlockables_Button_IG[i].GetComponentInChildren<Unlockable_Detials>().Detials.Title;
                }
                else
                {
                    Unlockables_Button_IG[i].GetComponentInChildren<Text>().text = "EXPLORE PLANET TO UNLOCK";
                }
            }
        }

        for (int i = 0; i < Unlockable_Counts; i++)
        {
            if (!Unlockable_detial[i].Unlocked)
            {
                All_Unlocked = false;
                return;
            }
            else if(i == Unlockable_Counts - 1)
            {
                All_Unlocked = true;
            }
        }


        if (SaveTest)
        {
            Save(ThisPlanet, 0, Unlockable_detial[0].Unlocked? 1 : 0);
            Save(ThisPlanet, 1, Unlockable_detial[1].Unlocked? 1 : 0);
            Save(ThisPlanet, 2, Unlockable_detial[2].Unlocked? 1 : 0);
            SaveTest = false;
        }

        if (LoadTest)
        {
            Load(ThisPlanet);
            LoadTest = false;
        }
    }

    //Planet_x_y
    //x = planet num
    //y = disc num
    public void InitSave(int PlanetNum)
    {
        if (!PlayerPrefs.HasKey("Planet_" + PlanetNum.ToString() + "_0".ToString()))
        {
            for (int i = 0; i < Unlockable_Counts; i++)
            {
                Save(ThisPlanet, i, 0);
            }
        }
    }
    public void Save(int PlanetNum, int DiscoveryNum, int Value)
    {
        PlayerPrefs.SetInt("Planet_"+ PlanetNum.ToString() +"_"+ DiscoveryNum.ToString(), Value);
    }

    public void SaveOverall(int PlanetNum, int Value)
    {
        PlayerPrefs.SetInt("Planet_" + PlanetNum.ToString() + "_" + "All_Unlocked", Value);
    }

    public void Load(int PlanetNum)
    {
        Past_All_Unlocked = PlayerPrefs.GetInt("Planet_" + PlanetNum.ToString() + "_" + "All_Unlocked") == 1 ? true:false;

        for (int i = 0; i < Unlockable_Counts; i++)
        {
            Unlockable_detial[i].Unlocked = PlayerPrefs.GetInt("Planet_" + PlanetNum.ToString() + "_" + i.ToString()) == 1 ? true : false;
        }
    }
}

[System.Serializable]
public class Unlockable_Detial
{
    public bool Unlocked;

    public string Title;

    [TextArea]
    public string Desc;
}