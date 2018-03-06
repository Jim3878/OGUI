using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatHandlerCmdImpl
{

    public class AddButton : BasePlatHandlerCmd
    {
        BtnHandler btn;
        int btnID;

        protected override void SelfExcute(PlatHandler plat)
        {
            btn.ID = btnID;
            plat.AddButton(btn);
        }

        public AddButton(BtnHandler btn,int btnID)
        {
            this.btn = btn;
            this.btnID = btnID;
        }
        
    }
}
