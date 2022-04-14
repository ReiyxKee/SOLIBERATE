using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Current_Stage : MonoBehaviour
{
    public GameObject[] StageList;
    public GameObject CurrentStage;
    public int Stage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentStage == null)
        {
            CurrentStage = StageList[Stage];
            StageList[Stage].SetActive(true);
        }
    }

    public void SwitchStage_Test_Next()
    {
        Stage++;
        if(Stage >= StageList.Length)
        {
            Stage = 0;
        }

        StageList[Stage].SetActive(true);
        
        if (Stage - 1 < 0)
        {
            StageList[StageList.Length - 1].SetActive(false);
        }
        else
        {
            StageList[Stage - 1].SetActive(false);
        }

        CurrentStage = StageList[Stage];
    }
}
