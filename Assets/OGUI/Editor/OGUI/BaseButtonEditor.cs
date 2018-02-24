using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BaseButton))]
public class BaseButtonEditor : Editor
{

    BaseButton compt;
    public static bool drawGizmos;

    private void OnEnable()
    {
        //drawGizmos = true;
        compt = target as BaseButton;
    }

    public override void OnInspectorGUI()
    {
        drawGizmos = EditorGUILayout.Toggle("Draw Gizmos", drawGizmos);

        base.OnInspectorGUI();
    }

    [DrawGizmo(GizmoType.Selected | GizmoType.Active | GizmoType.InSelectionHierarchy)]
    public static void BaseButtonDrawGizmos(BaseButton btn, GizmoType gizmosType)
    {
        if (drawGizmos)
        {
            //Gizmos.DrawCube(btn.transform.position, Vector3.one*500);
            Handles.BeginGUI();
            GUIStyle s = new GUIStyle();
            s.fontSize = 15;
            s.normal.textColor = Color.green;
            s.normal.background = Utility.MakeTexture(new Color(0, 0, 0, 0.5f), 50, 50);
            Vector3 offset = Vector3.zero;
            if (btn.GetComponent<RectTransform>() != null)
            {
                offset = new Vector3(btn.GetComponent<RectTransform>().rect.width / -2 - 15, 0);
            }
            Handles.Label(btn.transform.position + offset, string.Format(" [{0}] ", btn.id.ToString()), s);
            Handles.EndGUI();
        }

    }
}
