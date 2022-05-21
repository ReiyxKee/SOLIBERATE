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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
