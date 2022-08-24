using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Extension
{
    public static float MeasureDistanceX(this Vector3 a, Vector3 b)
    {
        return Mathf.Abs(b.x - a.x);
    }
}
