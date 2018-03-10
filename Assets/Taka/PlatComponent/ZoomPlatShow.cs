using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ZoomPlatShow : IPlatComponent
{

    #region PlatAnimaEvent
    public delegate void PlatAnimaEvent();
    #endregion
    float duration;
    Ease showEase, hideEase;
    Transform transform;

    public event PlatAnimaEvent onShowComplete;
    public event PlatAnimaEvent onHideComplete;

    public ZoomPlatShow(float duration, Ease showEase, Ease hideEase)
    {
        this.duration = duration;
        this.showEase = showEase;
        this.hideEase = hideEase;
    }

    public void Initialize(PlatHandler handler)
    {
        transform = handler.transform;
        handler.onHardShow += HardShow;
        handler.onHardHide += HardHide;
        handler.onShow += Show;
        handler.onHide += Hide;
    }

    public void HardShow()
    {
        transform.localScale = Vector3.one;
        transform.gameObject.SetActive(true);
        if (onShowComplete != null)
        {
            onShowComplete();
        }
    }

    public void HardHide()
    {
        transform.localScale = Vector3.zero;
        transform.gameObject.SetActive(false);
        if (onHideComplete != null)
        {
            onHideComplete();
        }
    }

    public void Show()
    {
        transform.DOScale(1, duration).SetEase(showEase).OnStart(() =>
        {
            transform.gameObject.SetActive(true);
            if (onShowComplete != null)
            {
                onShowComplete();
            }
        });
    }
    public void Hide()
    {
        transform.DOScale(0, duration).SetEase(hideEase).OnComplete(() =>
        {
            transform.gameObject.SetActive(false);
            if (onHideComplete != null)
            {
                onHideComplete();
            }
        });
    }
}
