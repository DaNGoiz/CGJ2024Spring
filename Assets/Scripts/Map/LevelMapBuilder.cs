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
        // 在空间的二维数组中记录房间的占位
        gameSpace[x, y] = roomName;
        roomDictionary.Add(roomName, room);
    }

    /// <summary>
    /// 1. 随机抽取一个房间
    /// 2. 检测房间在指定方向是否存在门
    /// 3. 把两个门的位置对准，随后检测房间放在坐标里是否与其他房间重叠
    /// 4. 连接其他方向上可能两两相对的门
    /// 
    /// 确认以后就给房间之间绑定门
    /// 
    /// 虚拟对接 & 实际对接都要存在，先有虚拟对接，然后实际对接就是把虚拟对接的门的位置对准然后传送。
    /// 改为3x4的核心
    /// </summary>
    /// <param name="roomIndex"></param>
    void AddRandomRoom(int roomIndex)
    {
        // 随机抽取并添加房间的逻辑
        // 确保房间不重复并且能够连接

        // 记录房间名
    }

    void PlaceEndRoom(GameObject endRoom, int roomIndex)
    {
        // 放置结束房间的逻辑
        // 确保选择一个合适的位置
    }
}
