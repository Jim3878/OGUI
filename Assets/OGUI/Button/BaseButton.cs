using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using System;

[Serializable]
public class BaseButton :MonoBehaviour, IButton
{
    public int id;
    public BasePlat plat;
    [Header("按鈕導向")]
    public BaseButton up;
    public BaseButton left, right, down;
    public UnityEvent onInitialize = new UnityEvent();
    public UnityEvent onBtnStateChange=new UnityEvent();
    protected BtnContent content = new BtnContent();
    /*content在執行Inialize時就必需加入上下左右BtnContent
     * 因此content必需在intialize之前便初始化完畢
     * 否則在加入上下左右時，其它BaseButton的BtnContent還未初始化
     */

    public void Initialize()
    {
        content.onChangeStae += OnBtnStateChange;
        content.SetBaseButton(this);
        content.ID = id;

        if (up != null) content.up = up.GetBtnContent();
        if (left != null) content.left = left.GetBtnContent();
        if (right != null) content.right = right.GetBtnContent();
        if (down != null) content.down = down.GetBtnContent();

        if (plat == null)
        {
            var plat = transform.parent.GetComponent<BasePlat>();
            if (plat != null)
            {
                this.plat = plat;
                plat.GetPlatContent().AddButton(this.content);

            }else
            {
                Debug.LogError("button miss plat in "+ transform.parent.gameObject.name + "/" + gameObject.name);
            }
        }
        
        var components = GetComponents<IUIComponent>();
        foreach(var component in components)
        {
            component.Initialize();
        }
    }

    protected virtual void SelfInitialize() { }


    public BtnContent GetBtnContent()
    {
        return content;
    }

    void OnBtnStateChange(object sender,EventArgs e)
    {
        onBtnStateChange.Invoke();
    }

}
