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

    private GameObject currentRoom;

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

        bool isAdded = TryAddRoomToArray(nextRoom, currentDoorDirection);
        
        if (nextRoomScript.HasDoor(currentDoorDirection) && isAdded)
        {
            // nextRoom = Instantiate(nextRoom);
            nextRoom = InstantiateAtDirection(currentRoom, currentDoorDirection, nextRoom);
            nextRoom.SetActive(true);

            // nextRoomScript = nextRoom.GetComponent<Room>();
              
            CheckRoomDoorsBinding(currentRoom);
            
            Door.Direction nextDoorDirection = Door.GetOppositeDirection(currentDoorDirection);
            GameObject nextRoomDoor = nextRoomScript.GetCorrespondingDoor(nextDoorDirection);
            
            currentRoom = nextRoom;
            return nextRoomDoor;
        }
        else
        {
            return SelectNextRoom(currentDoorDirection, door);
        }
    }

    bool TryAddRoomToArray(GameObject room, Door.Direction direction)
    {
        int x = currentRoomX;
        int y = currentRoomY;
        if (direction == Door.Direction.Up)
        {
            x -= 1;
        }
        else if (direction == Door.Direction.Down)
        {
            x += 1;
        }
        else if (direction == Door.Direction.Left)
        {
            y -= 1;
        }
        else if (direction == Door.Direction.Right)
        {
            y += 1;
        }

        if(roomArray[x, y] == null)
        {
            currentRoomX = x;
            currentRoomY = y;
            roomArray[currentRoomX, currentRoomY] = room;
            return true;
        }
        else
        {
            return false;
        }
    }

    void CheckRoomDoorsBinding(GameObject room)
    {
        CheckSingleDoorBinding(Door.Direction.Up, room);
        CheckSingleDoorBinding(Door.Direction.Down, room);
        CheckSingleDoorBinding(Door.Direction.Left, room);
        CheckSingleDoorBinding(Door.Direction.Right, room);
    }

    void CheckSingleDoorBinding(Door.Direction currentDoorDirection, GameObject room)
    {
        Room currentRoomScript = room.GetComponent<Room>();
        if (currentRoomScript.HasDoor(currentDoorDirection))
        {
            GameObject nextRoom = GetRoomInDirection(Door.GetOppositeDirection(currentDoorDirection));
            if (nextRoom != null)
            {
                Room nextRoomScript = nextRoom.GetComponent<Room>();
                // GameObject doorOfOtherSide = roomScript.GetCorrespondingDoor(Door.GetOppositeDirection(direction));
                // roomScript.BindDoor(direction, doorOfOtherSide);

                Door.Direction nextDoorDirection = Door.GetOppositeDirection(currentDoorDirection);
                GameObject nextRoomDoor = nextRoomScript.GetCorrespondingDoor(nextDoorDirection);
                currentRoomScript.BindDoor(Door.GetOppositeDirection(currentDoorDirection), nextRoomDoor);
                nextRoomScript.BindDoor(nextDoorDirection, room.GetComponent<Room>().GetCorrespondingDoor(currentDoorDirection));
            }
            else
            {
                RemoveDoorAndAddWall(currentDoorDirection);
            }
        }
    }

    GameObject GetRoomInDirection(Door.Direction direction)
    {
        GameObject room;
        int x = currentRoomX;
        int y = currentRoomY;
        if (direction == Door.Direction.Up)
        {
            x -= 1;
        }
        else if (direction == Door.Direction.Down)
        {
            x += 1;
        }
        else if (direction == Door.Direction.Left)
        {
            y -= 1;
        }
        else if (direction == Door.Direction.Right)
        {
            y += 1;
        }

        room = roomArray[x, y];
        return room;
    }

    void CheckOppositeRoomDoorBinding(Door.Direction direction, GameObject room, GameObject doorOfOtherSide)
    {
        Room roomScript = room.GetComponent<Room>();
        if (roomScript.HasDoor(direction))
        {
            roomScript.BindDoor(Door.GetOppositeDirection(direction), doorOfOtherSide);
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

    // This method is called when building the room
    public void RemoveDoorAndAddWall(Door.Direction direction)
    {
        // 1. remove door component + grid
        GameObject door = currentRoom.GetComponent<Room>().GetCorrespondingDoor(direction);
        Destroy(door.GetComponent<Door>());

        if(direction == Door.Direction.Up)
        {
            // 将tilemap里(-1,5,0)位置的tile变为空
            GameObject.Find("Tilemap").GetComponent<Tilemap>().SetTile(new Vector3Int(-1, 5, 0), null);
        }
        else if(direction == Door.Direction.Down)
        {
            // 将tilemap里(-1,-5,0)位置的tile变为空
            GameObject.Find("Tilemap").GetComponent<Tilemap>().SetTile(new Vector3Int(-1, -5, 0), null);
        }
        else if(direction == Door.Direction.Left)
        {
            // 将tilemap里(-12,0,0)位置的tile变为空
            GameObject.Find("Tilemap").GetComponent<Tilemap>().SetTile(new Vector3Int(-12, 0, 0), null);
        }
        else if(direction == Door.Direction.Right)
        {
            // 将tilemap里(10,0,0)位置的tile变为空
            GameObject.Find("Tilemap").GetComponent<Tilemap>().SetTile(new Vector3Int(10, 0, 0), null);
        }

        // 2. activate wall
        GameObject wall = currentRoom.GetComponent<Room>().GetCorrespondingWall(direction);
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