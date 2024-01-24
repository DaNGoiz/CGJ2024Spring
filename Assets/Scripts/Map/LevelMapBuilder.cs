using UnityEngine;
using System.Collections.Generic;

public class LevelMapBuilder : MonoBehaviour
{
    public GameObject initialRoom;
    public GameObject lastRoom;
    public List<GameObject> roomPrefabs;

    [Header("Transform")]
    public float transformPosition = 2f;

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

        
        if (nextRoomDoor == null)
        {
            nextRoomDoor = SelectNextRoom(door.GetComponent<Door>().doorDirection, door);
        }
        else
        {
            print(nextRoomDoor.name);
        }

        MovePlayerToRoom(nextRoomDoor);
    }

    GameObject SelectNextRoom(Door.Direction currentDoorDirection, GameObject door)
    {
        GameObject nextRoom = roomPrefabs[Random.Range(0, roomPrefabs.Count)];

        Room currentRoomScript = currentRoom.GetComponent<Room>();
        Room nextRoomScript = nextRoom.GetComponent<Room>();
        
        if (nextRoomScript.HasDoor(currentDoorDirection))
        {
            nextRoom = Instantiate(nextRoom);
            nextRoom.SetActive(true);

            nextRoomScript = nextRoom.GetComponent<Room>();
            
            Door.Direction nextDoorDirection = Door.GetOppositeDirection(currentDoorDirection);
            GameObject nextRoomDoor = nextRoomScript.GetCorrespondingDoor(nextDoorDirection);
            currentRoomScript.BindDoor(currentDoorDirection, nextRoomDoor);
            nextRoomScript.BindDoor(nextDoorDirection, door);

            currentRoom = nextRoom;
            return nextRoomDoor;
        }
        else
        {
            return SelectNextRoom(currentDoorDirection, door);
        }
    }

    void MovePlayerToRoom(GameObject door)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (door.GetComponent<Door>().doorDirection == Door.Direction.Up)
            {
                player.transform.position = door.transform.position + new Vector3(0, -transformPosition, 0);
            }
            else if (door.GetComponent<Door>().doorDirection == Door.Direction.Down)
            {
                player.transform.position = door.transform.position + new Vector3(0, transformPosition, 0);
            }
            else if (door.GetComponent<Door>().doorDirection == Door.Direction.Left)
            {
                player.transform.position = door.transform.position + new Vector3(transformPosition, 0, 0);
            }
            else if (door.GetComponent<Door>().doorDirection == Door.Direction.Right)
            {
                player.transform.position = door.transform.position + new Vector3(-transformPosition, 0, 0);
            }
            else
            {
                player.transform.position = door.transform.position;
            }
        }
    }
}