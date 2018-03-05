using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlatHandler))]
public class PlatHandlerInspector : Editor
{
    
    PlatHandler compt;

    private void OnEnable()
    {
        compt = target as PlatHandler;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //Rect up = GUILayoutUtility.GetRect(0, 1, GUILayout.ExpandWidth(true));

        //Rect down = GUILayoutUtility.GetRect(0, 1, GUILayout.ExpandWidth(true));
        //DropAreaGUI(up, down);
    }

    void DropAreaGUI(Rect up, Rect down)
    {
        Event evt = Event.current;
        //Rect drop_area = GUILayoutUtility.GetRect(0, up.position.y - up.position.y, GUILayout.ExpandWidth(true));
        Rect drop_area = new Rect(up.position, new Vector2(down.width, down.position.y - up.position.y));
        Debug.Log(drop_area.position);
        //GUI.Box(drop_area, "Add Trigger");

        switch (evt.type)
        {
            case EventType.DragUpdated:
            case EventType.DragPerform:
                if (!drop_area.Contains(evt.mousePosition))
                    return;

                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                if (evt.type == EventType.DragPerform)
                {
                    DragAndDrop.AcceptDrag();

                    foreach (Object dragged_object in DragAndDrop.objectReferences)
                    {
                        Debug.Log(dragged_object);
                        // Do On Drag Stuff here
                    }
                }
                break;
        }
    }
}
