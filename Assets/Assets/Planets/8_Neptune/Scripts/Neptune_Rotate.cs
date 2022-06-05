using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neptune_Rotate : MonoBehaviour
{
    public int CurrentAngle;

    public bool Init_random;
    public float Init;
    // Update is called once per frame
    void Update()
    {
        if (!Init_random)
        {
            Init += Time.deltaTime;

            this.transform.localEulerAngles = new Vector3(this.transform.localEulerAngles.x, this.transform.localEulerAngles.y + (Random.Range(-5,5) * 5), this.transform.localEulerAngles.z);

            if (Init >= 1.5f)
            {
                Init_random = true;
            }
        }

        CurrentAngle = Mathf.RoundToInt(this.transform.localEulerAngles.y);
    }
}
