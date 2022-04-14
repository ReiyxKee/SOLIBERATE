using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GlobeStation_Pos : MonoBehaviour
{
    public GameObject Sphere;
    public float radius = 1.0f;
    public Vector2 LatitudeLongitude;

    private void Start()
    {
    }

    private void Update()
    {
        this.transform.position = Sphere.transform.position + GeoToWorldPosition(LatitudeLongitude.x,LatitudeLongitude.y);
    }

    public Vector3 GeoToWorldPosition(float lat, float lon)
    {
        lat = (90f - lat) * Mathf.Deg2Rad;
        lon *= Mathf.Deg2Rad;

        float x = radius * Mathf.Sin(lat) * Mathf.Cos(lon);
        float y = radius * Mathf.Sin(lat) * Mathf.Sin(lon);
        float z = radius * Mathf.Cos(lat);

        Vector3 position = new Vector3(-x, z, -y);

        return position;
    }
}
