using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_Trigger : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Platform")
        {
            Debug.Log("Platform");
            if (other.gameObject.GetComponent<Platform_Type>().Special_Tile)
            {
                Debug.Log("Colored");
                if (this.GetComponent<Cube_Movement>().Color != other.gameObject.GetComponent<Platform_Type>().Color)
                {
                    this.GetComponent<Cube_Movement>().Delay = 1;
                }
            }
        }
        else if(other.tag == "Reset")
        {
            Debug.Log("Reset");
            this.GetComponent<Cube_Movement>()._Reset();
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.tag == "Reset")
        {
            Debug.Log("Reset");
            this.GetComponent<Cube_Movement>()._Reset();
        }
    }
}
