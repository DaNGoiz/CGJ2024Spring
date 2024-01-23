using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoomManager))]
public class RoomManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        RoomManager roomManager = (RoomManager)target;
        if (GUILayout.Button("Load Next Room Up"))
        {
            roomManager.LoadNextRoom(Room.Direction.Up);
        }

        if (GUILayout.Button("Load Next Room Down"))
        {
            roomManager.LoadNextRoom(Room.Direction.Down);
        }

        if (GUILayout.Button("Load Next Room Left"))
        {
            roomManager.LoadNextRoom(Room.Direction.Left);
        }

        if (GUILayout.Button("Load Next Room Right"))
        {
            roomManager.LoadNextRoom(Room.Direction.Right);
        }
        
        if (GUILayout.Button("Previous Room"))
        {
            roomManager.LoadPreviousRoom();
        }
    }
}
