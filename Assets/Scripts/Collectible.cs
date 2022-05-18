using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public Unlockables unlockable;
    public int Unlockable_ID;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (unlockable == null && GameObject.Find("PlanetGame"))
        {
            unlockable = GameObject.Find("PlanetGame").GetComponent<Unlockables>();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Unlocked " + Unlockable_ID);
            unlockable.Unlockable_detial[Unlockable_ID].Unlocked = true;
            unlockable.Save(unlockable.ThisPlanet, Unlockable_ID, unlockable.Unlockable_detial[Unlockable_ID].Unlocked ? 1 : 0);
            //Animate Button
            //Animate this Game Model
        }
    }
}
