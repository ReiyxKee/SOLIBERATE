using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

[RequireComponent(typeof(ARRaycastManager)), System.Serializable]
public class Placement : MonoBehaviour
{
    [Tooltip("Instantiates this prefab in front of Player")]
    public GameObject m_Game_Prefab;

    public GameObject Game_Prefab
    {
        get { return m_Game_Prefab; }
        set { m_Game_Prefab = value; }
    }

    public GameObject spawnedObject; //{ get; private set; }
    public ARSession ArSession;
    public GameObject Virtual_Cam;
    
    public GameObject Calibrator;


    public float Forward_Distance;
    public float Floor_Distance;
    private float Cal_Pos_1;
    private float Cal_Pos_2;

    public GameObject Cal_1_Button;
    public GameObject Cal_2_Button;
    public GameObject Spawn_Button;

    public TextMeshProUGUI Instruction;

    public void Start()
    {
        Instruction.text = "Position your phone to the floor as close as possible.";
        Cal_1_Button.SetActive(true);
        Cal_2_Button.SetActive(false);
        Spawn_Button.SetActive(false);
    }
    public void Update()
    {
    }

    public void Calibration_1()
    {
        //Player Get near the floor, Get Pos1
        Cal_Pos_1 = getHeight();
        Cal_1_Button.SetActive(false);
        Cal_2_Button.SetActive(true);
        Spawn_Button.SetActive(false);
        Instruction.text = "Thank You, Now Stand Back Up and Hold Your Phone Camera Facing Forward.";

    }
    public void Calibration_2()
    {
        //Player stand up, get pos2
        Cal_Pos_2 = getHeight();

        //height = pos 1 - pos 2
        Floor_Distance = Cal_Pos_2 - Cal_Pos_1;
        //reset session
        ArSession.Reset();
        //spawn button
        Cal_1_Button.SetActive(false);
        Cal_2_Button.SetActive(false);
        Spawn_Button.SetActive(true);

        Instruction.text = "";
    }
    public float getHeight()
    {
        return Calibrator.transform.position.y;
    }

    public void Spawn_Solar()
    {
        Vector3 Position = new Vector3(Virtual_Cam.transform.position.x, Virtual_Cam.transform.position.y - Floor_Distance, Virtual_Cam.transform.position.z) + Virtual_Cam.transform.forward * Forward_Distance;
        if (spawnedObject == null)
        {
            spawnedObject = Instantiate(m_Game_Prefab, Position, new Quaternion(0, 0, 0, 0));
            spawnedObject.transform.parent = GameObject.Find("Trackables").transform;
        }
        else
        {
            spawnedObject.transform.position = Position;
            spawnedObject.transform.parent = GameObject.Find("Trackables").transform;
        }
    }
}
