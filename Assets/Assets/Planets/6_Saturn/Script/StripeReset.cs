using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StripeReset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Spawner")
        {
            Debug.Log("Reset");
            if (other.GetComponent<RingGenerator>())
            {
                other.GetComponent<RingGenerator>().isReset = false;
            }

            if (other.GetComponent<MeteorBlock>())
            {
                other.GetComponent<MeteorBlock>().Reset();
            }
        }

    }
}
