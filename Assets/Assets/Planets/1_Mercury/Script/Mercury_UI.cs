using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Mercury_UI : MonoBehaviour
{
    public GameObject OuterCanvas;

    public bool ResultShown;

    public GameObject UI;
    public GameObject inGame;
    public GameObject endGame;

    public TextMeshProUGUI favourText;

    public Button LookAt;

    public Temperature_Manager temp;
    // Start is called before the first frame update
    void Start()
    {
        if (OuterCanvas == null && GameObject.Find("/Canvas"))
        {
            OuterCanvas = GameObject.Find("/Canvas");
        }

        UI.SetActive(true);
        inGame.SetActive(true);
        endGame.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (temp.win || temp.lost)
        {
            Result();
        }
    }

    public void start()
    {
        UI.SetActive(false);
        inGame.SetActive(true);
        endGame.SetActive(false);
    }

    public void Result()
    {
        if (!ResultShown)
        {
            UI.SetActive(false);
            inGame.SetActive(false);
            endGame.SetActive(true);

            if (temp.win)
            {
                favourText.text = "MERCURY HAD CALMED DOWN\nAND BACK TO ITS USUAL STATE";
            }
            else
            {
                favourText.text = temp.hot ? "IT'S WAY TOO HOT!!!" : "IT'S TOO COLD FOR MERCURY...";
                LookAt.interactable = false;
            }
            ResultShown = true;
        }
    }
    public void BackMenu()
    {
        OuterCanvas.GetComponent<Select_Stage>().BackToPrevious();
    }

    public void Stay()
    {
        UI.SetActive(false);
        inGame.SetActive(false);
        endGame.SetActive(false);
    }
    public void Restart()
    {
        OuterCanvas.GetComponent<Select_Stage>().CurrentPlanet_Code = 1;
        OuterCanvas.GetComponent<Select_Stage>().EnterStage();
    }
}
