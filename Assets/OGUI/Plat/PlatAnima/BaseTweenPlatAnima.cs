using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public abstract class BaseTweenPlatAnima : IPlatAnima
{


    public Transform body;
    public PlatContent content;
    public Ease showEase;
    public Ease hideEase;
    public AnimationCurve showCurve;
    public AnimationCurve hideCurve;
    public float showDuration;
    public float hideDuration;

    public BaseTweenPlatAnima(Transform body)
    {
        this.body = body;
    }

    public abstract void HardHide();

    public abstract void HardShow();

    public void Hide()
    {
        Hide(() => { });
    }

    public abstract void Hide(Action onComplete);

    public void Show()
    {
        Show(() => { });
    }

    public abstract void Show(Action onComplete);

    public BaseTweenPlatAnima SetShowEase(Ease ease)
    {
        this.showEase = ease;
        return this;
    }

    public BaseTweenPlatAnima SetShowEase(AnimationCurve curve)
    {
        this.showEase = Ease.INTERNAL_Custom;
        this.showCurve = curve;
        return this;
    }

    public BaseTweenPlatAnima SetHideEase(Ease ease)
    {
        this.hideEase = ease;
        return this;
    }

    public BaseTweenPlatAnima SetHideEase(AnimationCurve curve)
    {
        this.hideEase = Ease.INTERNAL_Custom;
        this.hideCurve = curve;
        return this;
    }

    public BaseTweenPlatAnima SetAllEase(Ease ease)
    {
        this.hideEase = this.showEase = ease;
        return this;
    }

    public BaseTweenPlatAnima SetAllEase(AnimationCurve curve)
    {
        this.hideEase = this.showEase = Ease.INTERNAL_Custom;
        this.showCurve = this.hideCurve = curve;
        return this;
    }

    public BaseTweenPlatAnima SetShowDuration(float duration)
    {
        this.showDuration = duration;
        return this;
    }

    public BaseTweenPlatAnima SetHideDuration(float duration)
    {
        this.hideDuration = duration;
        return this;
    }

    public BaseTweenPlatAnima SetAllDuration(float duration)
    {
        this.showDuration = this.hideDuration = duration;
        return this;
    }
}
