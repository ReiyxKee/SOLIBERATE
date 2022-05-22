using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public GameObject Explosion;
    public Score score;
    public GameObject Parent;
    public GameObject Earth;
    public float MoveSpeed;
    public float HP;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            Instantiate(Explosion, this.transform.position, this.transform.rotation);
            GameObject.Destroy(Parent.gameObject);
            score.CurrentMeteorite -= 1;
        }
        else
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, Earth.transform.position, MoveSpeed);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Planet")
        {
            score.HP -= 10;
            score.CurrentMeteorite -= 1;
            Instantiate(Explosion, this.transform.position, this.transform.rotation);
            GameObject.Destroy(Parent.gameObject);
        }
    }
}
