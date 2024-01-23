using UnityEngine;
using System.Collections.Generic;

public class LevelMapBuilder : MonoBehaviour
{
    public GameObject startRoomPrefab;
    public GameObject endRoomPrefab;
    public GameObject[] roomPrefabs;
    public int minRooms;
    public int maxRooms;

    private string[,] gameSpace = new string[100, 100]; // 游戏空间的二维数组
    private Dictionary<string, GameObject> roomDictionary = new Dictionary<string, GameObject>(); // 房间名与地形的对应关系
    private List<GameObject> mapRooms = new List<GameObject>(); // 存储地图上的房间
    private int totalRooms;

    void Start()
    {
        InitializeGameSpace();
        BuildMap();
    }

    void InitializeGameSpace()
    {
        for (int i = 0; i < gameSpace.GetLength(0); i++)
        {
            for (int j = 0; j < gameSpace.GetLength(1); j++)
            {
                gameSpace[i, j] = "0";
            }
        }
    }

    void BuildMap()
    {
        totalRooms = Random.Range(minRooms, maxRooms + 1);

        // 添加开始房间
        GameObject startRoom = Instantiate(startRoomPrefab);
        mapRooms.Add(startRoom);
        PlaceRoomInGameSpace(startRoom, 50, 50, "A");

        // 添加中间房间
        for (int i = 1; i < totalRooms - 1; i++)
        {
            AddRandomRoom(i);
        }

        // 添加结束房间
        GameObject endRoom = Instantiate(endRoomPrefab);
        mapRooms.Add(endRoom);
        PlaceEndRoom(endRoom, totalRooms - 1);
    }

    void PlaceRoomInGameSpace(GameObject room, int x, int y, string roomName)
    {
        // 在游戏空间的二维数组中记录房间的占位
        gameSpace[x, y] = roomName;
        roomDictionary.Add(roomName, room);
    }

    void AddRandomRoom(int roomIndex)
    {
        // 随机抽取并添加房间的逻辑
        // 确保房间不重复并且能够连接
        // 记录房间名和地形
    }

    void PlaceEndRoom(GameObject endRoom, int roomIndex)
    {
        // 放置结束房间的逻辑
        // 确保选择一个合适的位置
    }
}
