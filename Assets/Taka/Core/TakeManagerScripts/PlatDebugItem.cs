using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatDebugItem : MonoBehaviour
{
    public Button showHideBtn;
    public Text showHideBtnText;
    public Text platName;
    public Toggle enableToggle;
   

    bool isShow;
    public int handlerID
    {
        get
        {
            return handler.ID;
        }
    }
    PlatHandler handler;

    public void Initialize(PlatHandler handler)
    {
        this.handler = handler;
        platName.text = handler.gameObject.name;

        LinkShowHideBtnText();
        SetShowHideBtnEvent();

        LinkToggle();
        SetEnableToggleEvent();
    }

    void SetShowHideBtnEvent()
    {
        showHideBtn.onClick.AddListener(OnShowHideBtnClick);
    }

    void OnShowHideBtnClick()
    {
        if (handler.isShow)
        {
            handler.Hide();
        }
        else
        {
            handler.Show();
        }
    }

    void SetEnableToggleEvent()
    {
        enableToggle.onValueChanged.AddListener(OnFreezeChange);
    }

    void OnFreezeChange(bool isOn)
    {
        handler.isFreeze = !isOn;
    }

    void LinkShowHideBtnText()
    {
        if (handler.isShow)
        {
            showHideBtnText.text = "hide it";
        }
        else
        {
            showHideBtnText.text = "show it";
        }
        handler.onShow += OnPlatHandlerShow;
        handler.onHide += OnPlatHandlerHide;
    }

    void OnPlatHandlerHide()
    {
        showHideBtnText.text = "show it";
    }

    void OnPlatHandlerShow()
    {
        showHideBtnText.text = "hide it";
    }

    void LinkToggle()
    {
        
        enableToggle.isOn = !handler.isFreeze;
        handler.onFreeze += OnPlatHandlerFreezeTrig;
        handler.onUnfreeze += OnPlatHandlerFreezeTrig;
    }

    void OnPlatHandlerFreezeTrig()
    {
        enableToggle.isOn = !handler.isFreeze;
    }

}
