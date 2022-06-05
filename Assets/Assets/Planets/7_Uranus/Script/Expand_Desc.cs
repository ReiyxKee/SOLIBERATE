using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Expand_Desc : MonoBehaviour
{
    public bool Expand;
    public Animator anim;
    public TextMeshProUGUI Expans;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Expand", Expand);
        Expans.text = Expand ? "Collapse" : "Expand";
    }

    public void expand()
    {
        Expand = !Expand;
    }
}
