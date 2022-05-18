using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Step_Limit : MonoBehaviour
{
    public int MaxFuel;
    public int Fuel;

    public Slider Fuel_Display;


    // Start is called before the first frame update
    void Start()
    {
        Fuel = MaxFuel;
    }

    // Update is called once per frame
    void Update()
    {
        if (Fuel_Display == null)
        {
            if (GameObject.Find("InGame_UI/Fuel"))
            {
                Fuel_Display = GameObject.Find("InGame_UI/Fuel").GetComponent<Slider>();
            }
        }
        else
        {

            if (Fuel < 0)
            {
                Fuel = 0;
            }

            Fuel_Display.value = 1 * (float)Fuel / (float)MaxFuel;
        }        
    }
}
