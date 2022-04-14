using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
public class Placement_Detection : MonoBehaviour
{
    public PlaceOnPlane Placement;
    public Button ResetPos;
    public ARPlaneManager PlaneManager;
    public GameObject Detected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Placement.spawnedObject != null)
        {
            ResetPos.interactable = true;
            Detected.SetActive(true);
            PlaneDetection(false);
        }
        else
        {
            ResetPos.interactable = false;
            Detected.SetActive(false);
            PlaneDetection(true);
        }
    }

    public void PlaneDetection(bool BOOL)
    {
        PlaneManager.enabled = BOOL;

        if(PlaneManager.enabled)
        {
            PlaneVisibility(true);
        }
        else
        {
            PlaneVisibility(false);
        }
    }

    public void PlaneVisibility(bool BOOL)
    {
        foreach(var plane in PlaneManager.trackables)
        {
            plane.gameObject.SetActive(BOOL);
        }

    }
}
