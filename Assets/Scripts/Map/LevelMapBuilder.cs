using UnityEngine;
using System.Collections.Generic;

public class LevelMapBuilder : MonoBehaviour
{
    public GameObject startRoomPrefab;
    public GameObject endRoomPrefab;
    public GameObject[] roomPrefabs;
    public int minRooms;
    public int maxRooms;
    private int totalRooms;

    private string[,] gameSpace = new string[100, 100]; // 游戏空间的二维数组
    
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


        // 添加中间房间
        for (int i = 1; i < totalRooms - 1; i++)
        {
            AddRandomRoom(i);
        }

        // 添加结束房间

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

    void AddRandomRoom(int roomIndex)
    {
        GameObject randomRoom = roomPrefabs[Random.Range(0, roomPrefabs.Length)];
        int[,] grid = randomRoom.GetComponent<GridData>().grid;
        


    }

    void PlaceEndRoom(GameObject endRoom, int roomIndex)
    {
        // 放置结束房间的逻辑
        // 确保选择一个合适的位置
    }
}