using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWritter : MonoBehaviour
{
    public string text;
    public int i = 0;
    public float TextSpeed;
    private float textspeed;
    public AudioSource Beep;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textspeed += Time.deltaTime;

        if (textspeed > TextSpeed && i < text.Length)
        {
            textspeed = 0;
            this.GetComponent<TextMeshProUGUI>().text += text[i];
            Beep.Play();
            i++;
        }

    }

    public void Reset()
    {
        i = 0;
        this.GetComponent<TextMeshProUGUI>().text = "";
    }
}
