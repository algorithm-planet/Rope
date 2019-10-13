using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier
{
    Vector3[] points;

    public Bezier(Vector3 a , Vector3 b , Vector3 c , Vector3 d)
    {
        points = new Vector3[4]
        {
            a , b , c , d
        };
    }

    public Vector3 pos(float t)
    {
        Vector3 a = quad(points[0], points[1], points[2], t),
                b = quad(points[1], points[2], points[3], t);

        return Z.lerp(a, b, t);
    }

    #region quad
    static Vector3 quad(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        Vector3 A = Z.lerp(a, b, t),
                B = Z.lerp(b, c, t);
        return Z.lerp(A, B, t);
    }
    #endregion
}
