using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Move : MonoBehaviour
{
    public TextMeshProUGUI DebugWindow;

    public bool Init = false;

    public bool isRolling;
    public float rotationSpeed = 15.0f;

    public int Steps;

    public bool isReset;
    public bool isPause;


    public GameObject[] CubeList;

    public Vector3[] Pos_Init;
    public Quaternion[] Rot_Init;

    public Vector3[] intPos;
    public Quaternion[] intRotation;



    public Bounds bound;
    public Vector3 left;
    public Vector3 right;
    public Vector3 up;
    public Vector3 down;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < CubeList.Length;i++)
        {
            if(CubeList[i].GetComponent<Cube_Movement>().Reset)
            {
                _Reset();
            }
        }

        if (!Init)
        {
            _Register();

            DebugWindow.text += "\n" + "Setting Bounds";

            bound = CubeList[0].GetComponent<BoxCollider>().bounds;
            left = new Vector3(-bound.size.x / 2, -bound.size.y / 2, 0);
            right = new Vector3(bound.size.x / 2, -bound.size.y / 2, 0);
            up = new Vector3(0, -bound.size.y / 2, bound.size.z / 2);
            down = new Vector3(0, -bound.size.y / 2, -bound.size.z / 2);

            DebugWindow.text += "\n" + "Set Bounds Complete";

            DebugWindow.text += "\n" + "Rotate Speed = " + rotationSpeed.ToString();
            DebugWindow.text += "\n" + "Spawn Init Complete";
            Init = true;
        }

    }

    public void _Register()
    {
        Rot_Init = new Quaternion[CubeList.Length];
        Pos_Init = new Vector3[CubeList.Length];

        for (int i = 0; i < CubeList.Length; i++)
        {
            Rot_Init[i] = CubeList[i].gameObject.transform.rotation;
            Pos_Init[i] = CubeList[i].gameObject.transform.position;
        }
    }

    public void _Reset()
    {
        for (int i = 0; i < CubeList.Length; i++)
        {
            CubeList[i].GetComponent<Cube_Movement>().StopAllCoroutines();
            CubeList[i].gameObject.transform.rotation = Rot_Init[i];
            CubeList[i].gameObject.transform.position = Pos_Init[i];
            CubeList[i].GetComponent<Cube_Movement>().Reset = false;
            CubeList[i].GetComponent<Cube_Movement>().Rolling = false;
            CubeList[i].GetComponent<Cube_Movement>().Delay = 0;
        }
    }


    public void Move_Up()
    {
        if (!isRolling)
        {
            for (int i = 0; i < CubeList.Length; i++)
            {
                CubeList[i].GetComponent<Cube_Movement>().rotationSpeed = rotationSpeed;
                CubeList[i].GetComponent<Cube_Movement>().MakeItRoll(up);
            }
            DebugWindow.text += "\n" + "Up";
        }
    }

    public void Move_Down()
    {
        if (!isRolling)
        {
            for (int i = 0; i < CubeList.Length; i++)
            {
                CubeList[i].GetComponent<Cube_Movement>().rotationSpeed = rotationSpeed;
                CubeList[i].GetComponent<Cube_Movement>().MakeItRoll(down);
            }
            DebugWindow.text += "\n" + "Down";
        }
    }

    public void Move_Left()
    {
        if (!isRolling)
        {
            for (int i = 0; i < CubeList.Length; i++)
            {
                CubeList[i].GetComponent<Cube_Movement>().rotationSpeed = rotationSpeed;
                CubeList[i].GetComponent<Cube_Movement>().MakeItRoll(left);
            }
            DebugWindow.text += "\n" + "Left";
        }
    }

    public void Move_Right()
    {
        if (!isRolling)
        {
            for (int i = 0; i < CubeList.Length; i++)
            {
                CubeList[i].GetComponent<Cube_Movement>().rotationSpeed = rotationSpeed;
                CubeList[i].GetComponent<Cube_Movement>().MakeItRoll(right);
            }
            DebugWindow.text += "\n" + "Right";
        }
    }
}