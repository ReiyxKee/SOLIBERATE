using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station_List : MonoBehaviour
{
    public Color_Swap[] Stations;
    public bool All_Activated;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Stations.Length; i++)
        {
            if (!Stations[i].On)
            {
                All_Activated = false;
                return;
            }
            else if(i == Stations.Length-1)
            {
                All_Activated = true;
            }
        }
    }
}
