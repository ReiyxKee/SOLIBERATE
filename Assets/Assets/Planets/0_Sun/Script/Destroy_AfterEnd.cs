using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_AfterEnd : MonoBehaviour
{
    public ParticleSystem kaboom;
    // Start is called before the first frame update
    void Start()
    {
        kaboom.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (kaboom.isStopped)
        {
            Destroy(this.gameObject);
        }
    }
}
