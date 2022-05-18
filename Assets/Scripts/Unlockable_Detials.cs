using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Unlockable_Detials : MonoBehaviour
{
    public Unlockable_Detial Detials;

    public TextMeshProUGUI Title;
    public GameObject Desc;

    //Preview Desc =  true 
    public bool PrevIG;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Button>().interactable = Detials.Unlocked;

    }

    public void ShowDesc()
    {
        if (PrevIG)
        {
            Desc.SetActive(true);
            GameObject.Find("Preview_UI/Description/Discovery").GetComponent<TextMeshProUGUI>().text = Detials.Title;
            GameObject.Find("Preview_UI/Description/Viewport/iNFO").GetComponent<TextMeshProUGUI>().text = Detials.Desc;
        }
        else
        {
            Desc.SetActive(true);
            GameObject.Find("InGame_UI/Description/Discovery").GetComponent<TextMeshProUGUI>().text = Detials.Title;
            GameObject.Find("InGame_UI/Description/Viewport/iNFO").GetComponent<TextMeshProUGUI>().text = Detials.Desc;
        }
    }
}
