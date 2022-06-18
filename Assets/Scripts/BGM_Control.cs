using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Control : MonoBehaviour
{
    public Select_Stage stage;

    public AudioSource MainMenu;
    public AudioSource InGame;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (MainMenu.volume > 1)
        {
            MainMenu.volume = 1;
        }

        if (MainMenu.volume < 0)
        {
            MainMenu.volume = 0;
        }

        if (InGame.volume > 1)
        {
            InGame.volume = 1;
        }

        if (InGame.volume < 0)
        {
            InGame.volume = 0;
        }

        if (stage.InStage)
        {
            if (MainMenu.volume > 0)
            {
                MainMenu.volume -= Time.deltaTime;
            }


            if (InGame.volume < 1)
            {
                InGame.volume += Time.deltaTime;
            }
        }
        else
        {
            if (MainMenu.volume < 1)
            {
                MainMenu.volume += Time.deltaTime;
            }

            if (InGame.volume > 0)
            {
                InGame.volume -= Time.deltaTime;
            }
        }
    }
}
