using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class TG_HeightMapGenerator 
{
    public static float[,] DiamondSquareNoiseMap(int _mapSize, float[,] _initialValue, int _randomRange)
    {
        if (_initialValue.GetLength(0) < 2 || _initialValue.GetLength(1) < 2) throw new Exception("Wrong initial array for diamond square height map generation");
        if (_initialValue.GetLength(0) > 2 || _initialValue.GetLength(1) > 2)
            Debug.Log("Initial array for diamond square height map generation is bigger that expected\nOnly the first two values of each dimension will be taken into account");

        //Making sure the _mapSize is an odd number
        _mapSize = _mapSize % 2 == 0 ? _mapSize + 1 : _mapSize;

        //Map must be of equal width and length for the diamond square algorithm to work as expected 
        float[,] _map = new float[_mapSize, _mapSize];

        //Initializing borders of the map with the initial values array
        for (int i = 0; i < 2; i++)
            for (int j = 0; j < 2; j++)
                _map[i * _mapSize, j * _mapSize] = _initialValue[i, j];

        //int _step = ;

        for (int _side = _mapSize - 1; _side >= 2; _side /=2, _randomRange/=2 )
        {
            int _step = _side / 2;
            SquareStep(_side, _step, _randomRange, _mapSize, ref _map);
            DiamondStep(_side, _step, _randomRange, _mapSize, ref _map);
        }


        return _map;
    }

    static void SquareStep(int _side, int _step, int _randomRange, int _size, ref float[,] _map)
    {
        for (int i = 0; i < _size - 1; i+= _step)
        {
            for (int j = (i+_step)%_side; j < _size-1; j+=_side)
            {
                float _average = _map[i, j] + _map[i+_step, j] + _map[i, j] + _map[i, j];
            }
        }
    }

    static void DiamondStep(int _side, int _step, int _randomRange, int _size, ref float[,] _map)
    {

    }
}
