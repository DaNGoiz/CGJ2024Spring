using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoomManager))]
public class RoomManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // 绘制默认的Inspector

        RoomManager roomManager = (RoomManager)target;
        if (GUILayout.Button("Next Room"))
        {
            roomManager.LoadNextRoom();
        }
    }
}
