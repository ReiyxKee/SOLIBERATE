using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Cube_Movement : MonoBehaviour
{
    public string Color = "Yellow";
    public int Delay;
    public bool Reset = false;
    public bool Rolling;
    public float rotationSpeed;

    public void Update()
    {
        //this.transform.localPosition = new Vector3(this.transform.localPosition.x, 0.01f, this.transform.localPosition.z);
    }

    public void MakeItRoll(Vector3 positionToRotation)
    {
        if (!Rolling)
        {
            if (Delay > 0)
            {
                Delay--;
            }
            else
            {
                Rolling = true;
                StartCoroutine(Roll(positionToRotation));
            }
        }
    }

    public IEnumerator Roll(Vector3 positionToRotation)
    {
        float angle = 0;
        Vector3 point = transform.position + positionToRotation;
        Vector3 axis = Vector3.Cross(Vector3.up, positionToRotation).normalized;

        while (angle < 90f)
        {
            float angleSpeed = Time.fixedDeltaTime + rotationSpeed;
            transform.RotateAround(point, axis, angleSpeed);
            angle += angleSpeed;
            yield return null;
        }

        transform.RotateAround(point, axis, 90 - angle);
        Rolling = false;
    }

    public void _Reset()
    {
        Reset = true;
    }
}
