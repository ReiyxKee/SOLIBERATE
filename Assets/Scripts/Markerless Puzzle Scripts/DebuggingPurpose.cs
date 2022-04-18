using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;


public class DebuggingPurpose : MonoBehaviour
{
    public GameObject Player;
    //public PlaceOnPlane plp;
    //public GameObject Obj;
    //public GameObject Bearing;

    //public GameObject SwitchStage;

    //public float Angle;

    //public TextMeshProUGUI DebugPlayerPos;
    //public TextMeshProUGUI DebugObjPos;
    //public TextMeshProUGUI DebugBearingPos;
    //public TextMeshProUGUI DebugAngle_PlayerObj;
    //public TextMeshProUGUI DebugAngle_Bearing;
    public TextMeshProUGUI DebugAngle_ARCam;
    //public TextMeshProUGUI DebugAngle_ObjPlayerView;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //    if (plp.spawnedObject != null)
        //    {
        //        SwitchStage.SetActive(true);

        //        Obj = plp.spawnedObject.gameObject;

        //        DebugObjPos.text = "ObjPos(x,y,z): " + Obj.transform.position.x.ToString("0.00") + "," + Obj.transform.position.y.ToString("0.00") + "," + Obj.transform.position.z.ToString("0.00");

        //        DebugAngle_PlayerObj.text = "Angle: " + Vector3.SignedAngle(Obj.transform.forward, Obj.transform.position - Bearing.transform.position, Vector3.up).ToString();

        //        DebugAngle_Bearing.text = "Bearing Angle: " + (Bearing.transform.rotation.y).ToString();

        //        DebugAngle_ObjPlayerView.text = "Obj Angle from view: " + (Vector3.SignedAngle(Obj.transform.forward, Obj.transform.position - Bearing.transform.position, Vector3.up) - Vector3.SignedAngle(Player.transform.forward, Player.transform.position - Obj.transform.position, Vector3.up)).ToString();
        //    }
        //    else
        //    {
        //        SwitchStage.SetActive(false);

        //        Obj = null;

        //        DebugObjPos.text = "ObjPos(x,y,z): " + "Object Not Placed";

        //        DebugAngle_PlayerObj.text = "Bearing to Obj Angle: " + "Object Not Placed";

        //        DebugAngle_Bearing.text = "Bearing Angle: " + "Object Not Placed";

        //        DebugAngle_ObjPlayerView.text = "Obj Angle from view: " + "Object Not Placed";
        //    }

        DebugAngle_ARCam.text = Player.transform.position.ToString();

        //DebugPlayerPos.text = "PlayerPos(x,y,z): " + Player.transform.position.x.ToString("0.00") + "," + Player.transform.position.y.ToString("0.00") + "," + Player.transform.position.z.ToString("0.00");

        //DebugBearingPos.text = "BearingPos(x,y,z): " + Bearing.transform.position.x.ToString("0.00") + "," + Bearing.transform.position.y.ToString("0.00") + "," + Bearing.transform.position.z.ToString("0.00");
    }

    public void SwitchStage_Test()
    {
        //plp.spawnedObject.GetComponent<Current_Stage>().SwitchStage_Test_Next();

    }
}
