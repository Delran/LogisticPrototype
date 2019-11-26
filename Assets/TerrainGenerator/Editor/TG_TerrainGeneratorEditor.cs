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
