using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tap_On_Plate : MonoBehaviour
{

    public int Group;
    public int Num;

    public Material[] _Normal;
    public Material[] _CanTap;
    public Material[] _Tapped;

    public MeshRenderer mesh;

    public bool Started;
    public bool CanTap;
    public bool Tapped;
    public float _CanTap_Timer;
    public float _CanTap_CD;
    public float _CanTap_CD_Default;
    // Start is called before the first frame update


    public void Start()
    {
        mesh = this.GetComponentInChildren<MeshRenderer>();
        mesh.materials = _Normal;
    }
    public void Update()
    {
        if (Started)
        {
            if (Tapped)
            {
            }
            else
            {
                if (CanTap && !Tapped)
                {
                    _CanTap_CD -= Time.deltaTime;
                }

                if (_CanTap_Timer >= 0)
                {
                    _CanTap_Timer -= Time.deltaTime;
                }
                else
                {
                    CanTap = true;
                }

                if (_CanTap_CD <= 0)
                {
                    CanTap = false;
                    _CanTap_CD = _CanTap_CD_Default;
                    _CanTap_Timer = Random.Range(1, 10);
                }

                if (CanTap)
                {
                    mesh.materials = _CanTap;
                }
                else
                {
                    mesh.materials = _Normal;
                }

            }
        }
    }
}