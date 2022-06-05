using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targeting_SL9 : MonoBehaviour
{
    public Camera cam;
    public GameObject TargetHP;
    public GameObject TargetMarker;
    public GameObject SideMarker;
    public GameObject SelectedTarget;


    public bool Ended;
    public bool Started;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cam == null && GameObject.Find("AR Camera"))
        {
            cam = GameObject.Find("AR Camera").GetComponent<Camera>();
        }

        if (!Ended)
        {
            if (cam.WorldToScreenPoint(SelectedTarget.transform.position).x > cam.scaledPixelWidth)
            {
                SideMarker.SetActive(true);
                SideMarker.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, 0);
                TargetMarker.SetActive(false);
                SideMarker.GetComponent<RectTransform>().position = new Vector2(cam.scaledPixelWidth, cam.WorldToScreenPoint(SelectedTarget.transform.position).y);
            }
            else if (cam.WorldToScreenPoint(SelectedTarget.transform.position).x < 0)
            {
                SideMarker.SetActive(true);
                SideMarker.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, 180);
                TargetMarker.SetActive(false);
                SideMarker.GetComponent<RectTransform>().position = new Vector2(0, cam.WorldToScreenPoint(SelectedTarget.transform.position).y);
            }
            else if (cam.WorldToScreenPoint(SelectedTarget.transform.position).y > cam.scaledPixelHeight)
            {
                SideMarker.SetActive(true);
                SideMarker.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, 90);
                TargetMarker.SetActive(false);
                SideMarker.GetComponent<RectTransform>().position = new Vector2(cam.WorldToScreenPoint(SelectedTarget.transform.position).x, cam.scaledPixelHeight);
            }
            else if (cam.WorldToScreenPoint(SelectedTarget.transform.position).y < 0)
            {
                SideMarker.SetActive(true);
                SideMarker.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, -90);
                TargetMarker.SetActive(false);
                SideMarker.GetComponent<RectTransform>().position = new Vector2(cam.WorldToScreenPoint(SelectedTarget.transform.position).x, 0);
            }
            else
            {
                SideMarker.SetActive(false);
                TargetMarker.SetActive(!Started);
                TargetMarker.GetComponent<RectTransform>().position = cam.WorldToScreenPoint(SelectedTarget.transform.position);
            }
        }
        //TargetHP.GetComponent<RectTransform>().position = cam.WorldToScreenPoint(SelectedTarget.transform.position);
    }
}
