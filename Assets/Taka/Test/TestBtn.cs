using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBtn : BtnHandler {

    public List<Component> unCastComponentList;

    public override void Initialize(PlatHandler handler)
    {
        RegistBtnComponentList();
        base.Initialize(handler);
    }

    void RegistBtnComponentList()
    {
        foreach(Component cmpt in unCastComponentList)
        {
            if(cmpt is IBtnComponent)
            {
                base.btnComponentList.Add(cmpt as IBtnComponent);
            }
        }
    }
}
