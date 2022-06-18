using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTap : MonoBehaviour
{
    bool InGame;
    bool Tapped;

    public Select_Stage stageCheck;
    public GameObject visualTap_Prefab;
    public Transform visualTap_Parent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount == 0)
        {
            Tapped = false;
        }

        if (Input.touchCount > 0)
        {
            if (!Tapped && !InGame)
            {
                SpawnPress();
                if (stageCheck.InStage)
                {
                    GameObject.Find("Audio/SFX/Tap").GetComponent<AudioSource>().volume = 0.15f;
                }
                else
                {
                    GameObject.Find("Audio/SFX/Tap").GetComponent<AudioSource>().volume = 1.0f;
                }
                PlayTap();

                Tapped = true;
            }
        }

    }

    public void SpawnPress()
    {
        GameObject tap = Instantiate(visualTap_Prefab, visualTap_Parent);
        tap.GetComponent<RectTransform>().position = Input.GetTouch(0).position;
    }

    public void PlayTap()
    {
        GameObject.Find("Audio/SFX/Tap").GetComponent<AudioSource>().Play();
    }
}
