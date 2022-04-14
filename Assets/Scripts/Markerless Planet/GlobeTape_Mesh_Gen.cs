using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GlobeTape_Mesh_Gen : MonoBehaviour
{
    public GameObject Sphere;
    public float radius = 1.0f;
    public Vector2 LatitudeLongitude1;
    public Vector2 LatitudeLongitude2;

    public float Tape_Width;
    public int Tape_Segments;
    public Material[] Tape_Material;

    public bool Generated;

    private void Start()
    {
        Generated = false;
    }

    private void Update()
    {
        if (!Generated)
        {
            radius = Sphere.GetComponent<SphereCollider>().radius;
            this.GetComponent<MeshRenderer>().materials = Tape_Material;
            this.GetComponent<MeshFilter>().mesh = CreateTapeMesh(LatitudeLongitude1, LatitudeLongitude2, Tape_Width, Tape_Segments);
            this.GetComponent<MeshCollider>().sharedMesh = this.GetComponent<MeshFilter>().mesh;
            Generated = true;
        }
    }

    public Mesh CreateTapeMesh(Vector2 latLon1, Vector2 latLon2, float width, int nSegments)
    {
        //Creat the tape mesh
        //Get the points between these two
        Vector3[] points = GetPointsBetween(latLon1, latLon2, nSegments + 1).ToArray();

        //Build mesh vertices
        Vector3[] verts = new Vector3[nSegments * 2 + 2];
        Vector3[] normals = new Vector3[verts.Length];
        int[] triangles = new int[nSegments * 6];
        int p = 0;
        for (int i = 0; i < points.Length; i++)
        {
            Vector3 anchor = points[i] + (points[i] - transform.position).normalized * 0.005f;

            Vector3 toCenterVect = (transform.position - anchor).normalized;
            Vector3 forwardVect = (i < points.Length - 1) ? (points[i + 1] - anchor).normalized : (anchor - points[i - 1]).normalized;
            Vector3 sideVect = Vector3.Cross(toCenterVect, forwardVect);

            Vector3 leftVert = anchor - (sideVect.normalized * width * 0.5f);
            Vector3 rightVert = anchor + (sideVect.normalized * width * 0.5f);

            verts[p] = transform.InverseTransformPoint(leftVert);
            verts[p + 1] = transform.InverseTransformPoint(rightVert);

            //normal
            normals[p] = (leftVert - transform.position).normalized;
            normals[p + 1] = (rightVert - transform.position).normalized;

            p += 2;
        }

        //triangles
        int c = 0;
        int tri = 0;
        for (int i = 0; i < nSegments; i++)
        {
            triangles[tri] = c;
            triangles[tri + 1] = c + 1;
            triangles[tri + 2] = c + 2;
            triangles[tri + 3] = c + 1;
            triangles[tri + 4] = c + 3;
            triangles[tri + 5] = c + 2;
            c += 2;
            tri += 6;
        }

        //create Mesh
        Mesh mesh = new Mesh();
        mesh.vertices = verts;
        mesh.triangles = triangles;
        mesh.normals = normals;
        mesh.RecalculateBounds();
        mesh.Optimize();

        return mesh;
    }

    private List<Vector3> GetPointsBetween(Vector2 latLon1, Vector2 latLon2, int n)
    {
        //using slerp interpolation 
        List<Vector3> points = new List<Vector3>();
        Vector3 center = transform.position;
        Vector3 startPoint = GeoToWorldPosition(latLon1.x, latLon1.y);
        Vector3 endPoint = GeoToWorldPosition(latLon2.x, latLon2.y);

        Vector3 u = startPoint - transform.position;
        Vector3 v = endPoint - transform.position;

        for (int i = 0; i < n; i++)
        {
            float t = (float)i / (n - 1f);
            //final point
            Vector3 p = center + Vector3.Slerp(u, v, t);
            //add offset
            Vector3 off = (p - transform.position).normalized * 0.001f;
            points.Add(p + off);
        }
        return points;
    }
    public Vector3 GeoToWorldPosition(float lat, float lon)
    {
        lat = (90f - lat) * Mathf.Deg2Rad;
        lon *= Mathf.Deg2Rad;

        float x = radius * Mathf.Sin(lat) * Mathf.Cos(lon);
        float y = radius * Mathf.Sin(lat) * Mathf.Sin(lon);
        float z = radius * Mathf.Cos(lat);

        Vector3 position = new Vector3(-x, z, -y);

        return transform.TransformPoint(position);
    }
}
