using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class BtnContent
{

    public PlatContent platContent;
    public BtnContent up, left, right, down;
    public EventHandler onChangeStae;
    public BaseButton baseButton { get { return _baseButton; } }
    public bool isFreeze = false;
    public int ID;


    protected BaseButton _baseButton;
    protected BtnStateEnum _currentState;

    public void SetBaseButton(BaseButton baseButton)
    {
        if (_baseButton == null)
            _baseButton = baseButton;
    }

    public bool SetState(BtnStateEnum state)
    {
        if (_currentState != state && !isFreeze)
        {
            _currentState = state;
            if (onChangeStae != null)
            {
                onChangeStae(this, new EventArgs());
            }
        }
        return !isFreeze;
    }

    public BtnStateEnum GetState()
    {
        return _currentState;
    }

    public BtnContent GetNeighborBtn(BtnDirectionEnum dir)
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
