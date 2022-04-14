using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_Stage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount == 1)
        {
            Touch screenTouch = Input.GetTouch(0);

            RaycastHit hit;
            // Create a particle if hit
            if (Physics.Raycast(screenTouch.position, -Vector3.forward, out hit))
            {   //print("Found an object - distance: " + hit.distance);
                Debug.Log(hit.rigidbody.gameObject.name.ToString());
            }
        }
    }

}
