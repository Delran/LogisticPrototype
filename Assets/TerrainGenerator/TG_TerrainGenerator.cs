﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TG_TerrainGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public Version Version { get; set; } = new Version(0,0,1);

    [SerializeField] public float cornerUpLeft = 0;
    [SerializeField] public float cornerUpRight = 0;
    [SerializeField] public float cornerLeft = 0;
    [SerializeField] public float cornerLeft = 0;
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
