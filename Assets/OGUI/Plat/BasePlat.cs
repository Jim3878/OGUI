using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.Events;

public class IntPassEventArgs : EventArgs
{
    public int id;
    public IntPassEventArgs(int id)
    {
        this.id = id;
    }
}

public abstract class BasePlat : MonoBehaviour, IPlat
{

    public UnityEvent onInitialize;

    protected PlatContent _content;

    public void Initialize()
    {
        SelfInitalize();
        onInitialize.Invoke();
        foreach(var component in GetComponents<IUIComponent>())
        {
            component.Initialize();
        }
    }

    public EventHandler<IntPassEventArgs> invokeBtn;

    protected abstract void SelfInitalize();

    protected abstract void InitialPlatContent();

    public void InvokeBtn(int id)
    {
        if (invokeBtn != null)
            invokeBtn(this, new IntPassEventArgs(id));
    }

    public PlatContent GetPlatContent()
    {
        InitialPlatContent();
        /*
         * 按鈕先初始化時便會提取content,因此得在視窗初始化之前先初始化content
         */
        return _content;
    }

}
