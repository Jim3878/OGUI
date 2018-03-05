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

    public override void Initialize()
    {
        _platComponentList.Add(new ZoomPlatShow(0.5f, DG.Tweening.Ease.InSine, DG.Tweening.Ease.OutSine));
        addBtn = new DynamicAddBtn(scrollContent);
        _platComponentList.Add(addBtn);
        base.Initialize();

        for(int i = 0; i < 10; i++)
        {
            addBtn.AddBtn(GameObject.Instantiate(btnPrefabs, scrollContent).GetComponent<BtnHandler>());
        }
        onBtnTrigger += (id) =>
        {
            if (id == -1)
            {
                BtnHandler btn = GameObject.Instantiate(btnPrefabs, scrollContent).GetComponent<BtnHandler>();
                //Debug.Log("01 "+ btn.transform.localScale);
                addBtn.AddBtn(btn);
                //Debug.Log("02" + btnPrefabs.transform.localScale);
                //Debug.Log("03" + btn.transform.localScale);
            }
        };
    }
}
