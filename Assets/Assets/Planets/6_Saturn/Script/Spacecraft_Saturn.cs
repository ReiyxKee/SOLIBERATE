using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spacecraft_Saturn : MonoBehaviour
{
    public RingManager manager;

    public float Score;

    public bool Started;
    public bool Ended;

    public TextMeshProUGUI score;

    //public TextMeshProUGUI timeCleared;
    public TextMeshProUGUI Highscore_UI;
    public TextMeshProUGUI Highscore;
    public TextMeshProUGUI EndGameScore;
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

    // Start is called before the first frame update
    void Start()
    {
        Original_RotSpeed = planetRot.Self_Rotate_Speed;
        planetRot.Self_Rotate = false;


        if (OuterCanvas == null && GameObject.Find("/Canvas"))
        {
            OuterCanvas = GameObject.Find("/Canvas");
        }

        if (PlayerPrefs.HasKey("Saturn_Record"))
        {
            Highscore_UI.text = "HIGHSCORE:\n"+PlayerPrefs.GetFloat("Saturn_Record").ToString("0");
        }
        else
        {
            Highscore_UI.text = "";
        }
        newHighscore.SetActive(false);

        Tutorial.SetActive(false);
        InGame.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (!Ended && Started)
        {
            Score += Time.deltaTime * 10;
        }

        if (Ended && !Summarized)
        {
            GameEnd();
        }

        score.text = "Score: " + Score.ToString("0");

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

        Angle_Cam_Planet = Vector3.SignedAngle(Calibrator.transform.position - Planet.transform.position, Camera_Center.transform.position - Planet.transform.position, Vector3.up) * ((Camera_Center.transform.position.y - Calibrator.transform.position.y) < 0 ? 1 : -1);

        Angle_Cam_Planet = Mathf.Clamp(Angle_Cam_Planet, -20, 20);

        AngleH_Cam_Shutter = Vector3.SignedAngle(Calibrator.transform.position - Planet.transform.position, PlanetParent.transform.forward, Vector3.up);

        //ShutterModel.transform.localEulerAngles = new Vector3(0, 0, AngleH_Cam_Shutter < 0 ? 90 : -90);
        Shutter.transform.localEulerAngles = new Vector3(-Angle_Cam_Planet, -AngleH_Cam_Shutter, 0);

        if (Started)
        {
            planetRot.Self_Rotate_Speed += Time.deltaTime * Speedrate;
        }
    }
    public void StartGame()
    {
        Started = true;
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
        OuterCanvas.GetComponent<Select_Stage>().CurrentPlanet_Code = 6;
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

        if (PlayerPrefs.HasKey("Saturn_Record"))
        {
            if (Score > PlayerPrefs.GetFloat("Saturn_Record"))
            {
                newHighscore.SetActive(true);
                PlayerPrefs.SetFloat("Saturn_Record", Score);
            }
        }
        else
        {
            PlayerPrefs.SetFloat("Saturn_Record", Score);
        }

        EndGameScore.text = Score.ToString("0");
        Highscore.text = PlayerPrefs.GetFloat("Saturn_Record").ToString("0");

        UI.SetActive(false);
        InGame.SetActive(false);
        EndGame.SetActive(true);
        Summarized = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Started && !Ended)
        {
            if (other.tag == "Ring")
            {
                GameObject.Destroy(other.gameObject);
                Score += 100;
            }

            if (other.tag == "Meteorite")
            {
                for (int i = 0; i < manager.stripes.Length; i++)
                {
                    GameObject.Destroy(manager.stripes[i].gameObject);
                }
                this.GetComponent<MeshRenderer>().enabled = false;
                Instantiate(Explosion, this.transform.position, this.transform.rotation);
                Ended = true;
            }
        }
    }
}
