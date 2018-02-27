using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;



public class PlatHandler : MonoBehaviour
{

    public class UE_BtnTrigger : UnityEvent<int> { };
    public delegate void BtnTrigger(int id);
    public delegate void PlatEvent();

    public int id;
    public PlatEvent onInitialize;
    public PlatEvent Show;
    public PlatEvent HardShow;
    public PlatEvent Hide;
    public PlatEvent HardHide;
    public PlatEvent onFreeze;
    public PlatEvent onUnfreeze;
    public BtnTrigger onBtnTrigger;

    [SerializeField]
    private UnityEvent _onInitilize=new UnityEvent();
    [SerializeField]
    private UnityEvent _Show = new UnityEvent();
    [SerializeField]
    private UnityEvent _HardShow = new UnityEvent();
    [SerializeField]
    private UnityEvent _Hide = new UnityEvent();
    [SerializeField]
    private UnityEvent _HardHide = new UnityEvent();
    [SerializeField]
    private UnityEvent _onFreeze = new UnityEvent();
    [SerializeField]
    private UnityEvent _onUnfreeze = new UnityEvent();
    [SerializeField]
    private UE_BtnTrigger _onBtnTrigger = new UE_BtnTrigger();

    [HideInInspector]
    public bool isFreeze;

    bool _isShow;
    public bool isShow { get { return _isShow; } }

    List<BtnHandler> btnList;

    public void Initialize()
    {
        btnList = new List<BtnHandler>();
        Show += _Show.Invoke;
        HardShow += _HardShow.Invoke;
        Hide += _Hide.Invoke;
        HardHide += _HardHide.Invoke;
        onFreeze += _onFreeze.Invoke;
        onUnfreeze += _onUnfreeze.Invoke;
        onBtnTrigger += _onBtnTrigger.Invoke;
        onInitialize += _onInitilize.Invoke;
        foreach (BtnHandler btn in btnList)
        {
            btn.Initialize();
        }
        TakeManager.Instance().AddPlat(this);
        InitilzeComponent();
        onInitialize();
    }

    void InitilzeComponent()
    {
        foreach (IUIComponent compt in GetComponents<IUIComponent>())
        {
            compt.Initialize();
        }
    }

    public void AddButton(BtnHandler btn)
    {
        try
        {
            BtnHandler target = btnList.Find((x) => x.ID == btn.ID);
            if (target != null)
            {
                throw new Exception(string.Format("按鈕ID重覆 /n{0}/n{1}", btn.transform.GetPath(), target.transform.GetPath()));
            }
            btnList.Add(btn);

        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            throw;
        }
    }

    public void RemoveButton(int id)
    {
        BtnHandler target = btnList.Find((x) => x.ID == id);
        try
        {
            if (target != null)
            {
                btnList.Remove(target);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public BtnHandler GetButton(int id)
    {
        try
        {
            BtnHandler target = btnList.Find((x) => x.ID == id);
            return target;
        }
        catch (Exception)
        {
            Debug.Log("按鈕遺失或未註冊 id = " + id);
            throw;
        }
    }
    
    public void Show()
    {
        if (!isFreeze)
        {
            if (!_isShow)
            {
                _isShow = true;
                if (_Show != null)
                {
                    Show();
                }
            }
        }
    }

    public void HardShow()
    {
        _isShow = true;
        if (HardShow != null)
        {
            HardShow();
        }
    }
    
    public void Hide()
    {
        if (!isFreeze)
        {
            if (_isShow)
            {
                _isShow = false;
                if (_Hide != null)
                {
                    Hide();
                }
            }
        }
    }

    public void HardHide()
    {
        _isShow = false;
        if (_HardHide != null)
        {
            HardHide();
        }
    }

}
