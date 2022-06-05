using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Jupiter_UI : MonoBehaviour
{
    public TextMeshProUGUI time_left;
    public SL9 sl9;

    public float maxTime;
    public float timeLeft;

    public targeting_SL9 target;

    public GameObject OuterCanvas;

    public GameObject UI;
    public GameObject Tutorial;
    public GameObject InGame;
    public GameObject EndGame;

    public bool Ended;

    public TextMeshProUGUI EndGame_title;
    public TextMeshProUGUI EndGame_favor;
    // Start is called before the first frame update
    void Start()
    {
        if (OuterCanvas == null && GameObject.Find("/Canvas"))
        {
            OuterCanvas = GameObject.Find("/Canvas");
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (timeLeft < 0)
        {
            timeLeft = 0;
        }

        if (sl9.Hit)
        {
            timeLeft = 0;
            sl9.CD = false;
        }

        if (sl9.HP <= 0)
        {
            GameEnd();
        }

        if (!Ended)
        {

            if (sl9.Hit)
            {
                timeLeft = 0;
                GameEnd();
            }
            else
            {
                timeLeft = maxTime - sl9.TimeCount;
            }

            if (timeLeft <= 15 && !sl9.inAtmosphere.isPlaying)
            {
                sl9.inAtmosphere.Play();
            }

            if (timeLeft < 30)
            {
                time_left.color = ((int)timeLeft%2 == 0) ? new Color32(255, 0, 0, 255) : new Color32(255, 255, 255, 255);
                target.TargetMarker.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
                target.SideMarker.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color32(255, 0, 0, 100);
            }
            else
            {
                target.TargetMarker.GetComponent<Image>().color = new Color32(0, 255, 249, 255);
                target.SideMarker.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color32(0, 255, 249, 100);
            }
        }
        else
        {
            target.Ended = true;
            target.TargetMarker.SetActive(false);
            target.SideMarker.SetActive(false);
        }


        time_left.text = "TIME LEFT: " + ((int)timeLeft / 60).ToString("0") + ":" + (timeLeft % 60).ToString("00") + "." + ((timeLeft % 1) * 100).ToString("0");
    }

    public void StartGame()
    {
        sl9.Started = true;
        target.Started = true;
        UI.SetActive(false);
    }

    public void GameEnd()
    {
        if (sl9.endGame >= 4.5f || sl9.HP <=0)
        {
            InGame.SetActive(false);
            EndGame.SetActive(true);
            if (sl9.HP > 0)
            {
                EndGame_title.text = "MISSION FAILED";
                EndGame_favor.text = "IF JUPITER WAS VULNERABLE\n\nMAYBE EARTH IS, TOO";
            }
            else
            {
                EndGame_title.text = "MISSION ENDED";
                EndGame_favor.text = "Had the comet hit Earth instead\nit would be a disaster...\n\nlike the meteorite that wiped out dinosaurs 65 million years ago.";
            }

            Ended = true;
        }
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
        OuterCanvas.GetComponent<Select_Stage>().CurrentPlanet_Code = 5;
        OuterCanvas.GetComponent<Select_Stage>().EnterStage();
    }
    public void Stay()
    {
        UI.SetActive(false);
        InGame.SetActive(false);
        EndGame.SetActive(false);
    }

}
