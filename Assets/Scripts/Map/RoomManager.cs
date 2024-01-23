using UnityEngine;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

/// <summary>
/// 
/// 生成房间在map builder里做，这里只负责房间的切换和镜头的移动
/// 以enter和exit的坐标为基准，来确定下一个生成房间的位置
/// 进入到下一个房间前生成下下个房间i.e.一开始就要生成两个房间
/// 
/// 镜头移动（支持随意切换房间）
/// 
/// 右上角小地图图标UI，显示当前房间的位置
/// 生成房间的总数阈值（会随机抽阈值内的一个数为房间总数）
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
    private GameObject nextRoom; // 预加载的下一个房间
    private CinemachineVirtualCamera currentCamera;
    private Stack<GameObject> roomHistory = new Stack<GameObject>(); // 房间历史
    private HashSet<GameObject> usedRoomPrefabs = new HashSet<GameObject>(); // 已使用的房间预制体

    void Start()
    {
        LoadInitialRooms();
    }

    void LoadInitialRooms()
    {
        // 实例化并激活第一个房间
        GameObject firstRoom = Instantiate(roomPrefabs[0]);
        usedRoomPrefabs.Add(roomPrefabs[0]);
        roomHistory.Push(firstRoom);
        currentRoom = firstRoom;
        UpdateCamera(currentRoom);

        // 预加载并激活第二个房间，但确保相机聚焦在第一个房间
        PreloadNextRoom();
    }

    void PreloadNextRoom()
    {
        Room currentRoomScript = currentRoom.GetComponent<Room>();
        Room.Direction exitDirection = currentRoomScript.exitDirection;
        Room.Direction oppositeDirection = GetOppositeDirection(exitDirection);
        GameObject secondRoomPrefab = SelectNextRoomPrefab(oppositeDirection);
        if (secondRoomPrefab != null)
        {
            nextRoom = Instantiate(secondRoomPrefab);
            nextRoom.SetActive(true); // 立即激活第二个房间
            usedRoomPrefabs.Add(secondRoomPrefab);
        }
    }


    public void LoadNextRoom()
    {
        if (nextRoom != null)
        {
            nextRoom.SetActive(true); // 激活下一个房间
            roomHistory.Push(currentRoom); // 将当前房间添加到历史
            currentRoom = nextRoom;
            nextRoom = null;
            UpdateCamera(currentRoom);
            PreloadNextRoom(); // 预加载下一个房间
        }
    }

    public void LoadPreviousRoom()
    {
        if (roomHistory.Count > 0)
        {
            GameObject previousRoom = roomHistory.Pop(); // 获取上一个房间
            currentRoom = previousRoom;
            UpdateCamera(currentRoom);
        }
    }

    void UpdateCamera(GameObject newRoom)
    {
        CinemachineVirtualCamera newCamera = newRoom.GetComponentInChildren<CinemachineVirtualCamera>();
        if (newCamera != null)
        {
            if (currentCamera != null)
            {
                StartCoroutine(SmoothTransitionToNewCamera(newCamera));
            }
            else
            {
                currentCamera = newCamera;
            }
        }
    }

    void UpdateRoomAndCamera(GameObject newRoom)
    {
        currentRoom = newRoom;
        UpdateCamera(newRoom);
    }

    GameObject SelectNextRoomPrefab(Room.Direction exitDirection)
    {
        List<GameObject> validRooms = new List<GameObject>();
        foreach (var roomPrefab in roomPrefabs)
        {
            if (usedRoomPrefabs.Contains(roomPrefab))
                continue; // 跳过已使用的房间

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

        GameObject selectedRoom = validRooms[Random.Range(0, validRooms.Count)];
        usedRoomPrefabs.Add(selectedRoom); // 将选中的房间添加到已使用集合
        return selectedRoom;
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

    IEnumerator SmoothTransitionToNewCamera(CinemachineVirtualCamera newCamera)
    {
        float transitionDuration = 1.0f; // 过渡时间，可以根据需要调整

        Vector3 endPosition = newCamera.transform.position;
        Quaternion endRotation = newCamera.transform.rotation;

        Tween moveTween = currentCamera.transform.DOMove(endPosition, transitionDuration).SetEase(Ease.InOutQuad);
        Tween rotateTween = currentCamera.transform.DORotateQuaternion(endRotation, transitionDuration).SetEase(Ease.InOutQuad);

        yield return moveTween.WaitForCompletion();
        yield return rotateTween.WaitForCompletion();

        currentCamera = newCamera;
    }

}
