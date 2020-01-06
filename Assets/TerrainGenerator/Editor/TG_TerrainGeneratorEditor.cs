using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EditoolsUnity;
using UnityEditor;
using System;

[CustomEditor(typeof(TG_TerrainGenerator))]
public class TG_TerrainGeneratorEditor : EditorCustom<TG_TerrainGenerator>
{
    bool displayArray = false;

    //float[,] heightMap = null;

     public float minValue => eTarget.minValue;
     public float maxValue => eTarget.maxValue;
     public float minMapValue => eTarget.minMapValue;
     public float maxMapValue => eTarget.maxMapValue;
     public bool useMinMax => eTarget.useMinMax;

    protected override void OnEnable()
    {
        base.OnEnable();
        name = "[Terrain Generator]";
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditoolsBox.HelpBoxInfo($"Terrain generator build : {eTarget.Version}");

        GUILayout.Label("Initials corner value for heightmap generation");
        EditoolsField.FloatField("Up Left", ref eTarget.cornerUpLeft);
        EditoolsField.FloatField("Up Right", ref eTarget.cornerUpRight);
        EditoolsField.FloatField("Down Left", ref eTarget.cornerDownLeft);
        EditoolsField.FloatField("Down Right", ref eTarget.cornerDownRight);

        EditoolsBox.HelpBoxInfo("The map size will be of equation 2 ^ power + 1");
        EditoolsField.IntField("power", ref eTarget.power);

        EditoolsField.IntField("Random range", ref eTarget.randomRange);

        EditoolsLayout.Space();
        GUILayout.Label("Rasterization settings : ");
        EditoolsField.Toggle("Use min and max value", ref eTarget.useMinMax);
        EditoolsField.FloatField("Maximum value", ref eTarget.maxValue);
        EditoolsField.FloatField("Minimum value", ref eTarget.minValue);
        EditoolsLayout.Space();


        /*EditoolsLayout.Space();
        EditoolsButton.Button("Generate Height map", Color.white, GenerateHeightMap);
        EditoolsLayout.Space();*/

        EditoolsLayout.Space();
        EditoolsButton.Button("Create Map", Color.white, CreateMap);
        EditoolsLayout.Space();

        displayArray = EditoolsLayout.Foldout(displayArray, "Heightmap Content");
        if (displayArray)
        {
            if (eTarget.heightMap== null)
            {
                EditoolsBox.HelpBoxError("No valid heightmap");
            }
            else
            {
                DisplayTwoDimentionalArray(eTarget.heightMap);
            }
        }
            
    }

    private void OnSceneGUI()
    {
        
    }

    void GenerateHeightMap()
    {
        float[,] _initialValues = new float[2, 2];
        _initialValues[0, 0] = eTarget.cornerUpLeft;
        _initialValues[1, 0] = eTarget.cornerUpRight;
        _initialValues[0, 1] = eTarget.cornerDownLeft;
        _initialValues[1, 1] = eTarget.cornerDownRight;

        int _mapSize = (int)Mathf.Pow(2, eTarget.power) + 1;
        eTarget.mapSize = _mapSize % 2 == 0 ? _mapSize + 1 : _mapSize;

        eTarget.heightMap = TG_HeightMapGenerator.DiamondSquareNoiseMap(eTarget.mapSize, _initialValues, eTarget.randomRange, ref eTarget.maxMapValue, ref eTarget.minMapValue);

        RasterizeHeightMap(ref eTarget.heightMap);
    }


