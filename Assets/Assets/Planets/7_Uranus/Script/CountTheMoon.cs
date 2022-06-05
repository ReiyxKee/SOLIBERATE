using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CountTheMoon : MonoBehaviour
{

    public int Answer;
    public int Solution;
    bool Submit;
    bool Cleared;

    public GameObject UIQuestion;
    public GameObject Description;

    public bool hideUI;

    public TextMeshProUGUI _Ones;
    public TextMeshProUGUI _Tenth;

    public bool Shake;
    public float maxDuration;
    public float Duration;
    public int time;
    public Vector2 OriginalPos;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Uranus_Record"))
        {
            Cleared = PlayerPrefs.GetInt("Uranus_Record") == 1 ? true:false;
        }
        else
        {
            PlayerPrefs.SetInt("Uranus_Record", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        _Ones.text = (Answer % 10).ToString("0"); ;
        _Tenth.text = (Answer / 10).ToString("0"); ; 

        if (Shake)
        {
            Duration += Time.deltaTime;

            if (Duration > time * 0.05f)
            {
                UIQuestion.GetComponent<RectTransform>().position = new Vector2(UIQuestion.GetComponent<RectTransform>().position.x + ((time%2)== 0 ? -1 : 1) * 50, UIQuestion.GetComponent<RectTransform>().position.y);
                time++;
            }

            if (Duration > maxDuration)
            {
                time = 0;
                Shake = false;
                Duration = 0;
                UIQuestion.GetComponent<RectTransform>().position = OriginalPos;
            }

        }
        
        if (Cleared)
        {
            UIQuestion.SetActive(false);
            Description.SetActive(!hideUI);
        }
        else
        {
            UIQuestion.SetActive(true);
            Description.SetActive(false);
        }
    }

    public void Increment(int Num)
    {
        Answer += Num;

        if (Answer > 99)
        {
            Answer -= 100;
        }

    }
    public void Decrement(int Num)
    {
        Answer -= Num;
        
        if (Answer < 0)
        {
            Answer += 100;
        }
    }

    public void SubmitAns()
    {
        if (Answer == Solution)
        {
            GameClear();
        }
        else
        {
            WrongAns();
        }
    }

    public void WrongAns()
    {
        OriginalPos = UIQuestion.GetComponent<RectTransform>().position;
           Shake = true;
        Duration = 0;
        time = 0;
    }

    public void GameClear()
    {
        PlayerPrefs.SetInt("Uranus_Record", 1);
        Cleared = true;
    }

    public void HideUI()
    {
        hideUI = !hideUI;
    }
}
