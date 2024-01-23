using UnityEngine;
using System.Collections.Generic;

public class LevelMapBuilder : MonoBehaviour
{
    public GameObject initialRoom;
    public GameObject lastRoom;
    public List<GameObject> roomPrefabs;

    private GameObject currentRoom;


    void Start()
    {
        GameObject firstRoom = Instantiate(initialRoom);
        currentRoom = firstRoom;
        currentRoom.SetActive(true);
    }

    public void OnDoorTriggered(GameObject door)
    {
        Room currentRoomScript = currentRoom.GetComponent<Room>();
        GameObject nextRoomDoor = currentRoomScript.GetCorrespondingDoorInNextRoom(currentDoorScript.doorDirection);

        // 如果没有生成过下一个房间，就生成下一个房间
        if (nextRoomDoor == null)
        {
            nextRoomDoor = SelectNextRoom(door.GetComponent<Door>().doorDirection);
        }

        MovePlayerToRoom(nextRoomDoor);
    }

    GameObject SelectNextRoom(Door.Direction currentDoorDirection)
    {
        // 选择下一个房间
        // 绑定下一个房间的门
        // 返回下一个门

        return null; // 示例
    }

    void MovePlayerToRoom(GameObject door)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            // 移动玩家到下一个房间的入口
            player.transform.position = door.transform.position;
        }

    }
}