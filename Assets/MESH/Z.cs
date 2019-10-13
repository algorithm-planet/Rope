using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Z
{
    #region Rotate
    public static Vector3 Rotate(Vector3 v, Vector3 k, float Angle)
    {
        Vector3 x = k * dot(v, k);

        Vector3 n1 = cross(k, v),
                n2 = cross(-k, n1);

        float A = Angle * Mathf.Deg2Rad;

        Vector3 y = n2 * Mathf.Cos(A) + n1 * Mathf.Sin(A);
        return x + y;
    } 
    #endregion

    #region axis
    public static Vector3 axis(Vector3 up, int i = 0)
    {
        GameObject G = GameObject.Find("axis");
        if (!(bool)G)
            G = new GameObject("axis");

        Transform transform = G.transform;

        transform.up = up;
        if (i == 0)
            return transform.right;
        else
            return transform.forward;
    } 
    #endregion

    public static float dot(Vector3 a , Vector3 b)
    {
        return a.x * b.x +
               a.y * b.y +
               a.z * b.z;
    }

    public static Vector3 cross(Vector3 a , Vector3 b)
    {
        return -Vector3.Cross(a, b);
    }

    public static Vector3 lerp(Vector3 a , Vector3 b , float t)
    {
        Vector3 n = b - a;
        return a + n * t;
    }
    public static float lerp(float a, float b, float t)
    {
        float n = b - a;
        return a + n * t;
    }

    public static bool ear(Vector2 o, Vector2 n1, Vector2 n2, Vector2 pos)
    {
        float C1 = -n2.x / n2.y,
              C2 = -n1.x / n1.y;

        float A = pos.x - o.x,
              B = pos.y - o.y;

        float u = (A + C1 * B) / (n1.x + C1 * n1.y),
              v = (A + C2 * B) / (n2.x + C2 * n2.y);

        if (u > 0f && v > 0f)
            if (u + v < 1f)
                return false;

        return true;
    }
}
