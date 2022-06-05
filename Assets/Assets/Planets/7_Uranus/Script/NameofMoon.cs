using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameofMoon : MonoBehaviour
{
    public Camera cam;
    public GameObject[] Moons;
    public GameObject[] LabelParent;
    public float[] Distance;
    public bool HideName;

    public TextMeshProUGUI HideButton;

    // Start is called before the first frame update
    void Start()
    {
        Distance = new float[Moons.Length];
        for (int i = 0; i < Moons.Length; i++)
        {
            LabelParent[i].transform.GetComponentInChildren<TextMeshProUGUI>().text = Moons[i].gameObject.name;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (cam == null && GameObject.Find("AR Camera"))
        {
            cam = GameObject.Find("AR Camera").GetComponent<Camera>();
        }

        HideButton.text = HideName ? "Show UI" : "Hide UI";

        for (int i = 0; i < Moons.Length; i++)
        {
            if (HideName) 
            {
                LabelParent[i].SetActive(false);
            }
            else
            {
                Distance[i] = Vector3.Distance(Moons[i].transform.GetChild(0).transform.position, cam.gameObject.transform.position);

                LabelParent[i].GetComponent<RectTransform>().transform.localScale = new Vector3(0.25f / Distance[i], 0.25f / Distance[i], 0.25f / Distance[i]);

                LabelParent[i].GetComponent<RectTransform>().transform.position = new Vector2(cam.WorldToScreenPoint(Moons[i].transform.GetChild(0).transform.position).x, cam.WorldToScreenPoint(Moons[i].transform.GetChild(0).transform.position).y + (20f / Distance[i]));

                LabelParent[i].SetActive((Distance[i] < 0.5 && Distance[i] > 0.2) ? true : false);
            }
        }
 
    }

    public void ToggleName()
    {
        HideName = !HideName;
    }
}
