using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoomManager))]
public class RoomManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        RoomManager roomManager = (RoomManager)target;
        if (GUILayout.Button("Next Room"))
        {
            roomManager.LoadNextRoom();
        }
        if (GUILayout.Button("Previous Room"))
        {
            roomManager.LoadPreviousRoom();
        }
    }
}
