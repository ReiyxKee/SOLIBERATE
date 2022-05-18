using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Hide_TapElseWhere : MonoBehaviour
{
    public Camera ARCam;
    public bool Tapped;
    // Start is called before the first frame update
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            if (!IsPointerOverUIObject() && Tapped == false)
            {
                this.gameObject.SetActive(false);
            }
            else if (IsPointerOverUIObject())
            {
                Tapped = true;
            }
        }
        
        if(Input.touchCount == 0 && Tapped)
        { Tapped = false; }

    }

  
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

}
