using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    public GameObject ControlParent;
    public GameObject PlayerPos;
    public GameObject ObjectPos;
    public GameObject BearingPos;


    public float CameraObjRotation;
    public float UIOreintation;
    public Placement placement_ref;

    public Button Up;
    public Button Down;
    public Button Left;
    public Button Right;


    public TextMeshProUGUI DebugLog;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (placement_ref.spawnedObject != null)
        {
            if (ObjectPos == null)
            {
                ObjectPos = placement_ref.spawnedObject.gameObject;
                if (ObjectPos != null)
                {
                    DebugLog.text += "\n" + "Assigned Spawned Object "+ ObjectPos.name.ToString() ;
                }
                else
                {
                    DebugLog.text += "\n" + "Failed Assign Spawned Object";
                }
            }
            
            ControlOreintationUpdate();

        }
        else
        {
            if (ObjectPos != null)
            {
                ObjectPos = null;
            }
        }
    }

    public void ControlOreintationUpdate()
    {

        UIOreintation = Vector2.SignedAngle(new Vector2(BearingPos.transform.position.x, BearingPos.transform.position.z) - new Vector2(ObjectPos.transform.position.x, ObjectPos.transform.position.z), new Vector2(ObjectPos.transform.position.x, ObjectPos.transform.position.z) + new Vector2(0,1));

        Debug.Log("Object: " + ObjectPos.transform.position + " Bearing: " + BearingPos.transform.position + " Oreint: " + UIOreintation);
        ControlParent.transform.rotation = Quaternion.Euler(0, 0, UIOreintation);
    }

    public void Move_UP()
    {
        if(ObjectPos != null)
        ObjectPos.GetComponent<Move>().Move_Up();
    }
    public void Move_DOWN()
    {
        if (ObjectPos != null)
            ObjectPos.GetComponent<Move>().Move_Down();
    }
    public void Move_LEFT()
    {
        if (ObjectPos != null)
            ObjectPos.GetComponent<Move>().Move_Left();
    }
    public void Move_RIGHT()
    {
        if (ObjectPos != null)
            ObjectPos.GetComponent<Move>().Move_Right();
    }
}
