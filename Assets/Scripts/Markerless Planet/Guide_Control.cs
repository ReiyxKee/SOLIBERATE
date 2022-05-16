using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide_Control : MonoBehaviour
{
    public GameObject Player;
    public GameObject Canvas;
    public GameObject Display;

    public UI ui_Ref;

    public GameObject Top;
    public GameObject XPos;
    public GameObject XNeg;
    public GameObject ZPos;
    public GameObject ZNeg;
    public GameObject Bot;



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (ui_Ref == null)
        {
            ui_Ref = GameObject.Find("/Canvas").gameObject.GetComponent<UI>();
        }

        if (Canvas == null)
        {
            Canvas = GameObject.Find("Canvas");
        }

        if (Top == null)
        {
            Top = GameObject.Find("Turning_Point_Top");
        }

        if (Bot == null)
        {
            Bot = GameObject.Find("Turning_Point_Bot");
        }

        if (XPos == null)
        {
            XPos = GameObject.Find("Turning_Point_XPos");
        }

        if (XNeg == null)
        {
            XNeg = GameObject.Find("Turning_Point_XNeg");
        }

        if (ZPos == null)
        {
            ZPos = GameObject.Find("Turning_Point_ZPos");
        }

        if (ZNeg == null)
        {
            ZNeg = GameObject.Find("Turning_Point_ZNeg");
        }

        if (Canvas.GetComponent<Select_Stage>().Guide_On && (Top != null && Bot != null && XPos != null && XNeg != null && ZPos != null && ZNeg != null))
        {
            Display.SetActive(true);
            Follow();
            Rotation_Logic();
        }
        else
        {
            Display.SetActive(false);
        }

    }

    public void Rotation_Logic()
    {
        if (Top.GetComponent<TurningPoint>().Exist)
        {
            //0 x 90
            float x = 0;

            if (45 > ui_Ref.UIOreintation && ui_Ref.UIOreintation > -45)
            {
                x = 0;
            }
            else if (180 >= ui_Ref.UIOreintation && ui_Ref.UIOreintation > 135 || -135 > ui_Ref.UIOreintation && ui_Ref.UIOreintation >= -180)
            {
                x = 180;
            }
            else if (135 >= ui_Ref.UIOreintation && ui_Ref.UIOreintation >= 45)
            {
                x = 90;
            }
            else if (-45 >= ui_Ref.UIOreintation && ui_Ref.UIOreintation >= -135)
            {
                x = -90;
            }
            
            this.transform.rotation = Quaternion.Euler(0, x, 90);
        }
        else if (Bot.GetComponent<TurningPoint>().Exist)
        {
            //0 x -90


            float x = 0;

            if (45 > ui_Ref.UIOreintation && ui_Ref.UIOreintation > -45)
            {
                x = 0;
            }
            else if (180 >= ui_Ref.UIOreintation && ui_Ref.UIOreintation > 135 || -135 > ui_Ref.UIOreintation && ui_Ref.UIOreintation >= -180)
            {
                x = 180;
            }
            else if (135 >= ui_Ref.UIOreintation && ui_Ref.UIOreintation >= 45)
            {
                x = 90;
            }
            else if (-45 >= ui_Ref.UIOreintation && ui_Ref.UIOreintation >= -135)
            {
                x = -90;
            }

            this.transform.rotation = Quaternion.Euler(0, x, -90);
        }
        else if (XPos.GetComponent<TurningPoint>().Exist)
        {
            //90 0 0
            this.transform.rotation = Quaternion.Euler(90,0,0);
        }
        else if (XNeg.GetComponent<TurningPoint>().Exist)
        {
            //90 180 0
            this.transform.rotation = Quaternion.Euler(90, 180, 0);
        }
        else if (ZPos.GetComponent<TurningPoint>().Exist)
        {
            //90 -90 0
            this.transform.rotation = Quaternion.Euler(90, -90, 0);
        }
        else if (ZNeg.GetComponent<TurningPoint>().Exist)
        {
            //90 90 0
            this.transform.rotation = Quaternion.Euler(90, 90, 0);
        }
    }
    public void Follow()
    {
        this.transform.position = Player.transform.position;
    }
}
