using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Color_Swap : MonoBehaviour
{
    public GameObject Model;
    public Material[] Red;
    public Material[] Green;
    public bool On;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Model.GetComponent<MeshRenderer>().materials = On ? Green : Red;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log("Collided");
            On = !On;
        }
    }
}
