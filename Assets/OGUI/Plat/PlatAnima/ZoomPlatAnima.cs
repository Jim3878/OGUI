using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class ZoomPlatAnima : BaseTweenPlatAnima
{
    

    public ZoomPlatAnima(Transform body):base(body)
    {
    }

    public override void HardHide()
    {
        body.localScale = Vector3.zero;
    }

    public override void HardShow()
    {
        body.localScale = Vector3.one;
    }

    public override void Hide(Action onComplete)
    {

        Tween t= body.DOScale(Vector3.zero, hideDuration).OnComplete(() => onComplete());
        if (this.hideEase == Ease.INTERNAL_Custom)
        {
            t.SetEase(hideCurve);
        }else
        {
            t.SetEase(hideEase);
        }
    }

    public override void Show(Action onComplete)
    {
        Tween t= body.DOScale(Vector3.one, showDuration).OnComplete(() => onComplete());
        if (this.showEase == Ease.INTERNAL_Custom)
        {
            t.SetEase(showCurve);
        }
        else
        {
            t.SetEase(showEase);
        }
    }
}
