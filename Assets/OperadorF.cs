using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperadorF: MonoBehaviour
{
    public static float AND(float A, float B)
    {
        return Mathf.Min(A, B);
    }

    public static float OR(float A, float B)
    {
        return Mathf.Max(A, B);
    }

    public static float NOT(float A)
    {
        return 1 - A;
    }


}
