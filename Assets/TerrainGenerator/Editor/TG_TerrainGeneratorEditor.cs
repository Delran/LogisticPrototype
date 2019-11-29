using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EditoolsUnity;
using UnityEditor;
using System;

[CustomEditor(typeof(TG_TerrainGenerator))]
public class TG_TerrainGeneratorEditor : EditorCustom<TG_TerrainGenerator>
{
    bool displayArray = true;

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


        EditoolsLayout.Space();
        EditoolsButton.Button("Generate Height map", Color.white, GenerateHeightMap);
        EditoolsLayout.Space();

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
        eTarget.heightMap = TG_HeightMapGenerator.DiamondSquareNoiseMap(eTarget.power, _initialValues, eTarget.randomRange, ref eTarget.maxMapValue, ref eTarget.minMapValue);

        RasterizeHeightMap(ref eTarget.heightMap);
    }


    void CreateMap()
    {
        GameObject _objectMap = GameObject.CreatePrimitive(PrimitiveType.Plane);

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

    void OnEachMapCase<T>(ref T[,] _array, Action<T[,], int, int> OnEachtArray)
    {
        for (int x = 0; x < _array.GetLength(0); x++)
        {
            for (int y = 0; y < _array.GetLength(1); y++)
            {
                OnEachtArray?.Invoke(_array, x, y);
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
