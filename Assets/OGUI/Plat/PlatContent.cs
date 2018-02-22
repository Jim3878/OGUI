using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Events;
using System;
using DG.Tweening;

/// <summary>
/// 記錄與控管視窗資訊
/// </summary>
[System.Serializable]
public class PlatContent
{

    public int id;
    public EventHandler onShow;
    public EventHandler onHide;
    public EventHandler onFreezen;

    public bool isFreeze;
    public bool isShow { get { return _isShow; } }
    public BasePlat basePlat { get { return _basePlat; } }
    public List<BtnContent> btnList;

    BasePlat _basePlat;
    bool _isShow;
    IPlatAnima platAnima;

    public PlatContent(int id, BasePlat basePlat, IPlatAnima platAnima)
    {
        this.id = id;
        this.platAnima = platAnima;
        this._basePlat = basePlat;
    }

    public void InitialBtnList()
    {
        if (btnList == null)
        {
            btnList = new List<BtnContent>();
        }
    }

    public bool AddButton(BtnContent btn)
    {
        InitialBtnList();
        BtnContent target = btnList.Find((x) => x.ID == btn.ID);
        if (target != null)
        {
            Debug.LogError(string.Format("按鈕ID重覆 /n{0}/n{1}", btn.baseButton.transform.GetPath(), target.baseButton.transform.GetPath()));
            return false;
        }
        else
        {
            btnList.Add(btn);
            return true;
        }
    }

    public bool RemoveButton(int id)
    {
        InitialBtnList();
        BtnContent target = btnList.Find((x) => x.ID == id);
        if (target != null)
        {
            btnList.Remove(target);
            return true;
        }
        else
        {
            return false;
        }
    }

    public BtnContent GetButton(int id)
    {
        InitialBtnList();
        BtnContent target = btnList.Find((x) => x.ID == id);

        return target;
    }

    public void Show()
    {
        Show(() => { });
    }

    public void Show(Action onComplete)
    {
        if (!isFreeze)
        {
            if (!_isShow)
            {
                _isShow = true;
                platAnima.Show(onComplete);
            }
        }
    }

    public void HardShow()
    {
        platAnima.HardShow();
    }

    public void Hide()
    {
        Hide(() => { });
    }

    public void Hide(Action onComplete)
    {
        if (!isFreeze)
        {
            if (_isShow)
            {
                _isShow = false;
                platAnima.Hide(onComplete);
            }
        }
    }

    public void HardHide()
    {
        platAnima.HardHide();
    }


}
