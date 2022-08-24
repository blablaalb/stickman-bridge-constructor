using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Collider2DExtensions
{
    public static bool SeenByCamera(this Collider2D collider)
    {
        var camera = Camera.main;
        var planes = GeometryUtility.CalculateFrustumPlanes(camera);
        if (GeometryUtility.TestPlanesAABB(planes, collider.bounds)) return true;

        return false;
    }
}
