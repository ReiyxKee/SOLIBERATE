using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpaceShip_Mars : MonoBehaviour
{
    public float survivedDuration;
    public MeteorManager manager;

    public TextMeshProUGUI time;

    public TextMeshProUGUI timeCleared;
    public TextMeshProUGUI timeHighscore_UI;
    public TextMeshProUGUI timeHighscore;
    public GameObject newHighscore;

    public GameObject OuterCanvas;

    public GameObject UI;
    public GameObject Tutorial;
    public GameObject InGame;
    public GameObject EndGame;

    public bool Summarized;
    public GameObject Explosion;

    public Planet_Rotation planetRot;

    public GameObject Camera;
    public GameObject Camera_Center;
    public GameObject Calibrator;
    public GameObject PlanetParent;
    public GameObject Planet;
    public GameObject Shutter;
    public GameObject ShutterModel;

    public float Angle_Cam_Planet;
    public float AngleH_Cam_Shutter;

    public float Original_RotSpeed;
    public float Speedrate;

    public Initial_Adjust init;
    // Start is called before the first frame update
    void Start()
    {
        Original_RotSpeed = planetRot.Self_Rotate_Speed;
        planetRot.Self_Rotate = false;


        if (OuterCanvas == null && GameObject.Find("/Canvas"))
        {
            OuterCanvas = GameObject.Find("/Canvas");
        }

        if (PlayerPrefs.HasKey("Mars_Record"))
        {
            timeHighscore_UI.text = "BEST RECORD:\n" + (string)(PlayerPrefs.GetFloat("Mars_Record") < 3600 ? (((int)PlayerPrefs.GetFloat("Mars_Record")) / 60).ToString("00") + ":" + (((int)PlayerPrefs.GetFloat("Mars_Record")) % 60).ToString("00") + "." + (PlayerPrefs.GetFloat("Mars_Record") % 1 * 100).ToString("00.") : (((int)PlayerPrefs.GetFloat("Mars_Record")) / 3600).ToString("00") + ":" + ((((int)PlayerPrefs.GetFloat("Mars_Record")) / 60) % 60).ToString("00") + ":" + (((int)PlayerPrefs.GetFloat("Mars_Record")) % 60).ToString("00") + "." + (PlayerPrefs.GetFloat("Mars_Record") % 1 * 100).ToString("00."));
        }
        else
        {
            timeHighscore_UI.text = "";
        }
        newHighscore.SetActive(false);

        Tutorial.SetActive(false);
        InGame.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (!manager.lose && manager.start)
        {
            survivedDuration += 0.25f * Time.deltaTime;
        }

        if (manager.lose && !Summarized)
        {
            GameEnd();
        }

        if (survivedDuration < 3600)
        {
            time.text = "Time survived\n" + (((int)survivedDuration) / 60).ToString("00") + ":" + (((int)survivedDuration) % 60).ToString("00") + "." + (survivedDuration % 1 * 100).ToString("00.");
        }
        else
        {
            time.text = "Time survived\n" + (((int)survivedDuration) / 3600).ToString("00") + ":"+ ((((int)survivedDuration) /60) % 60).ToString("00") + ":" + (((int)survivedDuration) % 60).ToString("00") + "." + (survivedDuration % 1 * 100).ToString("00.");
        }

        if (Camera == null)
        {
            if (GameObject.Find("AR Camera"))
            {
                Camera = GameObject.Find("AR Camera");
            }
        }

        if (Calibrator == null)
        {
            if (GameObject.Find("Bearing AR Cam Pos"))
            {
                Calibrator = GameObject.Find("Bearing AR Cam Pos");
            }
        }

        Camera_Center.transform.position = new Vector3(ShutterModel.transform.position.x, Camera.transform.position.y, ShutterModel.transform.position.z);


        AngleH_Cam_Shutter = Vector3.SignedAngle(Calibrator.transform.position - Planet.transform.position, PlanetParent.transform.forward, Vector3.up);

        //ShutterModel.transform.localEulerAngles = new Vector3(0, 0, AngleH_Cam_Shutter < 0 ? 90 : -90);
        Shutter.transform.localEulerAngles = new Vector3(-Angle_Cam_Planet, -AngleH_Cam_Shutter, 0);

        if (GameObject.Find("Meteorite_Pos"))
        {
            manager = GameObject.Find("Meteorite_Pos").GetComponent<MeteorManager>();
        }

        if (manager.start)
        {
            Angle_Cam_Planet = Vector3.SignedAngle(Calibrator.transform.position - Planet.transform.position, Camera_Center.transform.position - Planet.transform.position, Vector3.zero) * (Camera_Center.transform.position.y > Planet.transform.position.y ? 1 : -1);

            Angle_Cam_Planet = Mathf.Clamp(Angle_Cam_Planet, -20, 20);

            planetRot.Self_Rotate_Speed += Time.deltaTime * Speedrate;
        }
    }
    public void StartGame()
    {
        init.ON = false;
        manager.start = true;
        planetRot.Self_Rotate = true;
        UI.SetActive(false);
        InGame.SetActive(true);
        EndGame.SetActive(false);
    }

    public void _Tutorial()
    {
        Tutorial.SetActive(true);
    }

    public void BackMenu()
    {
        OuterCanvas.GetComponent<Select_Stage>().BackToPrevious();
    }

    public void Restart()
    {
        OuterCanvas.GetComponent<Select_Stage>().CurrentPlanet_Code = 4;
        OuterCanvas.GetComponent<Select_Stage>().EnterStage();
    }
    public void Stay()
    {
        UI.SetActive(false);
        InGame.SetActive(false);
        EndGame.SetActive(false);
    }

    public void GameEnd()
    {
        planetRot.Self_Rotate_Speed = Original_RotSpeed;
        if (PlayerPrefs.HasKey("Mars_Record"))
        {
            if (survivedDuration > PlayerPrefs.GetFloat("Mars_Record"))
            {
                newHighscore.SetActive(true);
                PlayerPrefs.SetFloat("Mars_Record", survivedDuration);
            }
        }
        else
        {
            PlayerPrefs.SetFloat("Mars_Record", survivedDuration);
        }

        timeCleared.text = survivedDuration < 3600 ? (((int)survivedDuration) / 60).ToString("00") + ":" + (((int)survivedDuration) % 60).ToString("00") + "." + (survivedDuration % 1 * 100).ToString("00.") : (((int)survivedDuration) / 3600).ToString("00") + ":" + ((((int)survivedDuration) / 60) % 60).ToString("00") + ":" + (((int)survivedDuration) % 60).ToString("00") + "." + (survivedDuration % 1 * 100).ToString("00.");

        timeHighscore.text = PlayerPrefs.GetFloat("Mars_Record") < 3600 ? (((int)PlayerPrefs.GetFloat("Mars_Record")) / 60).ToString("00") + ":" + (((int)PlayerPrefs.GetFloat("Mars_Record")) % 60).ToString("00") + "." + (PlayerPrefs.GetFloat("Mars_Record") % 1 * 100).ToString("00.") : (((int)PlayerPrefs.GetFloat("Mars_Record")) / 3600).ToString("00") + ":" + ((((int)PlayerPrefs.GetFloat("Mars_Record")) / 60) % 60).ToString("00") + ":" + (((int)PlayerPrefs.GetFloat("Mars_Record")) % 60).ToString("00") + "." + (PlayerPrefs.GetFloat("Mars_Record") % 1 * 100).ToString("00.");

        UI.SetActive(false);
        InGame.SetActive(false);
        EndGame.SetActive(true);
        Summarized = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (manager.start && !manager.lose)
        {
            if (other.tag == "Meteorite")
            {
                for (int i = 0; i < manager.MeteorStripe.Length; i++)
                {
                    GameObject.Destroy(manager.MeteorStripe[i].gameObject);
                }
                this.GetComponent<MeshRenderer>().enabled = false;
                Instantiate(Explosion, this.transform.position, this.transform.rotation);
                manager.lose = true;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (!manager.start)
        {
            if (other.tag == "Meteorite")
            {
                GameObject.Destroy(other.gameObject);
            }
        }
    }
}
