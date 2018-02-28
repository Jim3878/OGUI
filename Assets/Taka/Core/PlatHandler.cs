using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;



public class PlatHandler : MonoBehaviour
{
    [Serializable]
    public class UE_BtnTrigger : UnityEvent<int> { };
    public delegate void BtnTrigger(int id);
    public delegate void PlatEvent();

    public int id;

    //初始化用兩者相同、platComponentList是針對Monobehavior而設
    /// <summary>
    /// Component需實作IPlatComponent才會初始化
    /// </summary>
    public List<MonoBehaviour> platComponentList = new List<MonoBehaviour>();
    protected List<IPlatComponent> _platComponentList = new List<IPlatComponent>();
    public List<BtnHandler> btnHandlerList = new List<BtnHandler>();

    public PlatEvent onInitialize;
    public PlatEvent show;
    public PlatEvent hardShow;
    public PlatEvent hide;
    public PlatEvent hardHide;
    public PlatEvent onFreeze;
    public PlatEvent onUnfreeze;
    public BtnTrigger onBtnTrigger;
    [SerializeField]
    private UnityEvent _onInitilize = new UnityEvent();
    [SerializeField]
    private UE_BtnTrigger _onBtnTrigger = new UE_BtnTrigger();

    private bool _isFreeze;
    [HideInInspector]
    public bool isFreeze
    {
        get
        {
            return _isFreeze;
        }
        set
        {
            if (value != _isFreeze)
            {
                _isFreeze = value;
                if (value)
                {
                    if (onFreeze != null)
                    {
                        onFreeze();
                    }
                }
                else
                {
                    if (onUnfreeze != null)
                    {
                        onUnfreeze();
                    }
                }
            }
        }
    }

    bool _isShow;
    public bool isShow { get { return _isShow; } }

    List<BtnHandler> btnList;

    public void Initialize()
    {
        btnList = new List<BtnHandler>();
        onBtnTrigger += _onBtnTrigger.Invoke;
        onInitialize += _onInitilize.Invoke;
        
        InitilizeBtnHandler();
        InitilzeComponent();
        TakeManager.Instance().AddPlat(this);
        
        
        onInitialize();
    }

    void InitilzeComponent()
    {
        foreach (MonoBehaviour compt in platComponentList)
        {
            try
            {
                if (compt is IPlatComponent)
                {
                    (compt as IPlatComponent).Initialize(this);
                }
                else
                {
                    throw new Exception("元件未實作IPlatComponent : " + compt.transform.GetPath());
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
        foreach (IPlatComponent compt in _platComponentList)
        {
            compt.Initialize(this);
        }
    }

    void InitilizeBtnHandler()
    {
        try
        {
            foreach (BtnHandler handler in btnHandlerList)
            {
                handler.Initialize(this);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    public void AddButton(BtnHandler btn)
    {

        if (btnList.Find((x) => x.ID == btn.ID) != null)
        {
            btnList.RemoveAll((x) => x.ID == btn.ID);
        }
        btnList.Add(btn);
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
                if (show != null)
                {
                    show();
                }
            }
        }
    }

    public void HardShow()
    {
        _isShow = true;
        if (hardShow != null)
        {
            hardShow();
        }
    }

    public void Hide()
    {
        if (!isFreeze)
        {
            if (_isShow)
            {
                _isShow = false;
                if (hide != null)
                {
                    hide();
                }
            }
        }
    }

    public void HardHide()
    {
        _isShow = false;
        if (hardHide != null)
        {
            hardHide();
        }
    }

}
