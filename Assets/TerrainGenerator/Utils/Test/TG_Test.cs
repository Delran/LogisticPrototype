using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TG_Test : MonoBehaviour
{
    Vector3[] newVertices;
    Vector2[] newUV;
    int[] newTriangles;
    // Start is called before the first frame update
    void Start()
    {
        //TestInt();
        //TestPlane();
        TestMesh();
    }

    void TestMesh()
    {
        GameObject _plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        MeshFilter _planeMesh = _plane.GetComponent<MeshFilter>();
        MeshRenderer _planeRenderer = _plane.GetComponent<MeshRenderer>();

        Vector3[] _vercicles = _planeMesh.mesh.vertices;

        Mesh mesh = new Mesh();
        gameObject.AddComponent<MeshFilter>().mesh = mesh;
        mesh.vertices = _vercicles;
        //mesh.uv = _planeMesh.mesh.uv;
        mesh.triangles = _planeMesh.mesh.triangles;
        Debug.Log(mesh.triangles.Length);
        gameObject.AddComponent<MeshRenderer>();
    }

    void TestPlane()
    {

    }

    void TestInt()
    {

    }

    void DisplayTwoDimentionalArray<T>(T[,] _array)
    {
        for (int x = 0; x < _array.GetLength(0); x++)
        {
            for (int y = 0; y < _array.GetLength(1); y++)
            {
                //Debug.Log();
            }
        }
    }
}
