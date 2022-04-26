using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinching_Zoom : MonoBehaviour
{
    private float initialDistance;
    private Vector3 initialScale;

    public float zoomCapMin;
    public float zoomCapMax;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount == 2)
        {
            var touch0 = Input.GetTouch(0);
            var touch1 = Input.GetTouch(1);

            if (touch0.phase == TouchPhase.Ended || touch0.phase == TouchPhase.Canceled || touch1.phase == TouchPhase.Ended || touch1.phase == TouchPhase.Canceled)
            {
                return;
            }

            if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
            {
                initialDistance = Vector2.Distance(touch0.position, touch1.position);
                initialScale = this.transform.localScale;
            }
            else 
            {
                var currentDistance = Vector2.Distance(touch0.position, touch1.position);

                if (Mathf.Approximately(initialDistance, 0))
                {
                    return; 
                }

                var factor = currentDistance / initialDistance;
                this.transform.localScale = initialScale * factor;
                this.transform.localScale = new Vector3(Mathf.Clamp(this.transform.localScale.x, zoomCapMin, zoomCapMax), Mathf.Clamp(this.transform.localScale.y, zoomCapMin, zoomCapMax), Mathf.Clamp(this.transform.localScale.z, zoomCapMin, zoomCapMax));
            }

        }
    }
}
