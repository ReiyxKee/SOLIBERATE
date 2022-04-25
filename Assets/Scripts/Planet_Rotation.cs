using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet_Rotation : MonoBehaviour
{
    public float TimeScale = 1;

    public bool Self_Rotate;
    public float Self_Rotate_Speed;
    private float self_Rotate_Speed;
    public bool Self_Rotate_Axis_X;
    public bool Self_Rotate_Axis_Y;
    public bool Self_Rotate_Axis_Z;

    public bool Rotate_Around;
    private bool Rolling;
    public float Rotate_Around_Speed;
    public float rotate_Around_Speed;
    public GameObject Rotate_Around_Parent;
    public bool Clockwise;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (TimeScale > 2)
        {
            TimeScale -= Time.deltaTime * 100;
        }
        else if(TimeScale < 2)
        { TimeScale = 2; }

        self_Rotate_Speed = Self_Rotate_Speed * TimeScale;
        rotate_Around_Speed = Rotate_Around_Speed * TimeScale;

        if (Self_Rotate)
        {
            this.transform.Rotate(Self_Rotate_Axis_X ? self_Rotate_Speed : 0, Self_Rotate_Axis_Y ? self_Rotate_Speed : 0, Self_Rotate_Axis_Z ? self_Rotate_Speed : 0);
        }

        if(Rotate_Around && !Rolling)
        {
            StartCoroutine(Roll());
        }
    }

    public IEnumerator Roll()
    {
        Rolling = true;
        float angle = 0;
        Vector3 point = Rotate_Around_Parent.transform.position;

        while (true)
        {
            float angleSpeed = Time.fixedDeltaTime + rotate_Around_Speed;
            this.transform.RotateAround(point, Clockwise ? Vector3.up : -Vector3.up, angleSpeed);
            angle += angleSpeed;
            yield return null;
        }

    }
}
