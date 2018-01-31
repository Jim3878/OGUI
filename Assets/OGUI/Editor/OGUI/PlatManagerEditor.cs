using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;

[CustomEditor(typeof(PlatManager))]
public class PlatManagerEditor : Editor {
    bool isFoldContent;
    List<bool> isPlatShow;
    PlatManager compt;
    private void OnEnable()
    {
        isFoldContent = true;
        compt = (PlatManager)target;
        isPlatShow = new List<bool>();
    }

    public override void OnInspectorGUI()
    {
        while (isPlatShow.Count < compt.platContentList.Count)
        {
            isPlatShow.Add(false);
        }
        //base.OnInspectorGUI();
        isFoldContent = EditorGUILayout.Foldout(isFoldContent, "PlatContent List");
        if (isFoldContent)
        {
            //顯示PlatContent清單
            var content = compt.platContentList;
            for (int i=0;i<compt.platContentList.Count;i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(content[i].id+":", GUILayout.Width(20));

                content[i].isFreeze = GUILayout.Toggle(content[i].isFreeze, "Freeze", GUILayout.Width(100));

                bool isShow = GUILayout.Toggle(content[i].isShow, "Show",GUILayout.Width(100));

                if (isShow != content[i].isShow)
                {
                    if (isShow)
                    {
                        content[i].Show();
                    }
                    else
                    {
                        content[i].Hide();
                    }
                }
                GUILayout.EndHorizontal();
                isPlatShow[i] = EditorGUILayout.Foldout(isPlatShow[i], "Button List");
                if (isPlatShow[i])
                {
                    BtnContent btn;
                    if (content[i].btnList != null)
                    {
                        for (int j = 0; j < content[i].btnList.Count; j++)
                        {
                            btn = content[i].btnList[j];
                            BtnStateEnum state = (BtnStateEnum)EditorGUILayout.EnumPopup(btn.GetState());
                            if (state != btn.GetState())
                            {
                                btn.SetState(state);
                            }

                        }
                    }
                }
                
            }
        }
        

    }

}
