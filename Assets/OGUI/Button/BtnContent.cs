using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class BtnContent {

    public PlatContent platContent;
    public BtnContent up, left, right, down;
    public EventHandler onChangeStae;
    public int ID;

    BtnStateEnum _currentState;

    public void SetState(BtnStateEnum state)
    {
        if (_currentState != state)
        {
            _currentState = state;
            onChangeStae(this, new EventArgs());
        }
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
