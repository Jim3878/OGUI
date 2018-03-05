using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility
{
    public static Texture2D MakeTexture(Color color ,int width,int height)
    {
        var pix = new Color[width * height];

        for (int i = 0; i < pix.Length; i++)
        {
            pix[i] = color;
        }

        var result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }

}

public static class StaticUtility
{
    public static string GetPath(this Transform go)
    {
        string path = go.gameObject.name;
        Transform current = go;
        while (current.parent != null)
        {
            path = current.parent.gameObject.name + "/" + path;
            current = go.parent;
        }
        return path;
    }
}
