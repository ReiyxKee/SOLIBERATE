using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Voice_Control : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider Setting_BGM;
    public Slider Pause_BGM;

    public Slider Setting_SFX;
    public Slider Pause_SFX;
    
      // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("BGM"))
        {
            float BGM = PlayerPrefs.GetFloat("BGM");
            float BGM_Slider_Range = Setting_BGM.maxValue - Setting_BGM.minValue;

            audioMixer.SetFloat("BGM_Volume", BGM == 0 ? -80 : (1 - (BGM / BGM_Slider_Range)) * -40);
            Pause_BGM.value = BGM;
            Setting_BGM.value = BGM;

        }
        else
        {
            PlayerPrefs.SetFloat("BGM", 0.5f);
        }

        if (PlayerPrefs.HasKey("SFX"))
        {
            float SFX = PlayerPrefs.GetFloat("SFX");
            float SFX_Slider_Range = Setting_SFX.maxValue - Setting_SFX.minValue;

            audioMixer.SetFloat("SFX_Volume", SFX == 0 ? -80 : (1 - (SFX / SFX_Slider_Range)) * -40);
            Pause_SFX.value = SFX;
            Setting_SFX.value = SFX;
        }
        else
        {
            PlayerPrefs.SetFloat("SFX", 0.75f);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void VolumeChange_Setting()
    {
        float BGM = Setting_BGM.value;
        float SFX = Setting_SFX.value;
        float BGM_Slider_Range = Setting_BGM.maxValue - Setting_BGM.minValue;
        float SFX_Slider_Range = Setting_SFX.maxValue - Setting_SFX.minValue;

        audioMixer.SetFloat("BGM_Volume", BGM == 0 ? -80 : (1-(BGM / BGM_Slider_Range)) * -40);
        Pause_BGM.value = BGM;


        audioMixer.SetFloat("SFX_Volume", SFX == 0 ? -80 : (1 - (SFX / SFX_Slider_Range)) * -40);
        Pause_SFX.value = SFX;

        PlayerPrefs.SetFloat("BGM", BGM);
        PlayerPrefs.SetFloat("SFX", SFX);
    }

    public void VolumeChange_Pause()
    {
        float BGM = Pause_BGM.value;
        float SFX = Pause_SFX.value;
        float BGM_Slider_Range = Pause_BGM.maxValue - Pause_BGM.minValue;
        float SFX_Slider_Range = Pause_SFX.maxValue - Pause_SFX.minValue;

        audioMixer.SetFloat("BGM_Volume", BGM == 0 ? -80 : (1 - (BGM / BGM_Slider_Range)) * -40);
        Setting_BGM.value = BGM;


        audioMixer.SetFloat("SFX_Volume", SFX == 0 ? -80 : (1 - (SFX / SFX_Slider_Range)) * -40);
        Setting_SFX.value = SFX;

        PlayerPrefs.SetFloat("BGM", BGM);
        PlayerPrefs.SetFloat("SFX", SFX);
    }
}
