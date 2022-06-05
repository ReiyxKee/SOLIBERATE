using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorStripe : MonoBehaviour
{
    public GameObject[] Meteorites;
    public MeteorBlock[] mb;
    // Start is called before the first frame update
    void Start()
    {
        mb = new MeteorBlock[Meteorites.Length];
        for (int i = 0; i < Meteorites.Length; i++)
        {
            mb[i] = Meteorites[i].GetComponentInChildren<MeteorBlock>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
