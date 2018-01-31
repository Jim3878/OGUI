using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class ZoomPlat : BasePlat {

    public int ID;
    public Transform body;
    

    protected override void SelfInitalize()
    {
        if (body == null)
        {
            body = transform;
        }
        Debug.Log("Initialize");
        _content = new PlatContent(ID, new ZoomPlatAnima(body).SetAllDuration(0.5f).SetAllEase(Ease.OutBack));

        PlatManager.Instance().AddPlatContent(_content);
    }
}
