using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Room))]
public class RoomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Room room = (Room)target;

        DrawDoorField("Up Door", ref room.up, ref room.currentUpDoor, ref room.nextRoomDownDoor);
        DrawDoorField("Down Door", ref room.down, ref room.currentDownDoor, ref room.nextRoomUpDoor);
        DrawDoorField("Left Door", ref room.left, ref room.currentLeftDoor, ref room.nextRoomRightDoor);
        DrawDoorField("Right Door", ref room.right, ref room.currentRightDoor, ref room.nextRoomLeftDoor);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(room);
        }
    }

    private void DrawDoorField(string label, ref bool doorBool, ref GameObject currentDoor, ref GameObject nextDoor)
    {
        EditorGUILayout.BeginHorizontal();
        doorBool = EditorGUILayout.Toggle(label, doorBool);
        EditorGUILayout.EndHorizontal();

        if (doorBool)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Current " + label);
            currentDoor = (GameObject)EditorGUILayout.ObjectField(currentDoor, typeof(GameObject), true);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Next Room Opposite " + label);
            nextDoor = (GameObject)EditorGUILayout.ObjectField(nextDoor, typeof(GameObject), true);
            EditorGUILayout.EndHorizontal();
        }
    }
}
