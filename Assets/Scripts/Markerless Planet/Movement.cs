using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
public float rotateSpeed = 0.1f;
public SwipeDetector swipeDetector;

    public UI ui_Ref;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(ui_Ref == null)
        {
            ui_Ref = GameObject.Find("/Canvas").gameObject.GetComponent<UI>();

        }

        if (Input.touchCount == 1)
        {
            Touch screenTouch = Input.GetTouch(0);

            if (screenTouch.phase == TouchPhase.Moved)
            {
                //Debug.Log(swipeDetector.swipe.Direction);
            }
        }

    }

}
