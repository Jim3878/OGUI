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
    public delegate void AddBtn(int id);
    public delegate void PlatEvent();

    #endregion

    public int ID;
    protected List<IPlatComponent> _platComponentList = new List<IPlatComponent>();
    [SerializeField]
    private List<BtnHandler> btnList = new List<BtnHandler>();
    public Dictionary<int, BtnHandler> registedBtnList = new Dictionary<int, BtnHandler>();
    public event PlatEvent onInitialize;
    public event PlatEvent onShow;
    public event PlatEvent onHardShow;
    public event PlatEvent onHide; 
    public event PlatEvent onHardHide;
    public event PlatEvent onFreeze;
    public event PlatEvent onUnfreeze;
    public event AddBtn onBtnAdd;
    public event BtnTrigger onBtnTrigger;

    string _log = "";

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
                _log += string.Format("setFreeze to {0}\n{1}\n\n", value, LogHelper.CallStack());
            }
        }
    }

    bool _isShow;
    public bool isShow { get { return _isShow; } }


    public virtual void Initialize()
    {
        InitilizeBtnHandler();
        InitilzeComponent();

        if (onInitialize != null)
        {
            onInitialize();
        }
        _log += "\nInitialize()\n" + LogHelper.CallStack() + "\n\n";

        TakeManager.Instance().AddPlat(this);
    }

    void InitilzeComponent()
    {
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
            _log += "InitilizeBtnHandler Error\n" + e + "\n\n";
            throw;
        }
    }

    public void AddButton(BtnHandler btn)
    {
        if (registedBtnList.ContainsKey(btn.ID))
        {
            registedBtnList.Remove(btn.ID);
        }
        registedBtnList.Add(btn.ID, btn);
        if (onBtnAdd != null)
            onBtnAdd(btn.ID);
        _log += "\nAddButton( btn.id = " + btn.ID + ")\n" + LogHelper.CallStack() + "\n\n";
    }

    public void RemoveButton(int id)
    {

        try
        {
            if (registedBtnList.ContainsKey(id))
            {
                registedBtnList.Remove(id);
            }
            _log += "\nRemoveButton( id = " + ID + " )\n" + LogHelper.CallStack() + " \n\n";
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            _log += "RemoveButton Error\n" + e + "\n\n";
            throw;
        }
    }

    public BtnHandler GetButton(int id)
    {
        try
        {
            BtnHandler target = registedBtnList[id];
            _log += "GetButton( id = " + ID + " )\n" + LogHelper.CallStack() + " \n\n";
            return target;

        }
        catch (Exception e)
        {
            Debug.Log("按鈕遺失或未註冊 id = " + id);
            _log += "GetButton( id = " + ID + " ) Error\n" + e + " \n\n";
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
                if (onShow != null)
                {
                    onShow();
                    _log += "\nShow() \n" + LogHelper.CallStack() + "\n\n";
                }
            }
        }
    }

    public void HardShow()
    {
        _isShow = true;
        if (onHardShow != null)
        {
            onHardShow();
            _log += "\nHArdShow()\n" + LogHelper.CallStack() + " \n\n";
        }
    }

    public void Hide()
    {
        if (!isFreeze)
        {
            if (_isShow)
            {
                _isShow = false;
                if (onHide != null)
                {
                    onHide();
                    _log += "\nHide() \n" + LogHelper.CallStack() + "\n\n";
                }
            }
        }
    }

    public void HardHide()
    {
        _isShow = false;
        if (onHardHide != null)
        {
            onHardHide();
            _log += "\nHardHide()\n" + LogHelper.CallStack() + "\n\n";
        }
    }

    public void SetCommand(BasePlatHandlerCmd cmd)
    {
        cmd.Excute(this);
    }

    public void TriggerBtn(int id)
    {
        onBtnTrigger(id);
        _log += "\nTriggerBtn( id = " + id + ")\n" + LogHelper.CallStack() + "\n\n";
    }

    [ContextMenu("Trace Log")]
    public void TraceLog()
    {
        string path = Application.persistentDataPath + "/" + transform.GetPath().Replace("/", "-") + ".TXT";
        using (StreamWriter sw = new StreamWriter(path))   //小寫TXT     
        {
            // Add some text to the file.
            sw.Write(_log);
            sw.Close();
            Debug.Log("Log save to :" + path);
        }

    }

    [ContextMenu("Clera Log")]
    public void ClearLost()
    {
        _log = "";
    }
}
