using UnityEngine;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// 以enter和exit的坐标为基准，来确定下一个生成房间的位置
/// 进入到下一个房间前生成下下个房间i.e.一开始就要生成两个房间
/// 也可以在豁口回去上一个房间，镜头也会移动回去
/// 
/// 右上角小地图图标UI，显示当前房间的位置
/// 生成房间的总数阈值（会随机抽阈值内的一个数为房间总数）
/// * 方向enum再多一个null，表示不生成房间
/// 
/// 离开最后一个房间时时间放缓直到停止，除了玩家层以外屏幕发白，然后显示胜利界面
/// 
/// 光效插件研究
/// 场景光影和粒子特效
/// </summary>
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
