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
        //GameObject _plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        //MeshFilter _planeMesh = _plane.GetComponent<MeshFilter>();
        //MeshRenderer _planeRenderer = _plane.GetComponent<MeshRenderer>();

        int _side = 9;
        int _mapSize = _side * _side;
        Vector3[] _vercicles = new Vector3[_mapSize];

        Vector3 _initialPos = Vector3.zero;

        //To get the number of triangles, we count the number of squares wich is number of vertice on one side - 1 power 2
        // 9 vertices each side = (9 - 1)^2 squares
        int _trianglesSize = (_side - 1) * (_side - 1);
        //There is 2 triangle by square and each triangle has two points so triangles = squares * 2 * 3;
        _trianglesSize *= 6;

        int[] _triangles = new int[_trianglesSize];

        Debug.Log(_trianglesSize);
        Debug.Log(_mapSize);

        float _step = 1;
        int _triangleIt = 0;
        for (int i = 0, _row = 0; i < _mapSize; i++)
        {
            int y = i % _side;
            if (i != 0 && y == 0) _row++;
            _vercicles[i] = new Vector3(_row * _step, 0 , y * _step) + _initialPos;
            if (_row != 0)
            {
                if (y != 0)
                {
                    _triangles[_triangleIt++] = i-_side;
                    _triangles[_triangleIt++] = i;
                    _triangles[_triangleIt++] = i-1;
                }
                if (y != _side - 1)
                {
                    _triangles[_triangleIt++] = i - (_side - 1);
                    _triangles[_triangleIt++] = i;
                    _triangles[_triangleIt++] = i - _side;
                }
            }
        }

        Mesh mesh = new Mesh();
        gameObject.AddComponent<MeshFilter>().mesh = mesh;
        mesh.vertices = _vercicles;
        //mesh.uv = _planeMesh.mesh.uv;
        mesh.triangles = _triangles;
        //mesh.normals = _planeMesh.mesh.normals;

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
