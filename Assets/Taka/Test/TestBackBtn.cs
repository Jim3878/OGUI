using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestBackBtn : BtnHandler {

    public override void Initialize(PlatHandler handler)
    {
        base.Initialize(handler);
        handler.onShow += OnShow;
        handler.onHide += OnHide;
        handler.onHardShow += OnHardShow;
        handler.onHardHide += OnHardHide;
    }

    void OnShow()
    {
        transform.DOMoveY(100, 0.5f);
    }

    void OnHide()
    {
        transform.DOMoveY(-100, 0.5f);
    }

    void OnHardHide()
    {
        transform.position -= new Vector3(0, 100);
    }

    void OnHardShow()
    {
        transform.position += new Vector3(0, 100);
    }
}
