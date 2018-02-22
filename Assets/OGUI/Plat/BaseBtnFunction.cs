using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBtnFunction : MonoBehaviour, IUIComponent
{

    protected BasePlat plat;
    public void Initialize()
    {
        plat = GetComponent<BasePlat>();
        plat.invokeBtn += InvokeBtn;
    }

    void InvokeBtn(object seder, IntPassEventArgs e)
    {
        if (!plat.GetPlatContent().GetButton(e.id).isFreeze)
            BtnFunction(e.id);
    }

    protected abstract void BtnFunction(int id);
}
