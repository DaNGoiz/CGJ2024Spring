using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class LevelMapBuilder : MonoBehaviour
{
    public GameObject initialRoom;
    public GameObject lastRoom;
    public List<GameObject> roomPrefabs;

    [Header("Transform")]
    public float transformPosition = 2f;
    public float roomTransformDistance = 10f;
    public GameObject currentRoom;

    private GameObject[,] roomArray = new GameObject[50, 50];
    private int currentRoomX = 25;
    private int currentRoomY = 25;


    void Start()
    {
        GameObject firstRoom = Instantiate(initialRoom);
        GameObject roomsParent = GameObject.Find("Rooms");
        if (roomsParent == null)
        {
            roomsParent = new GameObject("Rooms");
        }
        firstRoom.transform.SetParent(roomsParent.transform);

        currentRoom = firstRoom;
        currentRoom.SetActive(true);

        roomArray[25, 25] = currentRoom;
    }

    /// <summary>
    /// 1. 检查当前方向是否已经绑定了下一个门
    /// 2. 如果没有绑定，随机选择一个房间，检查是否有反方向的门，如果没有，就重新生成
    /// 3. 如果有，就生成房间，将其加入对应方向的房间数组，并把当前房间赋值给它
    /// 4. 跳转到新房间，检查在四个方向是否有对应的房间，如果有，并双方房间都有对应方向的门，就互相绑定门，否则就生成墙
    /// 5. 把玩家传送到新房间
    /// </summary>

    public GameObject nextRoomDoor; // debug
    public void OnDoorTriggered(GameObject door)
    {
        // 1. 检查当前方向是否已经绑定了下一个门
        Room currentRoomScript = currentRoom.GetComponent<Room>();
        nextRoomDoor = currentRoomScript.GetCorrespondingDoorInNextRoom(door.GetComponent<Door>().doorDirection);

        if(nextRoomDoor == null)
        {
            // 2. 如果没有绑定，随机选择一个房间，检查是否有反方向的门，如果没有，就重新生成
            // 3. 如果有，就生成房间，将其加入对应方向的房间数组，并把当前房间赋值给它
            nextRoomDoor = SelectNextRoom(door.GetComponent<Door>().doorDirection, door);
            // 4. 跳转到新房间，检查在四个方向是否有对应的房间，如果有，并双方房间都有对应方向的门，就互相绑定门，否则就生成墙
            CheckRoomDoorsBinding(currentRoom);
        }
        else
        {
            // 寻找nextRoomDoor对应的房间，并把currentRoom赋值给它
            currentRoom = GetRoomInDirection(door.GetComponent<Door>().doorDirection); // think it's not working
        }

        // 5. 把玩家传送到新房间
        MovePlayerToRoom(nextRoomDoor); // add camera movement

        // 6. 检测是否被完全包围，如果是就重新开一个门，并生成最后一个房间
    }

    // return nextRoomDoor
    GameObject SelectNextRoom(Door.Direction currentDoorDirection, GameObject door)
    {
        // 2. 随机选择一个房间，检查是否有反方向的门，如果没有，就重新生成
        // 3. 如果有，就生成房间，将其加入对应方向的房间数组，并把当前房间赋值给它
        GameObject nextRoom = roomPrefabs[Random.Range(0, roomPrefabs.Count)];
        Room nextRoomScript = nextRoom.GetComponent<Room>();
        if(nextRoomScript.GetCorrespondingDoor
            (Door.GetOppositeDirection(currentDoorDirection))
            != null)
        {
            nextRoom = InstantiateAtDirection(currentRoom, currentDoorDirection, nextRoom);
            nextRoom.SetActive(true);
            bool isAdded = TryAddRoomToArray(nextRoom, currentDoorDirection);
            if(!isAdded)
            {
                Debug.Log("Warning: Room already exists, but new room still added");
            }
            currentRoom = nextRoom;
            return nextRoomScript.GetCorrespondingDoor(Door.GetOppositeDirection(currentDoorDirection));
        }
        else
        {
            return SelectNextRoom(currentDoorDirection, door);
        }
    }

    public GameObject InstantiateAtDirection(GameObject currentRoom, Door.Direction direction, GameObject objectToInstantiate)
    {
        Vector3 roomPosition = currentRoom.transform.position;
        Vector3 instantiatePosition = roomPosition;

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

        GameObject instantiatedObject = Instantiate(objectToInstantiate, instantiatePosition, Quaternion.identity);

        GameObject roomsParent = GameObject.Find("Rooms");
        if (roomsParent == null)
        {
            roomsParent = new GameObject("Rooms");
        }

        instantiatedObject.transform.SetParent(roomsParent.transform);
        return instantiatedObject;
    }

    bool TryAddRoomToArray(GameObject room, Door.Direction direction)
    {
        int x = currentRoomX;
        int y = currentRoomY;
        if (direction == Door.Direction.Up){x -= 1;}
        else if (direction == Door.Direction.Down){x += 1;}
        else if (direction == Door.Direction.Left){y -= 1;}
        else if (direction == Door.Direction.Right){y += 1;}

        if(roomArray[x, y] == null)
        {
            currentRoomX = x;
            currentRoomY = y;
            roomArray[currentRoomX, currentRoomY] = room;
            return true;
        }
        else{return false;}
    }

    void CheckRoomDoorsBinding(GameObject room)
    {
        //4. 检查在四个方向是否有对应的房间，如果有，并双方房间都有对应方向的门，就互相绑定门，否则就生成墙
        CheckSingleDoorBinding(Door.Direction.Up, room);
        CheckSingleDoorBinding(Door.Direction.Down, room);
        CheckSingleDoorBinding(Door.Direction.Left, room);
        CheckSingleDoorBinding(Door.Direction.Right, room);
    }

    void CheckSingleDoorBinding(Door.Direction currentDoorDirection, GameObject room)
    {
        //4. 检查当前方向是否有对应的房间，如果有，并双方房间都有对应方向的门，就互相绑定门，否则就生成墙
        GameObject nextRoom = GetRoomInDirection(currentDoorDirection);
        if(nextRoom != null)
        {
            Room currentRoomScript = room.GetComponent<Room>();
            Room nextRoomScript = nextRoom.GetComponent<Room>();

            Door.Direction nextDoorDirection = Door.GetOppositeDirection(currentDoorDirection);

            GameObject currentRoomDoor = currentRoomScript.GetCorrespondingDoor(currentDoorDirection);
            GameObject nextRoomDoor = nextRoomScript.GetCorrespondingDoor(nextDoorDirection);

            if(currentRoomDoor != null && nextRoomDoor != null)
            {
                currentRoomScript.BindDoor(currentDoorDirection, nextRoomDoor);
                nextRoomScript.BindDoor(nextDoorDirection, currentRoomDoor);
            }
            else
            {
                RemoveDoorAndAddWall(currentDoorDirection);
            }
        }
        else
        {
            RemoveWall(currentDoorDirection);
        }
    }

    GameObject GetRoomInDirection(Door.Direction direction)
    {
        GameObject room;
        int x = currentRoomX;
        int y = currentRoomY;
        
        if (direction == Door.Direction.Up){x -= 1;}
        else if (direction == Door.Direction.Down){x += 1;}
        else if (direction == Door.Direction.Left){y -= 1;}
        else if (direction == Door.Direction.Right){y += 1;}

        room = roomArray[x, y];
        return room;
    }

    void MovePlayerToRoom(GameObject door)
    {
        Vector3 roomPosition = currentRoom.transform.position;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (door.GetComponent<Door>().doorDirection == Door.Direction.Up)
            {
                player.transform.position = roomPosition + door.transform.position + new Vector3(0, -transformPosition, 0);
            }
            else if (door.GetComponent<Door>().doorDirection == Door.Direction.Down)
            {
                player.transform.position = roomPosition + door.transform.position + new Vector3(0, transformPosition, 0);
            }
            else if (door.GetComponent<Door>().doorDirection == Door.Direction.Left)
            {
                player.transform.position = roomPosition + door.transform.position + new Vector3(transformPosition, 0, 0);
            }
            else if (door.GetComponent<Door>().doorDirection == Door.Direction.Right)
            {
                player.transform.position = roomPosition + door.transform.position + new Vector3(-transformPosition, 0, 0);
            }
            else
            {
                player.transform.position = door.transform.position;
            }
        }
    }

    public void RemoveWall(Door.Direction direction)
    {
        GameObject wall = currentRoom.GetComponent<Room>().GetCorrespondingWall(direction);
        if(wall != null)
        {
            wall.SetActive(false);
        }
    }

    // This method is called when building the room
    public void RemoveDoorAndAddWall(Door.Direction direction)
    {
        // 1. remove door component + grid
        // Tilemap tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
        // GameObject door = currentRoom.GetComponent<Room>().GetCorrespondingDoor(direction);
        // Door.SetDoorDirection(door, Door.Direction.Null);

        // if(direction == Door.Direction.Up)
        // {
        //     // 将tilemap里(-1,5,0)位置的tile变为空
        //     GameObject.Find("Tilemap").GetComponent<Tilemap>().SetTile(new Vector3Int(-1, 5, 0), null);
        // }
        // else if(direction == Door.Direction.Down)
        // {
        //     // 将tilemap里(-1,-5,0)位置的tile变为空
        //     GameObject.Find("Tilemap").GetComponent<Tilemap>().SetTile(new Vector3Int(-1, -5, 0), null);
        // }
        // else if(direction == Door.Direction.Left)
        // {
        //     // 将tilemap里(-12,0,0)位置的tile变为空
        //     GameObject.Find("Tilemap").GetComponent<Tilemap>().SetTile(new Vector3Int(-12, 0, 0), null);
        // }
        // else if(direction == Door.Direction.Right)
        // {
        //     // 将tilemap里(10,0,0)位置的tile变为空
        //     GameObject.Find("Tilemap").GetComponent<Tilemap>().SetTile(new Vector3Int(10, 0, 0), null);
        // }

        // 2. activate wall
        GameObject wall = currentRoom.GetComponent<Room>().GetCorrespondingWall(direction);
        wall.SetActive(true);
        wall.GetComponent<Wall>().isNaturalWall = true;
    }

    public void ActivateBannerWhenFight()
    {
        GameObject currentRoom = GameObject.Find("Rooms").transform.GetChild(0).gameObject;
        Room currentRoomScript = currentRoom.GetComponent<Room>();
        if (!currentRoomScript.upWall.GetComponent<Wall>().isNaturalWall)
        {
            currentRoomScript.upWall.GetComponent<Wall>().ActivateBanner();
        }
        if (!currentRoomScript.downWall.GetComponent<Wall>().isNaturalWall)
        {
            currentRoomScript.downWall.GetComponent<Wall>().ActivateBanner();
        }
        if (!currentRoomScript.leftWall.GetComponent<Wall>().isNaturalWall)
        {
            currentRoomScript.leftWall.GetComponent<Wall>().ActivateBanner();
        }
        if (!currentRoomScript.rightWall.GetComponent<Wall>().isNaturalWall)
        {
            currentRoomScript.rightWall.GetComponent<Wall>().ActivateBanner();
        }
    }

    public void DisactivateBannerAfterFight()
    {
        GameObject currentRoom = GameObject.Find("Rooms").transform.GetChild(0).gameObject;
        Room currentRoomScript = currentRoom.GetComponent<Room>();
        if (!currentRoomScript.upWall.GetComponent<Wall>().isNaturalWall)
        {
            currentRoomScript.upWall.GetComponent<Wall>().DisactivateBanner();
        }
        if (!currentRoomScript.downWall.GetComponent<Wall>().isNaturalWall)
        {
            currentRoomScript.downWall.GetComponent<Wall>().DisactivateBanner();
        }
        if (!currentRoomScript.leftWall.GetComponent<Wall>().isNaturalWall)
        {
            currentRoomScript.leftWall.GetComponent<Wall>().DisactivateBanner();
        }
        if (!currentRoomScript.rightWall.GetComponent<Wall>().isNaturalWall)
        {
            currentRoomScript.rightWall.GetComponent<Wall>().DisactivateBanner();
        }
    }
}