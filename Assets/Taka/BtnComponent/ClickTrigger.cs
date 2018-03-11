using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickTrigger : EventTrigger, IBtnComponent
{
    PlatHandler plat;
    BtnHandler btn;

    public void Initialize(BtnHandler handler)
    {
        btn = handler;
        plat = handler.plat;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        if (!btn.isFreeze && !plat.isFreeze)
            btn.SetState(BtnStateEnum.PRESSED);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        if (!btn.isFreeze && !plat.isFreeze)
            btn.SetState(BtnStateEnum.HIGHLIGHT);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        if (!btn.isFreeze && !plat.isFreeze)
            btn.SetState(BtnStateEnum.NORMAL);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        //if (!btn.isFreeze && !plat.isFreeze)
        {
            btn.SetState(BtnStateEnum.NORMAL);
            plat.TriggerBtn(btn.ID);
        }
    }
}
