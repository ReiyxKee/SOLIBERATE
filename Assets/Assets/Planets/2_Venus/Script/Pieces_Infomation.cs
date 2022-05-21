using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pieces_Infomation : MonoBehaviour
{
    public GameObject SolutionParent;
    public VenusPieces thisPieces;
    public bool Positioned;

    private void Update()
    {
        if (Positioned)
        {
            if (SolutionParent != null)
            {
                this.transform.position = SolutionParent.transform.position;
                this.transform.rotation = SolutionParent.transform.rotation;
            }
        }
    }

}
