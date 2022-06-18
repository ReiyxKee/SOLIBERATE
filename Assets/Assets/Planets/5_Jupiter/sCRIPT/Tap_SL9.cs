using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tap_SL9 : MonoBehaviour
{
    public float PopSpeed;
    public float ScaleRatio;
    public float StayUpTime;

    public void Start()
    {
        this.GetComponent<RectTransform>().localScale = new Vector3(0.75f, 0.75f, 0.75f);

        this.GetComponent<RectTransform>().localScale = new Vector3(this.GetComponent<RectTransform>().localScale.x * ScaleRatio, this.GetComponent<RectTransform>().localScale.y * ScaleRatio, this.GetComponent<RectTransform>().localScale.z * ScaleRatio);
    }

    private void Update()
    {
        StayUpTime += Time.deltaTime;

        if (StayUpTime >= 0.75f)
        {
            GameObject.Destroy(this.gameObject);
        }

        float Pos = this.GetComponent<RectTransform>().position.y + PopSpeed;

        this.GetComponent<RectTransform>().position = new Vector3(this.GetComponent<RectTransform>().position.x, Pos, this.GetComponent<RectTransform>().position.z);

    }
}
