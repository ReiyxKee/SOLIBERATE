using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class Initial_Adjust : MonoBehaviour
{
    public GameObject Cam;
    public bool ON;
    public float forward = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Cam == null && GameObject.Find("AR Camera"))
        {
            Cam = GameObject.Find("AR Camera");
        }

        if (ON)
        {
            this.transform.position = new Vector3(this.transform.position.x, Cam.transform.position.y, this.transform.position.z);
        }
        else
        {
            if (!this.gameObject.GetComponent<ARAnchor>())
            {
                this.gameObject.AddComponent<ARAnchor>();
            }
        }
    }
}
