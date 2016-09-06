using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoadRender : MonoBehaviour
{
    MeshFilter mesh;
    MeshRenderer meshRender;
    public float width;
    Vector3[] points;
    public void Flush()
    {
        Vector3[] vertices = new Vector3[(points.Length - 1) * 4];
        int[] triangles = new int[(points.Length - 1) * 2 * 3];

        mesh.mesh.Clear();

        for (int i = 0; i < points.Length - 1; i++)
        {
            Vector3 start = points[i];
            Vector3 end = points[i + 1];
            Vector3 direct = end - start;
            Vector3 t = Vector3.Cross(Vector3.up, direct);
            t = width / 2.0f * Vector3.Normalize(t);

            vertices[i * 4 + 0] = start + t;
            vertices[i * 4 + 1] = start - t;
            vertices[i * 4 + 2] = end - t;
            vertices[i * 4 + 3] = end + t;

            triangles[i * 6 + 0] = i * 4 + 0;
            triangles[i * 6 + 1] = i * 4 + 1;
            triangles[i * 6 + 2] = i * 4 + 2;
            triangles[i * 6 + 3] = i * 4 + 2;
            triangles[i * 6 + 4] = i * 4 + 3;
            triangles[i * 6 + 5] = i * 4 + 0;
        }

        mesh.mesh.vertices = vertices;
        mesh.mesh.triangles = triangles;
    }

    public void SetPosition(int index, Vector3 point)
    {
        points[index] = point;
    }

    public void SetVertexCount(int count)
    {
        points = new Vector3[count];
    }

    //private void GenerateOneRode(Vector3 v1, Vector3 v2)
    //{
    //    throw new System.NotImplementedException();
    //}
    // Use this for initialization
	void Start () 
    {
        mesh = gameObject.GetComponent<MeshFilter>();
        meshRender = gameObject.GetComponent<MeshRenderer>();
	}
	// Update is called once per frame
	void Update () 
    {
	
	}
}
