using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR_Cam_Bearing_Position : MonoBehaviour
{
    public GameObject Bearing;
    public GameObject ARCam;
    public PlaceOnPlane plp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (plp.spawnedObject != null)
        {
            Bearing.transform.LookAt(plp.spawnedObject.transform, Vector3.left);
            Bearing.transform.position = new Vector3(ARCam.transform.position.x,plp.spawnedObject.transform.position.y,ARCam.transform.position.z);
        } 
        else
        {
            Bearing.transform.position = ARCam.transform.position;
        }
    }
}
