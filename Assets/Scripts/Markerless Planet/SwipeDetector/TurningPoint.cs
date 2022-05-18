using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[ExecuteInEditMode]
public class TurningPoint : MonoBehaviour
{
    public GameObject Center;
    public GameObject Player;
    public GameObject PlayerModel;
    public bool Rolling;
    public float rotationSpeed;

    public bool Up_On;
    public Vector3 Up;
    public bool Down_On;
    public Vector3 Down;
    public bool Left_On;
    public Vector3 Left;
    public bool Right_On;
    public Vector3 Right;

    public bool LHR;
    public bool Exist;

    public SwipeDetector TouchInput;

    public UI ui_Ref;

    public bool PosDependant_Up;
    public bool PosDependant_Down;

    public SwipeData swipeData = new SwipeData{ Direction = SwipeDirection.None};

    public bool ifTutorial;
    public bool SpamLock;

    public Step_Limit step_ref;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (step_ref == null && GameObject.Find("PlayerAxis"))
        {
            step_ref = GameObject.Find("PlayerAxis").GetComponent<Step_Limit>();
        }

        if (ui_Ref == null)
        {
            ui_Ref = GameObject.Find("/Canvas").gameObject.GetComponent<UI>();
        }

        if (PlayerModel == null)
        {
            PlayerModel = GameObject.Find("PlayerModel");
        }

        if (SpamLock && Input.touchCount == 0)
        {
            SpamLock = false;
        }

