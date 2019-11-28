﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class TG_HeightMapGenerator 
{
    public static float[,] DiamondSquareNoiseMap(int _power, float[,] _initialValue, int _randomRange)
    {
        if (_initialValue.GetLength(0) < 2 || _initialValue.GetLength(1) < 2) throw new Exception("Wrong initial array for diamond square height map generation");
        if (_initialValue.GetLength(0) > 2 || _initialValue.GetLength(1) > 2)
            Debug.Log("Initial array for diamond square height map generation is bigger that expected\nOnly the first two values of each dimension will be taken into account");

        _power = _power > 16 ? 16 : _power;
        int mapSize = (int)Mathf.Pow(2, _power) + 1;

        mapSize = mapSize % 2 == 0 ? mapSize + 1 : mapSize;

        //Map must be of equal width and length for the diamond square algorithm to work as expected 
        float[,] _map = new float[mapSize, mapSize];

        int _side = mapSize - 1;

        //Initializing borders of the map with the initial values array
        for (int i = 0; i < 2; i++)
            for (int j = 0; j < 2; j++)
            {
                _map[i * _side, j * _side] = _initialValue[i, j];
            }

        //int _step = ;

        for (; _side >= 2; _side /=2, _randomRange/=2 )
        {
            Debug.Log("ITERATION");
        int _step = _side / 2;
        SquareStep(_side, _step, _randomRange, mapSize, ref _map);
        DiamondStep(_side, _step, _randomRange, mapSize, ref _map);
            //int _step = _side / 2;
            //SquareStep(_side, _step, _randomRange, mapSize, ref _map);
        }


        return _map;
    }

    static void SquareStep(int _side, int _step, int _randomRange, int _size, ref float[,] _map)
    {
        for (int i = 0; i < _size - 1; i+= _side)
        {
            for (int j = 0; j < _size-1; j+= _side)
            {
                float _average = _map[i, j] + _map[i+_step, j] + _map[i, j+_step] + _map[i+_step, j+_step];
                _average /= 4;
                _average += UnityEngine.Random.Range(-_randomRange, _randomRange);
                _map[i + _step, j + _step] = _average;
            }
        }
    }

    static void DiamondStep(int _side, int _step, int _randomRange, int _size, ref float[,] _map)
    {
        for (int i = 0; i < _size - 1; i += _step)
        {
            //Debug.Log("Stepping in");
            //(i+_step)%_side
            for (int j = (i + _step) % _side; j < _size - 1; j += _side)
            {
                /*if (i == 0 && j != 0)
                {
                    Debug.Log(i + _step);
                    Debug.Log(i);
                }*/
                //Debug.Log(j);


                //Debug.Log(MT_MathTools.Mod(i + _step, _size));
                //Debug.Log(j);
                float _average = _map[MT_MathTools.Mod(i + _step, _size), j];

                /*Debug.Log(i - _step);
                Debug.Log(MT_MathTools.Mod(i - _step, _size));
                Debug.Log(j);*/
                _average += _map[MT_MathTools.Mod(i - _step, _size), j];

                //Debug.Log(MT_MathTools.Mod(i + _step, _size));
                _average += _map[i, MT_MathTools.Mod(j + _step, _size)];

                //Debug.Log(MT_MathTools.Mod(i + _step, _size));
                _average += _map[i, MT_MathTools.Mod(j - _step, _size)];
                _average /= 4;
                _average += UnityEngine.Random.Range(-_randomRange, _randomRange);

                /*if (i == 0)
                {
                    _map[_size - 1, j] = 30;
                } 
                if (j==0)
                {
                    _map[i, _size - 1] = 10;
                }*/
                if (_map[i, j] != 0)
                {
                    Debug.Log($"OVERIDING : [{i + _step}][{i}]");
                    Debug.Log($"VALUE : {_map[i + _step, i]}");
                }

                if (i == 0)
                {
                    _map[_size - 1, j] = _average;
                }
                if (j == 0)
                {
                    _map[i, _size -1] = _average;
                }

                _map[i, j] = _average;

                /*if (_map[i, i + _step] != 0)
                {
                    Debug.Log($"OVERIDING : [{i}][{i + _step}]");
                    Debug.Log($"VALUE : {_map[i, i + _step]}");
                }
                _map[i+_step, i] = 100;
                _map[i, i+ _step] = 50;*/
            }
        }
    }

    public enum GenerationMethods
    {
        DiamondSquare
    }
}
