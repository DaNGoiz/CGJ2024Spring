using UnityEngine;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{
    public List<GameObject> roomPrefabs;
    private GameObject currentRoom;
    private CinemachineVirtualCamera currentCamera;

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
        Room.Direction oppositeDirection = GetOppositeDirection(exitDirection);

        GameObject nextRoomPrefab = SelectNextRoomPrefab(oppositeDirection);
        GameObject nextRoom = Instantiate(nextRoomPrefab);
        UpdateCamera(nextRoom);
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

        if (validRooms.Count == 0)
        {
            Debug.LogError("No valid rooms found for the given exit direction.");
            return null;
        }

        return validRooms[Random.Range(0, validRooms.Count)];
    }

    Room.Direction GetOppositeDirection(Room.Direction direction)
    {
        switch (direction)
        {
            case Room.Direction.Up: return Room.Direction.Down;
            case Room.Direction.Down: return Room.Direction.Up;
            case Room.Direction.Left: return Room.Direction.Right;
            case Room.Direction.Right: return Room.Direction.Left;
            default: return direction;
        }
    }

    void UpdateCamera(GameObject newRoom)
    {
        CinemachineVirtualCamera newCamera = newRoom.GetComponentInChildren<CinemachineVirtualCamera>();
        if (newCamera != null && currentCamera != null)
        {
            StartCoroutine(SmoothTransitionToNewCamera(newCamera));
        }
        else if (newCamera != null)
        {
            currentCamera = newCamera;
        }
    }

    IEnumerator SmoothTransitionToNewCamera(CinemachineVirtualCamera newCamera)
    {
        float transitionDuration = 1.0f; // 过渡时间，可以根据需要调整
        float elapsedTime = 0;

        Vector3 startPosition = currentCamera.transform.position;
        Quaternion startRotation = currentCamera.transform.rotation;
        Vector3 endPosition = newCamera.transform.position;
        Quaternion endRotation = newCamera.transform.rotation;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionDuration;

            currentCamera.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            currentCamera.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);

            yield return null;
        }

        currentCamera.transform.position = endPosition;
        currentCamera.transform.rotation = endRotation;
        currentCamera = newCamera;
    }
}
