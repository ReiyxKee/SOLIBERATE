using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temperature_Manager : MonoBehaviour
{
    public Slider slider_L;
    public Slider slider_R;

    public Slider Progress;

    public Handle _slider_L;
    public Handle _slider_R;

    public Material material;

    public float current_Total;

    public bool win;
    public bool lost;
    public float failCounter;
    public float progress;

    public bool hot;
    public bool Started;
    public bool progressed;


    public AudioSource Hot;
    public AudioSource Cold;
    // Start is called before the first frame update
    void Start()
    {
        material.color = new Color32(255, 0, 0, 255);
    }

    // Update is called once per frame
    void Update()
    {

        if (Started && !win && !lost)
        {
            Progress.value = progress;
            current_Total = slider_L.value + slider_R.value;

            if (Cold.volume >= 1)
            {
                Cold.volume = 1;
            }
            if (Cold.volume <= 0)
            {
                Cold.volume = 0;
            }

            if (Hot.volume >= 1)
            {
                Hot.volume = 1;
            }
            if (Hot.volume <= 0)
            {
                Hot.volume = 0;
            }


            if (progress <= 0)
            { 
                if (progressed)
                {
                    failCounter += Time.deltaTime;

                    if (failCounter >= 5.0)
                    {
                        lost = true;
                        _slider_L.start = false;
                        _slider_R.start = false;
                    }
                }
            }
            else
            {
                if (!progressed)
                {
                    progressed = true;
                }

                if(progress >= 100)
                {
                    win = true;
                    _slider_L.start = false;
                    _slider_R.start = false;
                }

                failCounter -= Time.deltaTime;
            }

            if (failCounter > 5.0)
            {
                failCounter = 5.0f;
            }

            if (failCounter < 0.0)
            {
                failCounter = 0.0f;
            }

            if (progress > 100)
            {
                progress = 100;
            }

            if (progress < 0)
            {
                progress = 0;
            }

            if (_slider_L.slide_range.value - 5 < slider_L.value && slider_L.value < _slider_L.slide_range.value + 5)
            {
                progress += Time.deltaTime * 5.0f;
            }
            else
            {
                progress -= Time.deltaTime * 7.5f;
            }

            if (_slider_R.slide_range.value - 5 < slider_R.value && slider_R.value < _slider_R.slide_range.value + 5)
            {
                progress += Time.deltaTime * 5.0f;
            }
            else
            {
                progress -= Time.deltaTime * 7.5f;
            }

            if (current_Total < 100 - (_slider_L.slide_range.value + _slider_R.slide_range.value) - 5)
            {
                if (Hot.volume > 0)
                {
                    Hot.volume -= Time.deltaTime;
                }

                if (Cold.volume < 1)
                {
                    Cold.volume += 2*Time.deltaTime;
                }

                float ratio = (255 * current_Total / (200 - _slider_L.slide_range.value - _slider_R.slide_range.value));
                material.color = new Color32((byte)ratio, 255, 255, 255);
                hot = false;
            }
            else if (current_Total < 100 - (_slider_L.slide_range.value + _slider_R.slide_range.value) + 5 && current_Total > 100 - (_slider_L.slide_range.value + _slider_R.slide_range.value) - 5)
            {
                if (Hot.volume > 0)
                {
                    Hot.volume -= Time.deltaTime;
                }

                if (Cold.volume > 0)
                {
                    Cold.volume -= Time.deltaTime;
                }

                material.color = new Color32(255, 255, 255, 255);
            }
            else if (current_Total > 100 - (_slider_L.slide_range.value + _slider_R.slide_range.value) + 5)
            {
                if (Hot.volume < 1)
                {
                    Hot.volume += Time.deltaTime;
                }

                if (Cold.volume > 0)
                {
                    Cold.volume -= Time.deltaTime;
                }


                float ratio = 255 - (255 * current_Total / (_slider_L.slide_range.value + _slider_R.slide_range.value));
                material.color = new Color32(255, (byte)ratio, (byte)ratio, 255);
                hot = true;
            }
        }
    }

    public void StartGame()
    {
        _slider_L.start = true;
        _slider_R.start = true;
        _slider_L.slide_range.value = Random.Range(15, 35);
        _slider_R.slide_range.value = 50-_slider_L.slide_range.value;

        Started = true;
    }
}
