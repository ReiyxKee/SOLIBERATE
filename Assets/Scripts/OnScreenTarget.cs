using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreenTarget : MonoBehaviour
{
    public GameObject SelectedTarget;
    public GameObject TargetMarker;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SelectedTarget == null)
        {
            TargetMarker.SetActive(false);
        }
        else
        {
            TargetMarker.SetActive(true);
            TargetMarker.GetComponent<RectTransform>().position = cam.WorldToScreenPoint(SelectedTarget.transform.GetChild(0).gameObject.transform.position);
        }
    }
}
