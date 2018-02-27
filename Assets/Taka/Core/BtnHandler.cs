using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BtnHandler : MonoBehaviour {
    public delegate void StateEvent(BtnStateEnum state);
    public delegate void BtnEvent();
    public class UE_StateEvent : UnityEvent<BtnStateEnum> { };

    public int ID;
    public bool isFreeze = false;
    
    public BtnHandler up, left, right, down;

    public StateEvent onChangeState;
    public BtnEvent onInitialize;
    public BtnEvent onFreeze;
    public BtnEvent onUnfreeze;

    [SerializeField]
    private UnityEvent _onInitialize;
    [SerializeField]
    private UnityEvent _onFreeze;
    [SerializeField]
    private UnityEvent _onUnfreeze;
    [SerializeField]
    private UE_StateEvent _onChangeState;



    protected BtnStateEnum _currentState;

    public void Initialize()
    {
        onChangeState += _onChangeState.Invoke;
        onInitialize += _onInitialize.Invoke;
        onFreeze += onFreeze;
        onUnfreeze += onUnfreeze;
        InitilzeComponent();
        onInitialize();
    }

    void InitilzeComponent()
    {
        foreach(IUIComponent compt in GetComponents<IUIComponent>())
        {
            compt.Initialize();
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
        return !isFreeze;
    }

    public BtnStateEnum GetState()
    {
        return _currentState;
    }

    public BtnHandler GetNeighborBtn(BtnDirectionEnum dir)
    {
        switch (dir)
        {
            case BtnDirectionEnum.UP:
                return up;
            case BtnDirectionEnum.LEFT:
                return left;
            case BtnDirectionEnum.RIGHT:
                return right;
            case BtnDirectionEnum.DOWN:
                return down;
            default:
                return null;

        }
    }
}
