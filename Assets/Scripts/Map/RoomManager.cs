using UnityEngine;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class RoomNode
{
    public GameObject Room;
    public Dictionary<Room.Direction, RoomNode> ConnectedRooms = new Dictionary<Room.Direction, RoomNode>();
}

public class RoomManager : MonoBehaviour
{
    public List<GameObject> roomPrefabs;
    private RoomNode rootNode;
    private RoomNode currentRoomNode;
    private Stack<RoomNode> roomHistory = new Stack<RoomNode>(); // 用于追踪房间历史
    private CinemachineVirtualCamera currentCamera;

    void Start()
    {
        BuildRoomTree();
        LoadInitialRoom();
    }

    void BuildRoomTree()
    {
        // 假设 roomPrefabs 列表中有至少一个房间预制体
        rootNode = new RoomNode { Room = Instantiate(roomPrefabs[0]) };
        Queue<RoomNode> queue = new Queue<RoomNode>();
        queue.Enqueue(rootNode);

        while (queue.Count > 0)
        {
            RoomNode current = queue.Dequeue();
            Room currentRoomScript = current.Room.GetComponent<Room>();
            Room.Direction exitDirection = currentRoomScript.exitDirection;

            foreach (var prefab in roomPrefabs)
            {
                Room prefabRoomScript = prefab.GetComponent<Room>();
                if (prefabRoomScript.entryDirection == GetOppositeDirection(exitDirection))
                {
                    RoomNode newNode = new RoomNode { Room = Instantiate(prefab) };
                    current.ConnectedRooms.Add(exitDirection, newNode);
                    queue.Enqueue(newNode);
                    break;
                }
            }
        }
    }

    void LoadInitialRoom()
    {
        currentRoomNode = rootNode;
        UpdateCamera(currentRoomNode.Room);
    }

    public void LoadNextRoom(Room.Direction direction)
    {
        if (currentRoomNode != null && currentRoomNode.ConnectedRooms.ContainsKey(direction))
        {
            RoomNode nextRoomNode = currentRoomNode.ConnectedRooms[direction];
            if (nextRoomNode != null)
            {
                roomHistory.Push(currentRoomNode); // 将当前房间节点添加到历史记录
                currentRoomNode = nextRoomNode;
                UpdateCamera(currentRoomNode.Room);
            }
        }
    }

    public void LoadPreviousRoom()
    {
        if (roomHistory.Count > 0)
        {
            currentRoomNode = roomHistory.Pop();
            UpdateCamera(currentRoomNode.Room);
        }
    }

    void UpdateCamera(GameObject newRoom)
    {
        CinemachineVirtualCamera newCamera = newRoom.GetComponentInChildren<CinemachineVirtualCamera>();
        if (newCamera != null && newCamera != currentCamera)
        {
            StartCoroutine(SmoothTransitionToNewCamera(newCamera));
        }
    }

    IEnumerator SmoothTransitionToNewCamera(CinemachineVirtualCamera newCamera)
    {
        float transitionDuration = 1.0f; // 过渡时间，可以根据需要调整

        Vector3 startPosition = currentCamera.transform.position;
        Quaternion startRotation = currentCamera.transform.rotation;

        Vector3 endPosition = newCamera.transform.position;
        Quaternion endRotation = newCamera.transform.rotation;

        Tween moveTween = currentCamera.transform.DOMove(endPosition, transitionDuration).SetEase(Ease.InOutQuad);
        Tween rotateTween = currentCamera.transform.DORotateQuaternion(endRotation, transitionDuration).SetEase(Ease.InOutQuad);

        yield return moveTween.WaitForCompletion();
        yield return rotateTween.WaitForCompletion();

        // 更新当前相机引用
        currentCamera = newCamera;

        // 确保新相机处于活跃状态
        newCamera.gameObject.SetActive(true);
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
}