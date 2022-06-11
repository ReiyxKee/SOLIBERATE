using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorBlock : MonoBehaviour
{
    public bool block;
    public bool spawned;
    public MeteorManager manager;
    public GameObject[] Prefab;
    // Start is called before the first frame update
    void Start()
    {
        if (Random.Range(0, 100) > 80)
        {
            block = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (block && !spawned)
        {
            if (GameObject.Find("Meteorite_Pos"))
            {
                GameObject rock = Instantiate(Prefab[Random.Range(0, Prefab.Length)], this.transform.position, Quaternion.identity, this.transform);               
                spawned = true;
            }
        }
    }

    public void Reset()
    {

        foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        if (GameObject.Find("Meteorite_Pos"))
        {
            if (Random.Range(0, 100) > 80)
            {
                GameObject rock = Instantiate(Prefab[Random.Range(0, Prefab.Length)], this.transform.position, Quaternion.identity, this.transform);
                spawned = true;
            }
        }
    }
}
