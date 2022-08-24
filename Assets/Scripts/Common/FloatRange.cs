using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FloatRange
{
    public float Min;
    public float Max;

    public float Random()
    {
        return UnityEngine.Random.Range(Min, Max);
    }
}
