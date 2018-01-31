using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

[Serializable]
public class BaseButton :MonoBehaviour, IButton
{

    public BaseButton up, left, right, down;
    public BasePlat plat;
    protected BtnContent content = new BtnContent();

    /*content在執行Inialize時就必需加入上下左右BtnContent
     * 因此content必需在intialize之前便初始化完畢
     * 否則在加入上下左右時，其它BaseButton的BtnContent還未初始化
     */
    public void Initialize()
    {
        
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
            }else
            {
                Debug.LogError("button miss plat in "+ transform.parent.gameObject.name + "/" + gameObject.name);
            }
        }
    }


    public BtnContent GetBtnContent()
    {
        return content;
    }


}
