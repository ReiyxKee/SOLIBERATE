using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{

    public GameObject OuterCanvas;

    public int score;
    public int HP = 100;

    public ShootLaser shoot;
    public Spawn spawn;

    public Slider EarthHP;

    public GameObject tutorial;
    public GameObject PrevHighScore;
    public GameObject CurrentScore;

    public TextMeshProUGUI PrevHighscore;
    public TextMeshProUGUI currentscore;


    public int CurrentMeteorite;
    public TextMeshProUGUI textCurrentMeteorite;

    public GameObject UIPreGame;
    public GameObject UIInGame;
    public GameObject UIEndGame;
    public TextMeshProUGUI EndGame_Text;
    public TextMeshProUGUI EndGame_Score;
    public TextMeshProUGUI EndGame_Accuracy;
    public TextMeshProUGUI EndGame_Highscore;
    public GameObject EndGame_NewHighscore;

    public Button viewPlanet;

    public bool Summary;


    public GameObject Parent;
    
    // Start is called before the first frame update
    void Start()
    {
        CurrentMeteorite = spawn.maxSpawn;
        UIPreGame.SetActive(true);
        UIInGame.SetActive(true);
        UIEndGame.SetActive(false);
        if (PlayerPrefs.HasKey("Earth_Record"))
        {
            PrevHighScore.SetActive(true);
            PrevHighscore.text = "best record:\n" + PlayerPrefs.GetFloat("Earth_Record").ToString();
        }
        else
        {
            PrevHighScore.SetActive(false);
            PrevHighscore.text = "";
        }
        HP = 100;
        CurrentScore.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (OuterCanvas == null && GameObject.Find("/Canvas"))
        {
            OuterCanvas = GameObject.Find("/Canvas");
        }

        textCurrentMeteorite.text = "Meteorite: " + CurrentMeteorite.ToString() + " / " + spawn.maxSpawn;

        score = shoot.Hits * 100;
        EarthHP.value = HP;
        currentscore.text = score.ToString();

        if (HP <= 0)
        {
            LoseGame();
        }
        else
        {
            if (Parent.transform.childCount <= 0 && spawn.Spawn_Limit <=0)
            {
                WinGame();
            }
        }
    }

    public void Tutorial()
    {
        tutorial.SetActive(true);
    }
    public void StartGame()
    {
        UIPreGame.SetActive(false);
        UIInGame.SetActive(true);
        UIEndGame.SetActive(false);
        PrevHighScore.SetActive(false);
        CurrentScore.SetActive(true);
        spawn.Started = true;
    }
    public void LoseGame()
    {
        if (!Summary)
        {
            foreach(Transform Child in Parent.transform)
            {
                GameObject.Destroy(Child.gameObject);
            }
            viewPlanet.interactable = false;
            UIPreGame.SetActive(false);
            UIInGame.SetActive(false);
            UIEndGame.SetActive(true);
            EndGame_Text.text = "WE COULDN'T MAKE IT\nTHERE'S TOO MANY METEORS";
            spawn.Started = false;
            CalcScore();
            CheckHighscore();
            EndGame_Score.text = score.ToString();
            EndGame_Accuracy.text = (shoot.Accuracy * 100).ToString("0.00") + " %";
            EndGame_Highscore.text = PlayerPrefs.GetFloat("Earth_Record").ToString();
            Summary = true;
        }
    }

    public void WinGame()
    {
        if (!Summary)
        {
            UIPreGame.SetActive(false);
            UIInGame.SetActive(false);
            UIEndGame.SetActive(true);
            EndGame_Text.text = "WE SUCCESS PROTECTED OUR EARTH";
            spawn.Started = false;
            CalcScore();
            CheckHighscore();
            EndGame_Score.text = score.ToString();
            EndGame_Accuracy.text = (shoot.Accuracy * 100).ToString("0.00") + " %";
            EndGame_Highscore.text = PlayerPrefs.GetFloat("Earth_Record").ToString();
            Summary = true;
        }
    }

    public void BackMenu()
    {
        OuterCanvas.GetComponent<Select_Stage>().BackToPrevious();
    }

    public void Restart()
    {
        OuterCanvas.GetComponent<Select_Stage>().CurrentPlanet_Code = 3;
        OuterCanvas.GetComponent<Select_Stage>().EnterStage();
    }
    public void Stay()
    {
        UIPreGame.SetActive(false);
        UIInGame.SetActive(false);
        UIEndGame.SetActive(false);
    }

    public void CalcScore()
    {
           score = (int)((float)score * (1 + shoot.Accuracy));
    }

    public void CheckHighscore()
    {
        if (!PlayerPrefs.HasKey("Earth_Record"))
        {
            EndGame_NewHighscore.SetActive(true);
            PlayerPrefs.SetFloat("Earth_Record", score);
        }
        else
        {
            if (score > PlayerPrefs.GetFloat("Earth_Record"))
            {
                EndGame_NewHighscore.SetActive(true);
                PlayerPrefs.SetFloat("Earth_Record", score);
            }
            else
            {
                EndGame_NewHighscore.SetActive(false);
            }
        }


    }
}
