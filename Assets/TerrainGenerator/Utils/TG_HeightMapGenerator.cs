﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class TG_HeightMapGenerator 
{
    public static float[,] DiamondSquareNoiseMap(int _size, float[,] _initialValue, int _randomRange, ref float _max, ref float _min)
    {
        if (_initialValue.GetLength(0) < 2 || _initialValue.GetLength(1) < 2) throw new Exception("Wrong initial array for diamond square height map generation");
        if (_initialValue.GetLength(0) > 2 || _initialValue.GetLength(1) > 2)
            Debug.Log("Initial array for diamond square height map generation is bigger that expected\nOnly the first two values of each dimension will be taken into account");

        //_power = _power > 16 ? 16 : _power;

        //Map must be of equal width and length for the diamond square algorithm to work as expected 
        float[,] _map = new float[_size, _size];

        int _side = _size - 1;

        _max = 0;
        _min = 0;

        //Initializing borders of the map with the initial values array
        for (int i = 0; i < 2; i++)
            for (int j = 0; j < 2; j++)
            {
                _map[i * _side, j * _side] = _initialValue[i, j];
                _max = _max < _initialValue[i, j] ? _initialValue[i, j] : _max;
                _min = _min > _initialValue[i, j] ? _initialValue[i, j] : _min;
            }


        for (; _side >= 2; _side /=2, _randomRange/=2 )
        {
            int _step = _side / 2;
            SquareStep(_side, _step, _randomRange, _size, ref _map, ref _min, ref _max);
            DiamondStep(_side, _step, _randomRange, _size, ref _map, ref _min, ref _max);
        }

        return _map;
    }

    static void SquareStep(int _side, int _step, int _randomRange, int _size, ref float[,] _map, ref float _min, ref float _max)
    {
        for (int i = 0; i < _size - 1; i+= _side)
        {
            for (int j = 0; j < _size-1; j+= _side)
            {
                float _average = _map[i, j] + _map[i+_step, j] + _map[i, j+_step] + _map[i+_step, j+_step];
                _average /= 4;
                _average += UnityEngine.Random.Range(-_randomRange, _randomRange);
                _map[i + _step, j + _step] = _average;
                _max = _max < _average ? _average : _max;
                _min = _min > _average ? _average : _min;
            }
        }
    }

    static void DiamondStep(int _side, int _step, int _randomRange, int _size, ref float[,] _map, ref float _min, ref float _max)
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

                _max = _max < _average ? _average : _max;
                _min = _min > _average ? _average : _min;
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
