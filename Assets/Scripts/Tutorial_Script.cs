using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial_Script : MonoBehaviour
{
    public GameObject Guide;
    public GameObject BoxGuide;
    public GameObject CircleGuide;
    public Animate_2DSprite anim;

    public TextMeshProUGUI Instruction;
    public TextMeshProUGUI Instruction_2;
    public TextMeshProUGUI TapAndContinue;

    public Sprite[] SwipeUp;
    public Sprite[] SwipeDown;
    public Sprite[] SwipeLeft;
    public Sprite[] SwipeRight;
    public Sprite[] Tap;

    public Camera cam;
    public GameObject Target;
    public GameObject BoxTarget;
    public GameObject CircleTarget;

    public bool Guide_TapWorld;
    public bool Guide_TapWorld_Done;
    public GameObject Blocker_TapWorld;

    public bool Guide_TapAboutWorld;
    public bool Guide_TapAboutWorld_Done;
    public GameObject Blocker_TapAboutWorld;

    public bool Guide_TapEnterWorld;
    public bool Guide_TapEnterWorld_Done;
    public GameObject Blocker_TapEnterWorld;

    public bool Guide_Unlockable;
    public bool Guide_Unlockable_Done;
    public GameObject Blocker_Guide_Unlockable;

    public bool Guide_StartGame;
    public bool Guide_StartGame_Done;
    public GameObject Blocker_Guide_StartGame;

    public GameObject Blocker_Tutorial_Full;

    public bool Tutorial_Start;
    public bool Tutorial_Start_Done;
    
    public bool Tutorial_Fuel;
    public bool Tutorial_Fuel_Done;
    public GameObject Blocker_Tutorial_Fuel;
    public GameObject FuelGuide;

    public bool Tutorial_Move_1;
    public bool Tutorial_Move_1_Done;

    public bool Tutorial_Move_2;
    public bool Tutorial_Move_2_Done;

    public bool Tutorial_Move_3;
    public bool Tutorial_Move_3_Done;

    public bool Tutorial_Move_4;
    public bool Tutorial_Move_4_Done;

    public bool Tutorial_Move_5;
    public bool Tutorial_Move_5_Done;

    public bool Tutorial_Move_6;
    public bool Tutorial_Move_6_Done;

    public bool Tutorial_Move_7;
    public bool Tutorial_Move_7_Done;

    // Start is called before the first frame update
    void Start()
    {
        Blocker_TapAboutWorld.SetActive(false);
        FuelGuide.SetActive(false);
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

        if (CircleTarget == null)
        {
            CircleGuide.SetActive(false);
        }
        else
        {
            CircleGuide.SetActive(true);
        }

        if (Guide_TapWorld && !Guide_TapWorld_Done)
        {
            //Select Planet
            Blocker_TapWorld.SetActive(true);
            Instruction.text = "WELCOME TO SOLIBERATE";
            anim.Frames = Tap;
            Guide.GetComponent<RectTransform>().position = cam.WorldToScreenPoint(Target.gameObject.transform.position);
        }

        if (Guide_TapAboutWorld && !Guide_TapAboutWorld_Done)
        {
            //About Planet
            Blocker_TapWorld.SetActive(false);
            Blocker_TapAboutWorld.SetActive(true);
            Instruction.text = "TAP HERE TO LOOK AT THE DETIALS ABOUT THE PLANET";
            anim.Frames = Tap;
            Guide.GetComponent<RectTransform>().position = Target.GetComponent<RectTransform>().position;
        }

        if (Guide_TapEnterWorld && !Guide_TapEnterWorld_Done)
        {
            //View Planet
            Blocker_TapAboutWorld.SetActive(false);
            Blocker_TapEnterWorld.SetActive(true);
            Instruction.text = "TAP HERE TO HAVE A BETTER VIEW OF THE PLANET";
            anim.Frames = Tap;
            Guide.GetComponent<RectTransform>().position = Target.GetComponent<RectTransform>().position;
        }

        if (Guide_Unlockable && !Guide_Unlockable_Done)
        {
            //Unlockable Planet
            Blocker_TapEnterWorld.SetActive(false);
            Blocker_Guide_Unlockable.SetActive(true);
            Instruction.text = "\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\nTHESE ARE SOME UNLOCKABLE TRIVIAL ABOUT THE PLANET.\n\nYOU CAN UNLOCK THEM WHILE TRAVELING ON THE PLANET";
            TapAndContinue.text = "-TAP AND CONTINUE-";
            BoxGuide.GetComponent<RectTransform>().position = BoxTarget.GetComponent<RectTransform>().position;

        }

        if (Guide_StartGame && !Guide_StartGame_Done)
        {
            //Start Planet
            Blocker_Guide_Unlockable.SetActive(false);
            Blocker_Guide_StartGame.SetActive(true);

            Instruction.text = "LET'S START THE MISSION";
            TapAndContinue.text = "";
            anim.Frames = Tap;
            Guide.GetComponent<RectTransform>().position = Target.GetComponent<RectTransform>().position;
        }

        if (Tutorial_Start && !Tutorial_Start_Done)
        {
            Instruction.text = "";
            Instruction_2.text = "IN THIS MISSION WE WILL TRAVEL THE EARTH WITH THE SPACECRAFT";
            Blocker_Guide_StartGame.SetActive(false);
            Blocker_Tutorial_Full.SetActive(true);

            CircleGuide.GetComponent<RectTransform>().position = cam.WorldToScreenPoint(CircleTarget.gameObject.transform.position);
            TapAndContinue.text = "-TAP AND CONTINUE-";
        }

        if (!Tutorial_Fuel_Done && Tutorial_Fuel)
        {
            //FUEL
            Blocker_Tutorial_Full.SetActive(true);
            Instruction_2.text = "THE SPACECRAFT HAVE LIMITED FUEL\n\nSO PLAN YOUR TRAVEL BEFORE MOVING";
            FuelGuide.SetActive(true);
            TapAndContinue.text = "-TAP AND CONTINUE-";
        }

        if (!Tutorial_Move_1_Done && Tutorial_Move_1)
        {
            //SWIPE
            FuelGuide.SetActive(false);
            Instruction_2.text = "YOU CAN MOVE THE SPACECRAFT BY SWIPING THE SCREEN";
            TapAndContinue.text = "-TAP AND CONTINUE-";
            Blocker_Tutorial_Full.SetActive(false);
        }

        if (!Tutorial_Move_2_Done && Tutorial_Move_2)
        {
            //SWIPE
            Instruction_2.text = "THE SPACECRAFT WILL MOVE BASE ON THE DIRECTION YOU FACING IT";
            TapAndContinue.text = "-TAP AND CONTINUE-";
        }

        if (!Tutorial_Move_3_Done && Tutorial_Move_3)
        {
            //SWIPE
            Instruction_2.text = "YOU CAN LOOK AT THE INDICATOR TO KNOW WHICH DIRECTION IT WILL MOVE TO WHEN SWIPING";
            TapAndContinue.text = "-TAP AND CONTINUE-";
            Blocker_Tutorial_Full.SetActive(true);
        }

        if (!Tutorial_Move_4_Done && Tutorial_Move_4)
        {
            //SWIPE
            Instruction_2.text = "PASSING THE SPACE STATION WILL SWITCH THEM BETWEEN RED AND GREEN";
            TapAndContinue.text = "-TAP AND CONTINUE-";
        }

        if (!Tutorial_Move_5_Done && Tutorial_Move_5)
        {
            //SWIPE
            Instruction_2.text = "TURNING ALL SPACE STATION GREEN WILL ACTIVATE THE DYSON SCANNER";
            TapAndContinue.text = "-TAP AND CONTINUE-";
        }

        if (!Tutorial_Move_6_Done && Tutorial_Move_6)
        {
            //SWIPE
            Instruction_2.text = "THIS WILL HELP US TO UNDERSTAND MORE ABOUT THE PLANET WE TRAVELLED";
            TapAndContinue.text = "-TAP AND CONTINUE-";
        }

        if (!Tutorial_Move_7_Done && Tutorial_Move_7)
        {
            //SWIPE
            Instruction_2.text = "GOOD LUCK AND GODSPEED";
            TapAndContinue.text = "-TAP AND CONTINUE-";
        }

        if (Tutorial_Move_7_Done)
        {
            Blocker_Tutorial_Full.SetActive(false);
            Instruction_2.text = "";
            TapAndContinue.text = "";
        }
    }
}