        if (Exist)
        {
            if (!SpamLock)
            {
                if (!TouchInput.Swiped)
                {
                    if (PosDependant_Up)
                    {
                        if (Input.touchCount == 1)
                        {
                            Touch screenTouch = Input.GetTouch(0);

                            if (screenTouch.phase == TouchPhase.Moved)
                            {

                                swipeData = TouchInput.swipe;

                                if (45 > ui_Ref.UIOreintation && ui_Ref.UIOreintation > -45)
                                {
                                    if (swipeData.Direction == SwipeDirection.Up && !Rolling && !TouchInput.Swiping && Down_On)
                                    {
                                        StartCoroutine(Roll(Down));
                                    }

                                    if (swipeData.Direction == SwipeDirection.Down && !Rolling && !TouchInput.Swiping && Up_On)
                                    {
                                        StartCoroutine(Roll(Up));
                                    }

                                    if (swipeData.Direction == SwipeDirection.Left && !Rolling && !TouchInput.Swiping && Left_On)
                                    {
                                        StartCoroutine(Roll(Left));
                                    }

                                    if (swipeData.Direction == SwipeDirection.Right && !Rolling && !TouchInput.Swiping && Right_On)
                                    {
                                        StartCoroutine(Roll(Right));
                                    }
                                }
                                else if (180 >= ui_Ref.UIOreintation && ui_Ref.UIOreintation > 135 || -135 > ui_Ref.UIOreintation && ui_Ref.UIOreintation >= -180)
                                {
                                    if (swipeData.Direction == SwipeDirection.Up && !Rolling && !TouchInput.Swiping && Up_On)
                                    {
                                        StartCoroutine(Roll(Up));
                                    }

                                    if (swipeData.Direction == SwipeDirection.Down && !Rolling && !TouchInput.Swiping && Down_On)
                                    {
                                        StartCoroutine(Roll(Down));
                                    }

                                    if (swipeData.Direction == SwipeDirection.Left && !Rolling && !TouchInput.Swiping && Right_On)
                                    {
                                        StartCoroutine(Roll(Right));
                                    }

                                    if (swipeData.Direction == SwipeDirection.Right && !Rolling && !TouchInput.Swiping && Left_On)
                                    {
                                        StartCoroutine(Roll(Left));
                                    }
                                }
                                else if (135 >= ui_Ref.UIOreintation && ui_Ref.UIOreintation >= 45)
                                {
                                    if (swipeData.Direction == SwipeDirection.Up && !Rolling && !TouchInput.Swiping && Right_On)
                                    {
                                        StartCoroutine(Roll(Right));
                                    }

                                    if (swipeData.Direction == SwipeDirection.Down && !Rolling && !TouchInput.Swiping && Left_On)
                                    {
                                        StartCoroutine(Roll(Left));
                                    }

                                    if (swipeData.Direction == SwipeDirection.Left && !Rolling && !TouchInput.Swiping && Down_On)
                                    {
                                        StartCoroutine(Roll(Down));
                                    }

                                    if (swipeData.Direction == SwipeDirection.Right && !Rolling && !TouchInput.Swiping && Up_On)
                                    {
                                        StartCoroutine(Roll(Up));
                                    }
                                }
                                else if (-45 >= ui_Ref.UIOreintation && ui_Ref.UIOreintation >= -135)
                                {
                                    if (swipeData.Direction == SwipeDirection.Up && !Rolling && !TouchInput.Swiping && Left_On)
                                    {
                                        StartCoroutine(Roll(Left));
                                    }

                                    if (swipeData.Direction == SwipeDirection.Down && !Rolling && !TouchInput.Swiping && Right_On)
                                    {
                                        StartCoroutine(Roll(Right));
                                    }

                                    if (swipeData.Direction == SwipeDirection.Left && !Rolling && !TouchInput.Swiping && Up_On)
                                    {
                                        StartCoroutine(Roll(Up));
                                    }

                                    if (swipeData.Direction == SwipeDirection.Right && !Rolling && !TouchInput.Swiping && Down_On)
                                    {
                                        StartCoroutine(Roll(Down));
                                    }
                                }

                            }
                        }
                    }
                    else if (PosDependant_Down)
                    {
                        if (Input.touchCount == 1)
                        {
                            Touch screenTouch = Input.GetTouch(0);

                            if (screenTouch.phase == TouchPhase.Moved)
                            {
                                //Debug.Log(TouchInput.swipe.Direction);

                                swipeData = TouchInput.swipe;

                                if (45 > ui_Ref.UIOreintation && ui_Ref.UIOreintation > -45)
                                {
                                    if (swipeData.Direction == SwipeDirection.Up && !Rolling && !TouchInput.Swiping && Up_On)
                                    {
                                        StartCoroutine(Roll(Up));
                                    }

                                    if (swipeData.Direction == SwipeDirection.Down && !Rolling && !TouchInput.Swiping && Down_On)
                                    {
                                        StartCoroutine(Roll(Down));
                                    }

                                    if (swipeData.Direction == SwipeDirection.Left && !Rolling && !TouchInput.Swiping && Left_On)
                                    {
                                        StartCoroutine(Roll(Left));
                                    }

                                    if (swipeData.Direction == SwipeDirection.Right && !Rolling && !TouchInput.Swiping && Right_On)
                                    {
                                        StartCoroutine(Roll(Right));
                                    }
                                }
                                else if (180 >= ui_Ref.UIOreintation && ui_Ref.UIOreintation > 135 || -135 > ui_Ref.UIOreintation && ui_Ref.UIOreintation >= -180)
                                {
                                    if (swipeData.Direction == SwipeDirection.Up && !Rolling && !TouchInput.Swiping && Down_On)
                                    {
                                        StartCoroutine(Roll(Down));
                                    }

                                    if (swipeData.Direction == SwipeDirection.Down && !Rolling && !TouchInput.Swiping && Up_On)
                                    {
                                        StartCoroutine(Roll(Up));
                                    }

                                    if (swipeData.Direction == SwipeDirection.Left && !Rolling && !TouchInput.Swiping && Right_On)
                                    {
                                        StartCoroutine(Roll(Right));
                                    }

                                    if (swipeData.Direction == SwipeDirection.Right && !Rolling && !TouchInput.Swiping && Left_On)
                                    {
                                        StartCoroutine(Roll(Left));
                                    }
                                }
                                else if (135 >= ui_Ref.UIOreintation && ui_Ref.UIOreintation >= 45)
                                {
                                    if (swipeData.Direction == SwipeDirection.Up && !Rolling && !TouchInput.Swiping && Right_On)
                                    {
                                        StartCoroutine(Roll(Right));
                                    }

                                    if (swipeData.Direction == SwipeDirection.Down && !Rolling && !TouchInput.Swiping && Left_On)
                                    {
                                        StartCoroutine(Roll(Left));
                                    }

                                    if (swipeData.Direction == SwipeDirection.Left && !Rolling && !TouchInput.Swiping && Up_On)
                                    {
                                        StartCoroutine(Roll(Up));
                                    }

                                    if (swipeData.Direction == SwipeDirection.Right && !Rolling && !TouchInput.Swiping && Down_On)
                                    {
                                        StartCoroutine(Roll(Down));
                                    }
                                }
                                else if (-45 >= ui_Ref.UIOreintation && ui_Ref.UIOreintation >= -135)
                                {
                                    if (swipeData.Direction == SwipeDirection.Up && !Rolling && !TouchInput.Swiping && Left_On)
                                    {
                                        StartCoroutine(Roll(Left));
                                    }

                                    if (swipeData.Direction == SwipeDirection.Down && !Rolling && !TouchInput.Swiping && Right_On)
                                    {
                                        StartCoroutine(Roll(Right));
                                    }

                                    if (swipeData.Direction == SwipeDirection.Left && !Rolling && !TouchInput.Swiping && Down_On)
                                    {
                                        StartCoroutine(Roll(Down));
                                    }

                                    if (swipeData.Direction == SwipeDirection.Right && !Rolling && !TouchInput.Swiping && Up_On)
                                    {
                                        StartCoroutine(Roll(Up));
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        if (Input.touchCount == 1)
                        {
                            Touch screenTouch = Input.GetTouch(0);

                            if (screenTouch.phase == TouchPhase.Moved)
                            {
                                //Debug.Log(TouchInput.swipe.Direction);

                                swipeData = TouchInput.swipe;

                                if (ifTutorial)
                                {
                                    if (swipeData.Direction != SwipeDirection.Up)
                                    {
                                        swipeData.Direction = SwipeDirection.None;
                                    }
                                }


                                if (swipeData.Direction == SwipeDirection.Up && !Rolling && !TouchInput.Swiping && Up_On)
                                {
                                    PlayerModel.transform.rotation = Quaternion.LookRotation(this.transform.up);
                                    PlayerModel.transform.GetChild(0).transform.localEulerAngles = new Vector3(0, -90, -90);
                                    StartCoroutine(Roll(Up));
                                }

                                if (swipeData.Direction == SwipeDirection.Down && !Rolling && !TouchInput.Swiping && Down_On)
                                {
                                    PlayerModel.transform.rotation = Quaternion.LookRotation(-this.transform.up);
                                    PlayerModel.transform.GetChild(0).transform.localEulerAngles = new Vector3(0, -90, -90);
                                    StartCoroutine(Roll(Down));
                                }

                                if (swipeData.Direction == SwipeDirection.Left && !Rolling && !TouchInput.Swiping && Left_On)
                                {
                                    PlayerModel.transform.rotation = Quaternion.LookRotation(-this.transform.right);
                                    PlayerModel.transform.GetChild(0).transform.localEulerAngles = new Vector3(0, -90, -90);
                                    StartCoroutine(Roll(Left));
                                }

                                if (swipeData.Direction == SwipeDirection.Right && !Rolling && !TouchInput.Swiping && Right_On)
                                {

                                    PlayerModel.transform.rotation = Quaternion.LookRotation(this.transform.right);
                                    PlayerModel.transform.GetChild(0).transform.localEulerAngles = new Vector3(180, -90, -90);
                                    StartCoroutine(Roll(Right));
                                }

                                //Debug.Log(PlayerModel.transform.eulerAngles);
                            }
                        }

                    }
                }
            }
        }
    }

    public IEnumerator Roll(Vector3 axis)
    {
        Rolling = true;
        float angle = 0;
        Vector3 point = Center.transform.position;
        

        swipeData.Direction = SwipeDirection.None;

        while (angle < 90f)
        {
            float angleSpeed = Time.fixedDeltaTime + rotationSpeed;
            Player.transform.RotateAround(point, axis, angleSpeed);
            angle += angleSpeed;
            yield return null;
        }

        Player.transform.RotateAround(point, axis, 90 - angle);
        swipeData.Direction = SwipeDirection.None;
        TouchInput.Swiped = true;
        Rolling = false;
        step_ref.Fuel--;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (PosDependant_Up)
            {
                PlayerModel.transform.eulerAngles = new Vector3(PlayerModel.transform.eulerAngles.x, PlayerModel.transform.eulerAngles.y, -90);
            }
            else if (PosDependant_Down)
            {
                PlayerModel.transform.eulerAngles = new Vector3(PlayerModel.transform.eulerAngles.x, PlayerModel.transform.eulerAngles.y, 90);
            }
            swipeData.Direction = SwipeDirection.None;
            SpamLock = true;
            Exist = true;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Exist = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            swipeData.Direction = SwipeDirection.None;
            Exist = false;
        }
    }
}
