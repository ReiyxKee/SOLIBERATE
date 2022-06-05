using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GenerateStrip : MonoBehaviour
{
    public bool startGenerate;
    public int Amt;
    public int Angle;
    public GameObject Parent;
    public GameObject Prefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startGenerate)
        {
            this.GetComponent<RingManager>().stripes = new RingStripeManager[Amt];
            for (int i = 0; i < Amt; i++)
            {
                GameObject Pref = Instantiate(Prefab, Vector3.zero, Quaternion.Euler(0,0 + i * Angle,0));
                //Pref.gameObject.name = "MeteorStripe ("+i.ToString() +")";
                Pref.transform.SetParent(Parent.transform);
                Pref.transform.localScale = Vector3.one;
                this.GetComponent<RingManager>().stripes[i] = Pref.GetComponent<RingStripeManager>();
            }
            startGenerate = false;
        }
    }
}
