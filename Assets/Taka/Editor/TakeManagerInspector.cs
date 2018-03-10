using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(TakeManager))]
public class TakeManagerInspector : Editor
{
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
        base.OnInspectorGUI();

        //if (EditorApplication.isPlaying)
        //{
        //    while (isPlatShow.Count < compt.platList.Count)
        //    {
        //        isPlatShow.Add(false);
        //    }

        //    DrawGizmosSetting();
        //    EditorGUILayout.Space();

        //    isFoldContent = EditorGUILayout.Foldout(isFoldContent, "PlatContent List");
        //    if (isFoldContent)
        //    {
        //        //顯示PlatContent清單
        //        DrawPlatContentList();
        //    }
        //}
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

    void InitializeEvent()
    {
        foreach (var plat in compt.platList)
        {
            plat.onBtnAdd -= OnBtnAdd;
            plat.onBtnAdd += OnBtnAdd;
        }
    }

    void OnBtnAdd(int id) { Repaint(); }


    void DrawContent(int id)
    {
        GUILayout.BeginHorizontal();

        var content = compt.platList[id];
        if (GUILayout.Button(isPlatShow[id] ? "-" : "+", GUILayout.Width(25)))
        {
            isPlatShow[id] = !isPlatShow[id];
        }
        DrawContentTitle(content);
        GUILayout.EndHorizontal();

        if (isPlatShow[id])
        {
            DrawContentButtonList(content);
        }
    }

    void DrawContentButtonList(PlatHandler handler)
    {
        try
        {
            if (handler.registedBtnList != null && handler.registedBtnList.Count > 0)
            {
                foreach (var key in handler.registedBtnList)
                {
                    DrawPlatButton(handler, key.Key);
                }
                DrawBtnControll(handler);
                GUILayout.Space(5);
            }
        }
        catch (InvalidOperationException)
        {

        }
        catch (Exception)
        {
            throw;
        }
    }

    void DrawBtnControll(PlatHandler handler)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Space(120);
        if (GUILayout.Button("Freeze /Unfreeze All", GUILayout.Width(150)) && handler.registedBtnList.Count > 0)
        {
            for (int i = 0; i < handler.registedBtnList.Count; i++)
            {
                handler.registedBtnList[i].isFreeze = !handler.registedBtnList[i].isFreeze;
            }
        }
        GUILayout.EndHorizontal();
    }

    void DrawPlatButton(PlatHandler handler, int id)
    {
        var btnContent = handler.registedBtnList[id];
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
            handler.TriggerBtn(btnContent.ID);
        }

        EditorGUILayout.EndHorizontal();
    }

    void DrawPlatContentList()
    {
        foreach (var plat in compt.platList)
        {
            DrawContent(plat.ID);
        }

    }


    private void OnSceneGUI()
    {
        DrawBtnID();
    }


    void DrawBtnID()
    {
        foreach(var plat in compt.platList)
        {
            var platHangler = plat;
            if (!platHangler.isFreeze && platHangler.registedBtnList != null)
            {
                foreach (var btnPair in platHangler.registedBtnList)
                {
                    var btn = btnPair.Value;

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
