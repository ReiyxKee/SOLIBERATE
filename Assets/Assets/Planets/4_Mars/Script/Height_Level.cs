using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Height_Level : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Camera;
    public GameObject Planet;

    public GameObject Label_UI;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Camera == null)
        {
            if (GameObject.Find("AR Camera"))
            {
                Camera = GameObject.Find("AR Camera");
            }
        }

        if ((Planet.transform.position.y - Camera.transform.position.y) > -1 && (Planet.transform.position.y - Camera.transform.position.y) < 1)
        {
            Label_UI.GetComponent<RectTransform>().position = new Vector2(Label_UI.GetComponent<RectTransform>().position.x, Camera.GetComponent<Camera>().scaledPixelHeight/2 + (Camera.transform.position.y - Planet.transform.position.y) / 1 * 0.3f * Camera.GetComponent<Camera>().scaledPixelHeight);
        }
        else if((Camera.transform.position.y - Planet.transform.position.y) < -1)
        {
            Label_UI.GetComponent<RectTransform>().position = new Vector2(Label_UI.GetComponent<RectTransform>().position.x, Camera.GetComponent<Camera>().scaledPixelHeight / 2 - 1 * 0.3f * Camera.GetComponent<Camera>().scaledPixelHeight);
        }
        else if ((Camera.transform.position.y - Planet.transform.position.y) > 1)
        {
            Label_UI.GetComponent<RectTransform>().position = new Vector2(Label_UI.GetComponent<RectTransform>().position.x, Camera.GetComponent<Camera>().scaledPixelHeight / 2 + 1 * 0.3f * Camera.GetComponent<Camera>().scaledPixelHeight);
        }
    }
}
