using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Animate_2DSprite : MonoBehaviour
{
    public bool Loop;
    public int frame = 0;
    float SecReset;
    public Image Renderer;
    public Sprite[] Frames;
    public float Delay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SecReset += Time.deltaTime;

        if (frame < Frames.Length && SecReset >= Delay)
        {
            SecReset = 0;

            if (frame < Frames.Length-1)
            {
                frame++;
                Renderer.sprite = Frames[frame];
            }
            else if (frame + 1 >= Frames.Length && Loop)
            {
                frame = 0;
                Renderer.sprite = Frames[frame];
            }

        }

    }
}
