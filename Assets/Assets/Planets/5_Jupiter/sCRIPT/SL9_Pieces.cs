using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SL9_Pieces : MonoBehaviour
{

    public SL9 parent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Planet")
        {
            parent.CD = false;
            if (!parent.Hit)
            {
                parent.Kaboom.Play();
            }
            parent.Hit = true;
        }
    }
}