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
        GameObject nextRoomDoor = currentRoomScript.GetCorrespondingDoorInNextRoom(door.GetComponent<Door>().doorDirection);

        // 如果没有生成过下一个房间，就生成下一个房间
        if (nextRoomDoor == null)
        {
            nextRoomDoor = SelectNextRoom(door.GetComponent<Door>().doorDirection);
        }

        MovePlayerToRoom(nextRoomDoor);
    }

    GameObject SelectNextRoom(Door.Direction currentDoorDirection)
    {
        GameObject nextRoom = roomPrefabs[Random.Range(0, roomPrefabs.Count)];

        Room currentRoomScript = currentRoom.GetComponent<Room>();
        Room nextRoomScript = nextRoom.GetComponent<Room>();
        
        if (nextRoomScript.HasDoor(currentDoorDirection))
        {
            nextRoom = Instantiate(nextRoom);
            nextRoom.SetActive(true);
            
            Door.Direction nextDoorDirection = Door.GetOppositeDirection(currentDoorDirection);
            GameObject nextRoomDoor = nextRoomScript.GetCorrespondingDoorInNextRoom(nextDoorDirection);
            
            currentRoomScript.BindDoor(currentDoorDirection, nextRoomDoor);

            currentRoom = nextRoom;
            return nextRoom;
        }
        else
        {
            // 如果没有门，就重新选择下一个房间
            return SelectNextRoom(currentDoorDirection);
        }
    }

    void MovePlayerToRoom(GameObject door)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            player.transform.position = door.transform.position;
        }

    }
}