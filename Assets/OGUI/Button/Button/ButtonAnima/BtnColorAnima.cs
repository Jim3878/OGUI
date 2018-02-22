using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnColorAnima : MonoBehaviour,IUIComponent {

    public Color Pressed=Color.white, Normal = Color.white, Highlight = Color.white, Disable = Color.white;
    public Image btnImage;
    BtnContent content;
    // Use this for initialization

    public void Initialize()
    {
        try
        {
            content = GetComponent<BaseButton>().GetBtnContent();
            if (content == null)
            {
                throw new Exception("找不到BaseButton元件 @" + transform.GetPath());
            }
            content.onChangeStae += OnBtnStateChange;
            if (btnImage == null)
            {
                btnImage = GetComponent<Image>();
            }
            if (btnImage == null)
            {
                throw new Exception("找不到可更改之Image元件 @"+transform.GetPath());
            }

            OnBtnStateChange(this,new EventArgs());
           
        }catch(Exception)
        {
            throw;
        }
    }

    void OnBtnStateChange(object sender,EventArgs e)
    {
        switch (content.GetState())
        {
            case BtnStateEnum.NORMAL:
                btnImage.color = Normal;
                break;
            case BtnStateEnum.PRESSED:
                btnImage.color = Pressed;
                break;
            case BtnStateEnum.HIGHLIGHT:
                btnImage.color = Highlight;
                break;
            case BtnStateEnum.DISABLE:
                btnImage.color = Disable;
                break;
            default:
                break;
        }
    }


}
