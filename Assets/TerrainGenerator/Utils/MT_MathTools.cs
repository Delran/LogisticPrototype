using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MT_MathTools
{
    public static int Mod(int _nb, int _mod)
    {
        return (_nb % _mod + _mod) % _mod;
    }
}
