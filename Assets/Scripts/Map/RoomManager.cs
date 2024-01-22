using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class RoomManager : MonoBehaviour
{
    [Header("Room Prefabs")]
    public List<GameObject> roomPrefabs; // 0: Start Room, 1: End Room, other logic?
    private GameObject currentRoom;
    public CinemachineVirtualCamera currentCamera;

    void Start()
    {
        LoadInitialRoom();
    }

    void LoadInitialRoom()
    {
        currentRoom = Instantiate(roomPrefabs[0]);
        UpdateCamera(currentRoom);
    }

    public void LoadNextRoom()
    {
        Room currentRoomScript = currentRoom.GetComponent<Room>();
        Room.Direction exitDirection = currentRoomScript.exitDirection;

        GameObject nextRoomPrefab = SelectNextRoomPrefab(exitDirection);
        Destroy(currentRoom);
        currentRoom = Instantiate(nextRoomPrefab);
        UpdateCamera(currentRoom);
    }

    GameObject SelectNextRoomPrefab(Room.Direction exitDirection)
    {
        List<GameObject> validRooms = new List<GameObject>();
        foreach (var roomPrefab in roomPrefabs)
        {
            Room roomScript = roomPrefab.GetComponent<Room>();
            if (roomScript.entryDirection == exitDirection)
            {
                validRooms.Add(roomPrefab);
            }
        }
        return validRooms[Random.Range(0, validRooms.Count)];
    }

    void UpdateCamera(GameObject newRoom)
    {
        if (currentCamera != null)
        {
            currentCamera.gameObject.SetActive(false); // 禁用当前相机
        }

        CinemachineVirtualCamera newCamera = newRoom.GetComponentInChildren<CinemachineVirtualCamera>();
        if (newCamera != null)
        {
            newCamera.gameObject.SetActive(true); // 激活新相机
            currentCamera = newCamera;
        }
    }
}
