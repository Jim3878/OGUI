using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public interface IPlatAnima
{
    /// <summary>
    /// 含動畫之正常開啟
    /// </summary>
    void Show();
    void Show(Action onComplete);
    /// <summary>
    /// 含動畫之正常關閉
    /// </summary>
    void Hide();
    void Hide(Action onComplete);
    /// <summary>
    /// 不含動畫立即開啟
    /// </summary>
    void HardShow();
    /// <summary>
    /// 不含動畫立即關閉
    /// </summary>
    void HardHide();

}
