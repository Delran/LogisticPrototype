using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MT_MathTools
{
    /// <summary>
    /// Return the real Modulus of nb modulus mod
    /// -1 % 5 = 5
    /// </summary>
    /// <param name="_nb">Number to get the modulus from</param>
    /// <param name="_mod">Modulus</param>
    /// <returns></returns>
    public static int Mod(int _nb, int _mod)
    {
        return (_nb % _mod + _mod) % _mod;
    }

    /// <summary>
    /// Translate a number in min/max range to the _rangeMin/_rangeMax range
    /// Translate between 0 and 1 by default
    /// </summary>
    /// <param name="_nb">Number to be translated</param>
    /// <param name="_max">High range value</param>
    /// <param name="_min">Low range value</param>
    /// <param name="_rangeMin">Low range to be translated</param>
    /// <param name="_rangeMax">Hight range to be translated</param>
    /// <returns></returns>
    public static float Cross(float _nb, float _max, float _min, float _rangeMin = 0, float _rangeMax = 1)
    {
        _max -= _min;
        _rangeMax -= _rangeMin;
        float _val = _nb - _min;
        float _mult = _val / _max;
        return _rangeMax * _mult + _rangeMin;
    }
}
