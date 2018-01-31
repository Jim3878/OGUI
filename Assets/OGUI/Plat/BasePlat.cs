using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.Events;

public abstract class BasePlat :MonoBehaviour, IPlat {

    public UnityEvent onInitialize;

    protected PlatContent _content;
    public void Initialize()
    {
        
        SelfInitalize();
        onInitialize.Invoke();
    }

    protected abstract void SelfInitalize();

    public PlatContent GetPlatContent()
    {
        return _content;
    }
    
}
