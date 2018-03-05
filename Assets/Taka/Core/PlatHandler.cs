using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.IO;

public class PlatHandler : MonoBehaviour
{
    #region TYPE
    [Serializable]
    public class UE_BtnTrigger : UnityEvent<int> { };
    public delegate void BtnTrigger(int id);
    public delegate void PlatEvent();
    #endregion

    public int ID;
    protected List<IPlatComponent> _platComponentList = new List<IPlatComponent>();
    public List<BtnHandler> btnList = new List<BtnHandler>();

    public event PlatEvent onInitialize;
    public event PlatEvent show;
    public event PlatEvent hardShow;
    public event PlatEvent hide;
    public event PlatEvent hardHide;
    public event PlatEvent onFreeze;
    public event PlatEvent onUnfreeze;
    public event BtnTrigger onBtnTrigger;

    string _Log = "";

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
    List<BtnHandler> registedBtnList = new List<BtnHandler>();

    public virtual void Initialize()
    {
        InitilizeBtnHandler();
        InitilzeComponent();
        
        if (onInitialize != null)
        {
            onInitialize();
        }
        _Log += "\nInitialize()\n" + LogHelper.CallStack() + "\n\n";

        TakeManager.Instance().AddPlat(this);
    }

    void InitilzeComponent()
    {
        //foreach (MonoBehaviour compt in platComponentList)
        //{
        //    try
        //    {
        //        if (compt is IPlatComponent)
        //        {
        //            (compt as IPlatComponent).Initialize(this);
        //        }
        //        else
        //        {
        //            throw new Exception("元件未實作IPlatComponent : " + compt.transform.GetPath());
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Debug.Log(e);
        //    }
        //}
        foreach (IPlatComponent compt in _platComponentList)
        {
            compt.Initialize(this);
        }
    }

    void InitilizeBtnHandler()
    {
        try
        {
            foreach (BtnHandler handler in btnList)
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

        if (registedBtnList.Find((x) => x.ID == btn.ID) != null)
        {
            registedBtnList.RemoveAll((x) => x.ID == btn.ID);
        }
        registedBtnList.Add(btn);
        _Log += "\nAddButton( btn.id = " + btn.ID + ")\n" + LogHelper.CallStack() + "\n\n";
    }

    public void RemoveButton(int id)
    {
        BtnHandler target = registedBtnList.Find((x) => x.ID == id);
        try
        {
            if (target != null)
            {
                registedBtnList.Remove(target);
            }
            _Log += "\nRemoveButton( id = " + ID + " )\n" + LogHelper.CallStack() + " \n\n";
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
            BtnHandler target = registedBtnList.Find((x) => x.ID == id);

            return target;

        }
        catch (Exception)
        {
            Debug.Log("按鈕遺失或未註冊 id = " + id);
            throw;
        }
        finally
        {
            _Log += "GetButton( id = " + ID + " )\n" + LogHelper.CallStack() + " \n\n";
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
                    _Log += "\nShow() \n" + LogHelper.CallStack() + "\n\n";
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
            _Log += "\nHArdShow()\n" + LogHelper.CallStack() + " \n\n";
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
                    _Log += "\nHide() \n" + LogHelper.CallStack() + "\n\n";
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
            _Log += "\nHardHide()\n" + LogHelper.CallStack() + "\n\n";
        }
    }

    public void TriggerBtn(int id)
    {
        onBtnTrigger(id);
        _Log += "\nTriggerBtn( id = " + id + ")\n" + LogHelper.CallStack() + "\n\n";
    }

    [ContextMenu("Trace Log")]
    public void TraceLog()
    {
        string path = Application.persistentDataPath + "/" + transform.GetPath().Replace("/", "/") + ".TXT";
        using (StreamWriter sw = new StreamWriter(path))   //小寫TXT     
        {
            // Add some text to the file.
            sw.Write(_Log);
            sw.Close();
            Debug.Log("Log save to :" + path);
        }

    }

    [ContextMenu("Trace")]
    public void ttt()
    {
        Debug.Log(transform.GetPath());
    }
}
