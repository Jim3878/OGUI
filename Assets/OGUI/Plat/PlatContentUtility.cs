using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public static class PlatContentUtility {

	public static bool HasBtn(this PlatContent plat,int id)
    {
        return plat.GetButton(id) != null;
    }

    public static bool SetBtnState(this PlatContent plat,int id,BtnStateEnum state)
    {
        if(HasBtn(plat , id))
        {
            plat.GetButton(id).SetState(state);
            return true;
        }else
        {
            return false;
        }
    }
}
