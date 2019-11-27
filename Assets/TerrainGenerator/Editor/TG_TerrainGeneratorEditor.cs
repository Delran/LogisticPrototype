using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EditoolsUnity;
using UnityEditor;

[CustomEditor(typeof(TG_TerrainGenerator))]
public class TG_TerrainGeneratorEditor : EditorCustom<TG_TerrainGenerator>
{
    bool displayArray = true;

    float[,] heightMap = null;

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
        EditoolsButton.Button("Generate Height map", Color.white, GenerateHeightMap);
        EditoolsLayout.Space();

        displayArray = EditoolsLayout.Foldout(displayArray, "Heightmap Content");
        if (displayArray)
        {
            if (heightMap == null)
            {
                EditoolsBox.HelpBoxError("No valid heightmap");
            }
            else
            {
                DisplayTwoDimentionalArray(heightMap);
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
        heightMap = TG_HeightMapGenerator.DiamondSquareNoiseMap(eTarget.power, _initialValues, eTarget.randomRange);
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

}