    void CreateMap()
    {
        GenerateHeightMap();
        //GameObject _plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        //MeshFilter _planeMesh = _plane.GetComponent<MeshFilter>();
        //MeshRenderer _planeRenderer = _plane.GetComponent<MeshRenderer>();
        GameObject _map = new GameObject();

        int _mapSize = eTarget.mapSize * eTarget.mapSize;
        int _side = eTarget.mapSize;
        Vector3[] _vercicles = new Vector3[_mapSize];

        Vector3 _initialPos = Vector3.zero;

        //To get the number of triangles, we count the number of squares wich is number of vertice on one side - 1 power 2
        // 9 vertices each side = (9 - 1)^2 squares
        int _trianglesSize = (_side - 1) * (_side - 1);
        //There is 2 triangle by square and each triangle has two points so triangles = squares * 2 * 3;
        _trianglesSize *= 6;

        int[] _triangles = new int[_trianglesSize];

        float _step = 1;
        int _triangleIt = 0;
        for (int i = 0, _row = 0; i < _mapSize; i++)
        {
            int y = i % _side;
            if (i != 0 && y == 0) _row++;
            _vercicles[i] = new Vector3(_row * _step, eTarget.heightMap[_row, y]*10, y * _step) + _initialPos;
            if (_row != 0)
            {
                if (y != 0)
                {
                    _triangles[_triangleIt++] = i - _side;
                    _triangles[_triangleIt++] = i;
                    _triangles[_triangleIt++] = i - 1;
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
        _map.AddComponent<MeshFilter>().mesh = mesh;
        mesh.vertices = _vercicles;
        //mesh.uv = _planeMesh.mesh.uv;
        mesh.triangles = _triangles;
        //mesh.normals = _planeMesh.mesh.normals;

        _map.AddComponent<MeshRenderer>();

    }

    /*void RasterizeHeightMap(float _min = 0, float _max = 1)
    {
        OnEachArrayCase(ref heightMap, (_array, _x, _y) => CaseRasterization(ref _array, _x, _y, _min, _max));
    }

    void CaseRasterization(ref float[,] _array, int _x, int _y, float _min, float _max)
    {
        float _val = _array[_x, _y];
    }

    void OnEachArrayCase<T>(ref T[,] _array, Action<T[,] ,int, int> OnEachtArray)
    {
        for (int x = 0; x < _array.GetLength(0); x++)
        {
            for (int y = 0; y < _array.GetLength(1); y++)
            {
                OnEachtArray?.Invoke(_array,x,y);
            }
        }
    }*/

    void RasterizeHeightMap(ref float[,] _array)
    {
        float _maxValue = maxValue;
        float _minValue = minValue;
        if (!useMinMax)
        {
            _maxValue = maxMapValue;
            _minValue = minMapValue;
        }
        RasterizationSettings _settings = new RasterizationSettings(_minValue, _maxValue);      
        OnEachMapCase(ref _array, (a, b, c) => CaseRasterization(ref a, b, c, _settings));
    }

    void CaseRasterization(ref float[,] _array, int _x, int _y, RasterizationSettings _settings)
    {
        float _val = _array[_x, _y];
        if (_val > _settings.maxValue)
        {
            _val = _settings.maxValue;
        } else if (_val < _settings.minValue)
        {
            _val = _settings.minValue;
        }

        _array[_x, _y] = MT_MathTools.Cross(_val, _settings.maxValue, _settings.minValue);
    }

    void OnEachMapCase<T>(ref T[,] _array, Action<T[,], int, int> OnEachArray)
    {
        for (int x = 0; x < _array.GetLength(0); x++)
        {
            for (int y = 0; y < _array.GetLength(1); y++)
            {
                OnEachArray?.Invoke(_array, x, y);
            }
        }
    }

    void DisplayTwoDimentionalArray<T>(T[,] _array)
    {
        EditoolsLayout.Horizontal(true);
        for (int x = 0; x < _array.GetLength(0); x++)
        {
            EditoolsLayout.Vertical(true);
            for (int y = 0; y < _array.GetLength(1); y++)
            {
                GUILayout.Label($"[{_array[x,y]}]");
            }
            EditoolsLayout.Vertical(false);
        }
        EditoolsLayout.Horizontal(false);
    }

    public struct RasterizationSettings
    {
        public RasterizationSettings(float _minVal, float _maxVal)
        {
            minValue = _minVal;
            maxValue = _maxVal;
        }
        public float maxValue;
        public float minValue;
    }

}
