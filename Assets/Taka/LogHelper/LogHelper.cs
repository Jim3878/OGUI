using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class LogHelper : MonoBehaviour {

    public static string WhoCallMe()
    {
        // 2:省略目前位置與呼叫目前Function的位置
        // true:顯示檔案資訊
        var stackTrace = new StackTrace(2, true);
        var target = stackTrace.GetFrame(0);
        return new StackTrace(target).ToString();
    }

    public static string CallStack()
    {
        // 1:省略目前位置
        // true:顯示檔案資訊
        var stackTrace = new StackTrace(1, true);
        return stackTrace.ToString();
    }
}
