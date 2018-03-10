using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPanel : PlatHandler
{
    DynamicAddBtn addBtn;
    [SerializeField]
    ScrollRect scroll;
    [SerializeField]
    Transform scrollContent;
    [SerializeField]
    GameObject btnPrefabs;
    int currentID = 1;

    public override void Initialize()
    {
        var zoomPlatShow = new ZoomPlatShow(0.5f, DG.Tweening.Ease.InSine, DG.Tweening.Ease.OutSine);
        _platComponentList.Add(zoomPlatShow);
        addBtn = new DynamicAddBtn(scrollContent);
        _platComponentList.Add(addBtn);
        base.Initialize();

        for(int i = 0; i < 10; i++)
        {
            BtnHandler btn = GameObject.Instantiate(btnPrefabs, scrollContent).GetComponent<BtnHandler>();
            this.SetCommand(new PlatHandlerCmdImpl.AddButton(btn, currentID));
            currentID++;
        }
        onBtnTrigger += OnBtnTrigger;
        onShow += RestScroll;
        
    }

    void RestScroll()
    {
        Debug.Log("set");
        scroll.content.localPosition = Vector2.zero;
    }

    void OnBtnTrigger(int id)
    {
        if (id == -1)
        {
            BtnHandler btn = GameObject.Instantiate(btnPrefabs, scrollContent).GetComponent<BtnHandler>();
            this.SetCommand(new PlatHandlerCmdImpl.AddButton(btn, currentID));
            currentID++;
        }
    }
}
