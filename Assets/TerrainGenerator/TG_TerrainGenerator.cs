using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TG_TerrainGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public Version Version { get; set; } = new Version(0,0,1);

    [HideInInspector]
    [SerializeField] public float cornerUpLeft = 0;
    [HideInInspector]
    [SerializeField] public float cornerUpRight = 0;
    [HideInInspector]
    [SerializeField] public float cornerDownLeft = 0;
    [HideInInspector]
    [SerializeField] public float cornerDownRight = 0;
    [HideInInspector]
    [SerializeField] public int power = 0;
    [HideInInspector]
    [SerializeField] public int mapSize = 0;
    [HideInInspector]
    [SerializeField] public int randomRange = 0;
    [HideInInspector]
    [SerializeField] public float minValue = 0;
    [HideInInspector]
    [SerializeField] public float maxValue = 0;
    [HideInInspector]
    [SerializeField] public float minMapValue = 0;
    [HideInInspector]
    [SerializeField] public float maxMapValue = 0;
    [HideInInspector]
    [SerializeField] public bool useMinMax = false;

    [SerializeField] public float[,] heightMap = null;

    #region Custom Methods
    private void Init()
    {

    }
    #endregion


    #region Unity Methods
    void Start() => Init();

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion
}
