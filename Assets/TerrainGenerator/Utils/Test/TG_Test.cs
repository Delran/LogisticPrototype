using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TG_Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TestInt();
    }


    void TestInt()
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
