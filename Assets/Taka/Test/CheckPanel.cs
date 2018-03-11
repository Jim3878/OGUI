using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPanel : PlatHandler
{

    Action<bool> action;

    public override void Initialize(TakeManager manager)
    {
        onTriggerBtn += OnTriggerBtn;
        var zoomPlatShow = new ZoomPlatShow(0.5f, DG.Tweening.Ease.InSine, DG.Tweening.Ease.OutSine);
        _platComponentList.Add(zoomPlatShow);
        base.Initialize(manager);
    }

    public void Show(Action<bool> action)
    {
        Show();
        this.action = action;
    }

    void OnTriggerBtn(int id)
    {
        if (id == 0)
        {
            action(false);
        }
        else
        {
            action(true);
        }
    }
}
