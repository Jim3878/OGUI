using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageStateHolder : MonoBehaviour, IBtnComponent
{
    public Image image;
    public Color normal = Color.white;
    public Color pressed = Color.white;
    public Color highlight = Color.white;
    public Color disable = Color.white;

    public void Initialize(BtnHandler handler)
    {
        handler.onChangeState += ChangeState;
    }

    void ChangeState(BtnStateEnum state)
    {
        switch (state)
        {
            case BtnStateEnum.NORMAL:
                image.color = normal;
                break;
            case BtnStateEnum.PRESSED:
                image.color = pressed;
                break;
            case BtnStateEnum.HIGHLIGHT:
                image.color = highlight;
                break;
            case BtnStateEnum.DISABLE:
                image.color = disable;
                break;
            default:
                break;
        }
    }
}
