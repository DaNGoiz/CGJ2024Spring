using UnityEngine;
using System.Collections.Generic;

public class LevelMapBuilder : MonoBehaviour
{
    public GameObject initialRoom;
    public GameObject lastRoom;
    public List<GameObject> roomPrefabs;

    [Header("Transform")]
    public float transformPosition = 2f;
    public float roomTransformDistance = 10f;

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
            // nextRoom = Instantiate(nextRoom);
            nextRoom = InstantiateAtDirection(currentRoom, currentDoorDirection, nextRoom);
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

    public GameObject InstantiateAtDirection(GameObject currentRoom, Door.Direction direction, GameObject objectToInstantiate)
    {
        // 获取当前房间的位置
        Vector3 roomPosition = currentRoom.transform.position;
        Vector3 instantiatePosition = roomPosition;

        // 根据方向计算实例化位置
        switch (direction)
        {
            case Door.Direction.Up:
                instantiatePosition += new Vector3(0, roomTransformDistance, 0);
                break;
            case Door.Direction.Down:
                instantiatePosition += new Vector3(0, -roomTransformDistance, 0);
                break;
            case Door.Direction.Left:
                instantiatePosition += new Vector3(-roomTransformDistance, 0, 0);
                break;
            case Door.Direction.Right:
                instantiatePosition += new Vector3(roomTransformDistance, 0, 0);
                break;
        }

        // 实例化对象并返回
        GameObject instantiatedObject = Instantiate(objectToInstantiate, instantiatePosition, Quaternion.identity);
        return instantiatedObject;
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