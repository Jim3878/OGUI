using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility
{


}

public static class StaticUtility
{
    public static string GetPath(this Transform go)
    {
        string path = go.gameObject.name;
        Transform parent = go.parent;
        while (parent.parent != null)
        {
            path = parent.gameObject.name + "/" + path;
            parent = go.parent;
        }
        return path;
    }
}
