using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;

[CustomEditor(typeof(PlatManager))]
public class PlatManagerEditor : Editor {
    bool isFoldContent;
    PlatManager compt;
    private void OnEnable()
    {
        isFoldContent = true;
        compt = (PlatManager)target;
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        isFoldContent = EditorGUILayout.Foldout(isFoldContent, "PlatContent List");
        if (isFoldContent)
        {
            //顯示PlatContent清單
            foreach (PlatContent content in compt.platContentList)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(content.id.ToString(), GUILayout.Width(20));

                content.isFreeze = GUILayout.Toggle(content.isFreeze, "Freeze", GUILayout.Width(100));

                bool isShow = GUILayout.Toggle(content.isShow, "Show",GUILayout.Width(100));

                if (isShow != content.isShow)
                {
                    if (isShow)
                    {
                        content.Show();
                    }
                    else
                    {
                        content.Hide();
                    }
                }

                GUILayout.EndHorizontal();
            }
        }
        

    }

}
