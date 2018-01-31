using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DG.Tweening;

public abstract class BasePlatManagerCmd  {

    protected Action onComplete;
    protected Action onStart;

    public BasePlatManagerCmd()
    {
        onComplete = () => { };
        onStart = () => { };
    }
    
    public BasePlatManagerCmd OnComplete(Action action)
    {
        onComplete = action;
        return this;
    }

    public BasePlatManagerCmd OnStart(Action action)
    {
        onStart = action;
        return this;
    }

    public void Excute(PlatManager manager)
    {
        onStart();
        SelfExcute(manager);
    }

    protected abstract void SelfExcute(PlatManager manager);
}
