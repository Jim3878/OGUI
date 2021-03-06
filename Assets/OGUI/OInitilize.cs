﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class OInitilize : MonoBehaviour {

    private void Awake()
    {
        InitialButton();
        InitialPlat();
    }

    void InitialButton()
    {
        var childs = transform.GetComponentsInChildren<BaseButton>(true);
        Debug.Log(childs.Length);
        foreach (BaseButton btn in childs)
        {
            btn.Initialize();
        }
    }

    void InitialPlat()
    {
        var childs = transform.GetComponentsInChildren<BasePlat>(true);
        

        foreach (BasePlat plat in childs)
        {
            plat.Initialize();
        }
    }
}
