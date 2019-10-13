using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEXTURE_2D
{
    #region checker
    public static Texture2D checker_1D(Color a, Color b)
    {
        Texture2D texture2D = new Texture2D(2, 1);

        texture2D.SetPixel(0, 0, a);
        texture2D.SetPixel(1, 0, b);
        texture2D.filterMode = FilterMode.Point;

        texture2D.Apply();
        return texture2D;
    }

    public static Texture2D checker_2D(Color a, Color b)
    {
        Texture2D texture2D = new Texture2D(2, 2);

        texture2D.SetPixel(0, 0, a);
        texture2D.SetPixel(1, 0, b);
        texture2D.SetPixel(0, 1, b);
        texture2D.SetPixel(1, 1, a);
        texture2D.filterMode = FilterMode.Point;

        texture2D.Apply();
        return texture2D;
    } 
    #endregion


}
