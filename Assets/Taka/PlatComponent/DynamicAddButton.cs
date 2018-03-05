using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicAddBtn : IPlatComponent
{
    PlatHandler plat;
    public int currentID = 0;
    Transform parent;

    public DynamicAddBtn(Transform parent)
    {
        this.parent = parent;
    }

    public void AddBtn(BtnHandler handler)
    {
        handler.transform.parent = parent;
        handler.ID = currentID;
        currentID++;
        plat.AddButton(handler);
    }

    public void Initialize(PlatHandler handler)
    {
        this.plat = handler;
    }
}
