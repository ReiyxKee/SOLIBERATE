using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tutorial_Script : MonoBehaviour
{
    public GameObject Guide;
    public GameObject BoxGuide;
    public Animate_2DSprite anim;

    public Sprite[] SwipeUp;
    public Sprite[] SwipeDown;
    public Sprite[] SwipeLeft;
    public Sprite[] SwipeRight;
    public Sprite[] Tap;

    public Camera cam;
    public GameObject Target;
    public GameObject BoxTarget;

    public bool Guide_TapWorld;
    public bool Guide_TapWorld_Done;

    public bool Guide_TapAboutWorld;
    public bool Guide_TapAboutWorld_Done;

    public bool Guide_TapEnterWorld;
    public bool Guide_TapEnterWorld_Done;

    public bool Guide_Unlockable;
    public bool Guide_Unlockable_Done;

    public bool Guide_StartGame;
    public bool Guide_StartGame_Done;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (BoxTarget == null)
        {
            BoxGuide.SetActive(false);
        }
        else
        {
            BoxGuide.SetActive(true);
        }

        if (Target == null)
        {
            Guide.SetActive(false);
        }
        else
        {
            Guide.SetActive(true);
        }

        if (Guide_TapWorld && !Guide_TapWorld_Done)
        {
            //Select Planet
            anim.Frames = Tap;
            Guide.GetComponent<RectTransform>().position = cam.WorldToScreenPoint(Target.gameObject.transform.position);
        }

        if (Guide_TapAboutWorld && !Guide_TapAboutWorld_Done)
        {
            //About Planet
            anim.Frames = Tap;
            Guide.GetComponent<RectTransform>().position = Target.GetComponent<RectTransform>().position;
        }

        if (Guide_TapEnterWorld && !Guide_TapEnterWorld_Done)
        {
            //About Planet
            anim.Frames = Tap;
            Guide.GetComponent<RectTransform>().position = Target.GetComponent<RectTransform>().position;
        }

        if (Guide_Unlockable && !Guide_Unlockable_Done)
        {
            //About Planet
            BoxGuide.GetComponent<RectTransform>().position = BoxTarget.GetComponent<RectTransform>().position;
        }

        if (Guide_StartGame && !Guide_StartGame_Done)
        {
            //About Planet
            anim.Frames = Tap;
            Guide.GetComponent<RectTransform>().position = Target.GetComponent<RectTransform>().position;
        }
    }
}
