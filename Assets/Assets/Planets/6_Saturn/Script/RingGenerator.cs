using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingGenerator : MonoBehaviour
{
    public bool isReset;

    public GameObject[] Meteorite;
    public GameObject Ring;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isReset)
        {
            if (this.transform.childCount > 0)
            {
                GameObject.Destroy(this.transform.GetChild(0).gameObject);
            }

            float Randomize = Random.Range(0, 100);

            if (Randomize > 0 && Randomize < 70)
            {
                //Spawn ntg
            }
            else if(Randomize > 70 && Randomize < 85)
            {
                //Spawn ring
                GameObject ring = Instantiate(Ring, this.transform.position, Quaternion.identity, this.transform);

                ring.tag = "Ring";
            }
            else
            {
                GameObject rock = Instantiate(Meteorite[Random.Range(0, Meteorite.Length)], this.transform.position, Quaternion.identity, this.transform);

                rock.tag = "Meteorite";
                //Spawn Meteorite
            }

            isReset = true;
        }
    }
}
