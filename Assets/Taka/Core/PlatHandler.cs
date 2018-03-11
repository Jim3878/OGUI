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
    private TakeManager _manager;
    public TakeManager manager { get { return _manager; } }
    public Dictionary<int, BtnHandler> registedBtnList = new Dictionary<int, BtnHandler>();
    public event PlatEvent onInitialize;
    public event PlatEvent onShow;
    public event PlatEvent onHardShow;
    public event PlatEvent onHide;
    public event PlatEvent onHardHide;
    public event PlatEvent onFreeze;
    public event PlatEvent onUnfreeze;
    public event AddBtn onBtnAdd;
    public event BtnTrigger onTriggerBtn;

    string _log = "";

    private bool _isFreeze;
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
                if (_isFreeze)
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


    public virtual void Initialize(TakeManager manager)
    {
        _manager = manager;
        InitilizeBtnHandler();
        InitilzeComponent();

        if (onInitialize != null)
        {
            onInitialize();
        }
        _log += "\nInitialize()\n" + LogHelper.CallStack() + "\n\n";
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
            _log += "\nInitilizeBtnHandler Error\n" + e + "\n\n";
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
            _log += "\nRemoveButton Error\n" + e + "\n\n";
            throw;
        }
    }

    public List<BtnHandler> GetAllBtn()
    {
        _log += "\nGetAllBtn()\n\n";
        return btnList.GetRange(0, btnList.Count);
    }

    public BtnHandler GetButton(int id)
    {
        try
        {
            BtnHandler target = registedBtnList[id];
            _log += "\nGetButton( id = " + ID + " )\n" + LogHelper.CallStack() + " \n\n";
            return target;

        }
        catch (Exception e)
        {
            Debug.Log("有人好像跟我拿了一個不存在的按鈕，你說我該怎麼辦？\n btn ID = " + id);
            _log += "\nGetButton( id = " + ID + " ) Error\n" + e + " \n\n";
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

    /// <summary>
    /// 無視isFreeze強制立即開啟
    /// </summary>
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

    /// <summary>
    /// 無視isFreeze強制立即關閉
    /// </summary>
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
        var btn = btnList.Find((x) => x.ID == id);
        if (!isFreeze && btn != null && !btn.isFreeze)
        {
            onTriggerBtn(id);
            _log += "\nTriggerBtn( id = " + id + ")\n" + LogHelper.CallStack() + "\n\n";
        }
        else
        {
            _log += string.Format("有人想要觸發按鈕{0},然而並沒有任何卵用。\n isfreeze = {1}\nbtn = {2}\n{3}\n\n",
                id, isFreeze, btn == null ? btn.ToString() : btn.transform.GetPath(), LogHelper.CallStack());
        }
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
