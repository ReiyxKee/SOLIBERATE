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

    public GameObject PlayableTest;
    public GameObject spawnedObject; //{ get; private set; }
    public ARSession ArSession;
    public GameObject Virtual_Cam;

    public GameObject Calibrator;

    public float Forward_Distance;
    public float Floor_Distance;
    public GameObject Cal_Button;
    public GameObject Cal_Img;
    public GameObject Spawn_Button;
    public GameObject Spawn_Img;

    public TextMeshProUGUI Instruction;
    public GameObject InstructionPanel;
    public bool Calibration_Complete;
    public void Start()
    {
        InstructionPanel.SetActive(true);
        Instruction.text = "Please Hold Your Phone Up";
        Calibration_Complete = false;
        Cal_Button.SetActive(true);
        Cal_Img.SetActive(true);
        Spawn_Button.SetActive(false);
        Spawn_Img.SetActive(false);
    }
    public void Update()
    {
        if (Instruction.text == "")
        {
            InstructionPanel.SetActive(false);
        }
        else
        {
            InstructionPanel.SetActive(true);
        }
    }

    public void Calibration_2()
    {
        InstructionPanel.SetActive(true);
        ArSession.Reset();
        Cal_Button.SetActive(false);
        Cal_Img.SetActive(false);
        Spawn_Button.SetActive(true);
        Spawn_Img.SetActive(true);
        Instruction.text = "Stay in a well lit room\nfor best AR Experience";
    }


    public void Spawn_Solar()
    {
        InstructionPanel.SetActive(true);
        Calibration_Complete = true;
        Cal_Button.SetActive(false);
        Cal_Img.SetActive(false);
        Spawn_Button.SetActive(false);
        Spawn_Img.SetActive(false);
        Instruction.text = "";
        Vector3 Position = new Vector3(Virtual_Cam.transform.position.x, Virtual_Cam.transform.position.y - Floor_Distance, Virtual_Cam.transform.position.z) + Virtual_Cam.transform.forward * Forward_Distance;
        if (spawnedObject == null)
        {
            
            spawnedObject = Instantiate(m_Game_Prefab, Position, new Quaternion(0, 0, 0, 0));
            spawnedObject.name = "Game_SolarSys";
            spawnedObject.transform.parent = GameObject.Find("Trackables").transform;
            Pose parentPos = new Pose(spawnedObject.transform.parent.position, Quaternion.identity);
            if (spawnedObject.GetComponent<ARAnchor>() == null)
            {
                spawnedObject.AddComponent<ARAnchor>();
            }
        }
        else
        {
            Destroy(spawnedObject.gameObject);
            spawnedObject = Instantiate(m_Game_Prefab, Position, new Quaternion(0, 0, 0, 0));
            spawnedObject.transform.position = Position;
            spawnedObject.transform.parent = GameObject.Find("Trackables").transform;
            if (spawnedObject.GetComponent<ARAnchor>() == null)
            {
                spawnedObject.AddComponent<ARAnchor>();
            }
        }
    }
    
    public void Spawn_StageTest()
    {
        Cal_Button.SetActive(false);
        Cal_Img.SetActive(false);
        Spawn_Button.SetActive(false);
        Spawn_Img.SetActive(false);
        Instruction.text = "";
        Vector3 Position = new Vector3(Virtual_Cam.transform.position.x, Virtual_Cam.transform.position.y, Virtual_Cam.transform.position.z) + Virtual_Cam.transform.forward*0.5f;
        if (spawnedObject == null)
        {
            spawnedObject = Instantiate(PlayableTest, Position, new Quaternion(0, 0, 0, 0));
            spawnedObject.name = "PlanetGame";
            spawnedObject.transform.parent = GameObject.Find("Trackables").transform;
        }
        else
        {
            Destroy(spawnedObject.gameObject);

            spawnedObject = Instantiate(PlayableTest, Position, new Quaternion(0, 0, 0, 0));
            spawnedObject.name = "PlanetGame";
            spawnedObject.transform.position = Position;
            spawnedObject.transform.parent = GameObject.Find("Trackables").transform;
        }
    }
    public void Spawn_Planet(GameObject Planet)
    {
        Cal_Button.SetActive(false);
        Cal_Img.SetActive(false);
        Spawn_Button.SetActive(false);
        Spawn_Img.SetActive(false);
        Instruction.text = "";
        Vector3 Position = new Vector3(Virtual_Cam.transform.position.x, Virtual_Cam.transform.position.y, Virtual_Cam.transform.position.z) + Virtual_Cam.transform.forward*0.5f;
        if (spawnedObject == null)
        {
            spawnedObject = Instantiate(Planet, Position, new Quaternion(0, 0, 0, 0));
            spawnedObject.name = "PlanetGame";
            spawnedObject.transform.parent = GameObject.Find("Trackables").transform;
            if (spawnedObject.GetComponent<ARAnchor>() == null)
            {
                spawnedObject.AddComponent<ARAnchor>();
            }
        }
        else
        {
            Destroy(spawnedObject.gameObject);

            spawnedObject = Instantiate(Planet, Position, new Quaternion(0, 0, 0, 0));
            spawnedObject.name = "PlanetGame";
            spawnedObject.transform.position = Position;
            spawnedObject.transform.parent = GameObject.Find("Trackables").transform;
            if (spawnedObject.GetComponent<ARAnchor>() == null)
            {
                spawnedObject.AddComponent<ARAnchor>();
            }
        }
    }

    public void Reset_ARSession()
    {
        ArSession.Reset();

        Calibration_Complete = false;
        Destroy(spawnedObject.gameObject);
        spawnedObject = null;
        Cal_Button.SetActive(true);
        Cal_Img.SetActive(true);
        Spawn_Button.SetActive(false);
        Spawn_Img.SetActive(false);
        Instruction.text = "Please Hold Your Phone Upright at your Comfortable Height";
    }

    public void Timescale(float TimeScale)
    {
        Time.timeScale = TimeScale;
    }
}
