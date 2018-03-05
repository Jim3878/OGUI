using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ZoomPlatShow : IPlatComponent {

    float duration;
    Ease showEase,hideEase;
    Transform transform;

    public ZoomPlatShow(float duration,Ease showEase,Ease hideEase)
    {
        this.duration = duration;
        this.showEase = showEase;
        this.hideEase = hideEase;
    }

    public void Initialize(PlatHandler handler)
    {
        transform = handler.transform;
        handler.hardShow += HardShow;
        handler.hardHide += HardHide;
        handler.show += Show;
        handler.hide += Hide;
    }

    public void HardShow()
    {
        transform.localScale = Vector3.one;
        transform.gameObject.SetActive(true);
    }

    public void HardHide()
    {
        transform.localScale = Vector3.zero;
        transform.gameObject.SetActive(false);
    }

    public void Show()
    {
        transform.DOScale(1, duration).SetEase(showEase).OnStart(()=> {
            transform.gameObject.SetActive(true);
        });
    }
    public void Hide()
    {
        transform.DOScale(0, duration).SetEase(hideEase).OnComplete(()=> {
            transform.gameObject.SetActive(false);
        });
    }
}
