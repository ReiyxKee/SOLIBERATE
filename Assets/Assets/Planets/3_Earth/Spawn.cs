using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    public GameObject Earth;
    public GameObject[] MeteorPrefab;

    public Score scoreRef;

    public float Range_Min;
    public float Range_Max;

    public float MinMeteorSpeed;
    public float MaxMeteorSpeed;

    public float spawn_timer;
    public float spawn_timer_default;

    public bool Started;
    public GameObject Parent;

    public int Spawn_Limit = 50;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Started)
        {
            spawn_timer -= Time.deltaTime;

            if (spawn_timer <= 0)
            {
                spawn_timer = spawn_timer_default;
                SpawnMeteor(RandomPosition());
            }
        }
    }

    public void SpawnMeteor(Vector3 Position)
    {
        if (Spawn_Limit > 0)
        {
            GameObject Meteor = Instantiate(MeteorPrefab[Random.Range(0, 3)], Position, Quaternion.LookRotation(Earth.transform.position, transform.up));
            Meteor.transform.SetParent(Parent.transform);

            Meteor.GetComponentInChildren<Meteor>().MoveSpeed = Random.Range(MinMeteorSpeed, MaxMeteorSpeed);
            Meteor.GetComponentInChildren<Meteor>().Earth = Earth;
            Meteor.GetComponentInChildren<Meteor>().HP = 50;
            Meteor.GetComponentInChildren<Meteor>().score = scoreRef;
            Spawn_Limit--;
        }
    }

    public Vector3 RandomPosition()
    {
        float x = Random.Range(0,100) >= 50 ? Random.Range(Earth.transform.position.x + Range_Min, Earth.transform.position.x + Range_Max) : Random.Range(Earth.transform.position.x - Range_Min, Earth.transform.position.x - Range_Max);
        float y = Random.Range(Earth.transform.position.y-0.15f, Earth.transform.position.y + 0.15f); 
        float z = Random.Range(0, 100) >= 50 ? Random.Range(Earth.transform.position.z + Range_Min, Earth.transform.position.z + Range_Max) : Random.Range(Earth.transform.position.z - Range_Min, Earth.transform.position.z - Range_Max);

        Vector3 RandPos = new Vector3(x, y, z);
        return RandPos;
    }
}
