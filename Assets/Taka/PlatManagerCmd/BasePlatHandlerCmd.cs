using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DG.Tweening;

public abstract class BasePlatHandlerCmd  {

    protected Action onComplete;
    protected Action onStart;

    public BasePlatHandlerCmd()
    {
        onComplete = () => { };
        onStart = () => { };
    }
    
    public BasePlatHandlerCmd OnComplete(Action action)
    {
        onComplete = action;
        return this;
    }

    public BasePlatHandlerCmd OnStart(Action action)
    {
        onStart = action;
        return this;
    }

    public void Excute(PlatHandler handler)
    {
        onStart();
        SelfExcute(handler);
    }

    protected abstract void SelfExcute(PlatHandler handler);
}
