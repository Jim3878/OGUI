using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;
using System;
using System.Reflection;

[CustomEditor(typeof(PlatManager))]
public class PlatManagerEditor : Editor
{
    bool isFoldContent;
    List<bool> isPlatShow;
    PlatManager compt;

    Vector3 btnLabelOffset = new Vector2(-1, 0);
    Color btnLabelTextColor = Color.green;
    int btnLabelTextSize = 15;

    void Test()
    {
        MemberInfo info = typeof(BaseButtonEditor);
        var ce = (CustomEditor[])Attribute.GetCustomAttributes(info, typeof(CustomEditor));
        Debug.Log(ce.Length);
        foreach (var n in ce)
        {
            Debug.Log(n);
            Debug.Log(n.TypeId);
            Debug.Log("///////////");
        }
    }

    private void OnEnable()
    {
        Test();
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

        DrawGizmosSetting();
        EditorGUILayout.Space();

        isFoldContent = EditorGUILayout.Foldout(isFoldContent, "PlatContent List");
        if (isFoldContent)
        {
            //顯示PlatContent清單
            DrawPlatContentList();
        }
    }

    void DrawGizmosSetting()
    {

        btnLabelOffset = EditorGUILayout.Vector3Field("UI Offset", btnLabelOffset);
        btnLabelTextColor = EditorGUILayout.ColorField("UI Coloer", btnLabelTextColor);
        btnLabelTextSize = EditorGUILayout.IntField("UI text size", btnLabelTextSize);

    }

    void DrawContentTitle(PlatContent content)
    {

        GUILayout.BeginHorizontal();
        content.isFreeze = !GUILayout.Toggle(!content.isFreeze, "", GUILayout.Width(20));
        GUILayout.Label(content.basePlat.gameObject.name, GUILayout.Width(110));

        EditorGUI.BeginChangeCheck();
        bool isShow = GUILayout.Toggle(content.isShow, "show", GUILayout.Width(80));
        if (EditorGUI.EndChangeCheck())
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

    void DrawContent(int index)
    {
        GUILayout.BeginHorizontal();

        var content = compt.platContentList[index];
        if (GUILayout.Button(isPlatShow[index] ? "-" : "+", GUILayout.Width(25)))
        {
            isPlatShow[index] = !isPlatShow[index];
        }
        DrawContentTitle(content);
        GUILayout.EndHorizontal();

        if (isPlatShow[index])
        {
            DrawContentButtonList(content);
        }
    }

    void DrawContentButtonList(PlatContent content)
    {
        if (content.btnList != null && content.btnList.Count > 0)
        {
            for (int i = 0; i < content.btnList.Count; i++)
            {
                DrawPlatButton(content, i);
            }
            DrawBtnControll(content);
            GUILayout.Space(5);
        }
    }

    void DrawBtnControll(PlatContent content)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Space(120);
        if (GUILayout.Button("Freeze /Unfreeze All", GUILayout.Width(150)) && content.btnList.Count > 0)
        {
            for (int i = 0; i < content.btnList.Count; i++)
            {
                content.btnList[i].isFreeze = !content.btnList[i].isFreeze;
            }
        }
        GUILayout.EndHorizontal();
    }

    void DrawPlatButton(PlatContent content, int index)
    {
        var btnContent = content.btnList[index];
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(25);
        GUILayout.Label(btnContent.ID + ":", GUILayout.Width(50));
        btnContent.isFreeze = GUILayout.Toggle(btnContent.isFreeze, "isFreeze", GUILayout.Width(80));

        EditorGUI.BeginChangeCheck();
        BtnStateEnum state = (BtnStateEnum)EditorGUILayout.EnumPopup(btnContent.GetState(), GUILayout.Width(100));
        if (EditorGUI.EndChangeCheck())
        {
            btnContent.SetState(state);
        }
        if (GUILayout.Button("Test", GUILayout.Width(50)))
        {
            content.basePlat.InvokeBtn(btnContent.ID);
        }

        EditorGUILayout.EndHorizontal();
    }

    void DrawPlatContentList()
    {
        for (int i = 0; i < compt.platContentList.Count; i++)
        {
            DrawContent(i);
        }

    }


    private void OnSceneGUI()
    {
        DrawBtnID();
    }


    void DrawBtnID()
    {
        for (int i = 0; i < compt.platContentList.Count; i++)
        {
            var platContent = compt.platContentList[i];
            if (!platContent.isFreeze && platContent.btnList != null)
            {
                for (int j = 0; j < platContent.btnList.Count; j++)
                {
                    var btn = platContent.btnList[j];

                    GUIStyle style = new GUIStyle();
                    style.normal.textColor = Color.green;
                    style.fontSize = this.btnLabelTextSize;


                    Handles.BeginGUI();

                    Handles.Label(btn.baseButton.transform.position + btnLabelOffset, btn.ID.ToString(), style);

                    Handles.EndGUI();
                }
            }
        }
    }
}
