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
        Desc.SetActive(true);
        GameObject.Find("Preview_UI/Description/Discovery").GetComponent<TextMeshProUGUI>().text = Detials.Title;
        GameObject.Find("Preview_UI/Description/Viewport/iNFO").GetComponent<TextMeshProUGUI>().text = Detials.Desc;
    }
}
