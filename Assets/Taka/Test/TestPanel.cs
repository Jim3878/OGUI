using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPanel : PlatHandler
{
    DynamicAddBtn addBtn;
    [SerializeField]
    Transform scrollContent;
    [SerializeField]
    GameObject btnPrefabs;
    int currentID = 1;

    public override void Initialize()
    {
        _platComponentList.Add(new ZoomPlatShow(0.5f, DG.Tweening.Ease.InSine, DG.Tweening.Ease.OutSine));
        addBtn = new DynamicAddBtn(scrollContent);
        _platComponentList.Add(addBtn);
        base.Initialize();

        for(int i = 0; i < 10; i++)
        {
            BtnHandler btn = GameObject.Instantiate(btnPrefabs, scrollContent).GetComponent<BtnHandler>();
            this.SetCommand(new PlatHandlerCmdImpl.AddButton(btn, currentID));
            currentID++;
        }
        onBtnTrigger += (id) =>
        {
            if (id == -1)
            {
                BtnHandler btn = GameObject.Instantiate(btnPrefabs, scrollContent).GetComponent<BtnHandler>();
                this.SetCommand(new PlatHandlerCmdImpl.AddButton(btn, currentID));
                currentID++;
            }
        };
    }
}
