using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject Explosion;
    public float LifeTime = 2.5f;
    public float bulletSpeed = 5;
    public ShootLaser sl;
    public AudioSource sfx;

    // Start is called before the first frame update
    void Start()
    {
        sfx.Play();
        this.GetComponent<Rigidbody>().AddForce(this.transform.forward * bulletSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        LifeTime -= Time.deltaTime;

        if (LifeTime <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Meteor")
        {
            other.GetComponent<Meteor>().HP -= 10;
            Instantiate(Explosion, other.transform.position, other.transform.rotation);
            sl.Hits++;
            GameObject.Destroy(this.gameObject);
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Meteor")
        {
            other.GetComponent<Meteor>().HP -= 10;
            Instantiate(Explosion, other.transform.position, other.transform.rotation);
            sl.Hits++;
            GameObject.Destroy(this.gameObject);
        }
    }
}
