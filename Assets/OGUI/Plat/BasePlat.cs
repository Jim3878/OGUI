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

    protected abstract void InitialPlatContent();

    public PlatContent GetPlatContent()
    {
        InitialPlatContent();
        /*
         * 按鈕先初始化時便會提取content,因此得在視窗初始化之前先初始化content
         */
        return _content;
    }
    
}
