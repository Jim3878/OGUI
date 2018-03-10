using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class BtnHandler : MonoBehaviour
{

    #region VARIABLE
    public delegate void StateEvent(BtnStateEnum state);
    public delegate void BtnEvent();
    [System.Serializable]
    public class UE_StateEvent : UnityEvent<BtnStateEnum> { };
    #endregion
    public int ID;
    public bool isFreeze = false;
    protected List<IBtnComponent> btnComponentList = new List<IBtnComponent>();

    //public BtnHandler up, left, right, down;
    public StateEvent onChangeState;
    public BtnEvent onInitialize;
    public BtnEvent onFreeze;
    public BtnEvent onUnfreeze;

    protected PlatHandler _plat;

    public PlatHandler plat
    {
        get
        {
            return _plat;
        }
    }

    string _log = "";
    protected BtnStateEnum _currentState;

    public virtual void Initialize(PlatHandler handler)
    {
        this._plat = handler;
        plat.AddButton(this);
        InitilzeComponent();
        _log += string.Format("Initialize \n{0}\n\n", LogHelper.CallStack());
        if (onInitialize != null)
            onInitialize();
    }

    void InitilzeComponent()
    {
        foreach (IBtnComponent compt in btnComponentList)
        {
            compt.Initialize(this);
        }
    }

    public bool SetState(BtnStateEnum state)
    {
        if (_currentState != state && !isFreeze)
        {
            _currentState = state;
            if (onChangeState != null)
            {
                onChangeState(_currentState);
            }
        }
        _log += string.Format("SetState to {1} \n{0}\n\n", LogHelper.CallStack(), state);
        return !isFreeze;
    }

    public BtnStateEnum GetState()
    {
        return _currentState;
    }

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

    public void ClearLog()
    {
        _log = "";
    }

    //public BtnHandler GetNeighborBtn(BtnDirectionEnum dir)
    //{
    //    switch (dir)
    //    {
    //        case BtnDirectionEnum.UP:
    //            return up;
    //        case BtnDirectionEnum.LEFT:
    //            return left;
    //        case BtnDirectionEnum.RIGHT:
    //            return right;
    //        case BtnDirectionEnum.DOWN:
    //            return down;
    //        default:
    //            return null;

    //    }
    //}
}
