using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TakeManager))]
public class TakeManagerInspector : Editor{
    bool isFoldContent;
    List<bool> isPlatShow;
    TakeManager compt;

    Vector3 btnLabelOffset = new Vector2(-1, 0);
    Color btnLabelTextColor = Color.green;
    int btnLabelTextSize = 15;
    

    private void OnEnable()
    {
        isFoldContent = true;
        compt = (TakeManager)target;
        isPlatShow = new List<bool>();
    }

    public override void OnInspectorGUI()
    {
        while (isPlatShow.Count < compt.platList.Count)
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

    void DrawContentTitle(PlatHandler content)
    {

        GUILayout.BeginHorizontal();
        content.isFreeze = !GUILayout.Toggle(!content.isFreeze, "", GUILayout.Width(20));
        GUILayout.Label(content.gameObject.name, GUILayout.Width(110));

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

        var content = compt.platList[index];
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

    void DrawContentButtonList(PlatHandler handler)
    {
        if (handler.btnHandlerList != null && handler.btnHandlerList.Count > 0)
        {
            for (int i = 0; i < handler.btnHandlerList.Count; i++)
            {
                DrawPlatButton(handler, i);
            }
            DrawBtnControll(handler);
            GUILayout.Space(5);
        }
    }

    void DrawBtnControll(PlatHandler handler)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Space(120);
        if (GUILayout.Button("Freeze /Unfreeze All", GUILayout.Width(150)) && handler.btnHandlerList.Count > 0)
        {
            for (int i = 0; i < handler.btnHandlerList.Count; i++)
            {
                handler.btnHandlerList[i].isFreeze = !handler.btnHandlerList[i].isFreeze;
            }
        }
        GUILayout.EndHorizontal();
    }

    void DrawPlatButton(PlatHandler handler, int index)
    {
        var btnContent = handler.btnHandlerList[index];
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
            handler.onBtnTrigger(btnContent.ID);
        }

        EditorGUILayout.EndHorizontal();
    }

    void DrawPlatContentList()
    {
        for (int i = 0; i < compt.platList.Count; i++)
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
        for (int i = 0; i < compt.platList.Count; i++)
        {
            var platContent = compt.platList[i];
            if (!platContent.isFreeze && platContent.btnHandlerList != null)
            {
                for (int j = 0; j < platContent.btnHandlerList.Count; j++)
                {
                    var btn = platContent.btnHandlerList[j];

                    GUIStyle style = new GUIStyle();
                    style.normal.textColor = Color.green;
                    style.fontSize = this.btnLabelTextSize;


                    Handles.BeginGUI();

                    Handles.Label(btn.transform.position + btnLabelOffset, btn.ID.ToString(), style);

                    Handles.EndGUI();
                }
            }
        }
    }
}
