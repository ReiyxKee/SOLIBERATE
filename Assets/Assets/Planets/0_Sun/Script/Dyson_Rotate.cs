using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dyson_Rotate : MonoBehaviour
{
    public float rotateSpeed = 1;
    public bool x, y, z;
    public bool reverse;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localEulerAngles = new Vector3(x ? this.transform.localEulerAngles.x + Time.deltaTime * (reverse ? -rotateSpeed : rotateSpeed) : this.transform.localEulerAngles.x,
            y ? this.transform.localEulerAngles.y +  Time.deltaTime * (reverse ? -rotateSpeed : rotateSpeed) : this.transform.localEulerAngles.y,
            z ? this.transform.localEulerAngles.z +  Time.deltaTime * (reverse ? -rotateSpeed : rotateSpeed) : this.transform.localEulerAngles.z);
    }
}
