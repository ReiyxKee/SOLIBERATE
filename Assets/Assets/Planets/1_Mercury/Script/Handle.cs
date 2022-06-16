using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Handle : MonoBehaviour
{
    public bool start;
    public bool isUp;
    public float Rate;
    public Slider slide;

    public Slider slide_range;

    public Button _Up;
    public Button _Down;

    public Image imageLabel;
    public Sprite isUp_Image;
    public Sprite isDown_Image;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        imageLabel.transform.gameObject.SetActive(start ? true : false);
        imageLabel.sprite = isUp ? isUp_Image : isDown_Image;

        _Up.interactable = !isUp;
        _Down.interactable = isUp;

        if (start)
        {
            slide.value += (isUp ? +1 : -1) * Time.deltaTime * Rate;
        }

    }

    public void Up()
    {
        isUp = true;
    }

    public void Down()
    {
        isUp = false;
    }
}
