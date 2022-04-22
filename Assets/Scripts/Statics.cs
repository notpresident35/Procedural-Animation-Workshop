using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubic
{
    public static float In(float k)
    {
        return k * k * k;
    }

    public static float Out(float k)
    {
        return 1f + ((k -= 1f) * k * k);
    }

    public static float InOut(float k)
    {
        if ((k *= 2f) < 1f) return 0.5f * k * k * k;
        return 0.5f * ((k -= 2f) * k * k + 2f);
    }
}
