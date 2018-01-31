using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class OInitilize : MonoBehaviour {

    private void Awake()
    {
        var childs= transform.GetComponentsInChildren<BasePlat>(true);
        Debug.Log(childs.Length);
        foreach(BasePlat plat in childs)
        {
            plat.Initialize();
        }
    }
}
