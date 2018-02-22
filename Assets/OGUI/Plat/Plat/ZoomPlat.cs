using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class ZoomPlat : BasePlat
{

    public int ID;
    public Transform body;


    protected override void SelfInitalize()
    {
        
        InitialPlatContent();
        PlatManager.Instance().AddPlatContent(_content);
    }

    protected override void InitialPlatContent()
    {
        if (body == null)
        {
            body = transform;
        }
        if (_content == null)
        {
            _content = new PlatContent(ID, this, new ZoomPlatAnima(body).SetAllDuration(0.5f).SetAllEase(Ease.OutBack));
        }
    }
}
