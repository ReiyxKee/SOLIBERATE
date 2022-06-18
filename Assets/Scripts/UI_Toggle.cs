using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Toggle : MonoBehaviour
{
    public GameObject UI;
    public GameObject Cal;
    public Toggle UI_Button;
    public Select_Stage stage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
        UI_Button.transform.gameObject.SetActive(!Cal.activeInHierarchy && !stage.InStage);
        
        UI.SetActive(UI_Button.isOn);
    }
}
