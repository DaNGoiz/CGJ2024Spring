using UnityEngine;

public class Room : MonoBehaviour
{
    public bool up, down, left, right;
    public GameObject currentUpDoor, currentDownDoor, currentLeftDoor, currentRightDoor; // 当前房间的门
    public GameObject nextRoomDownDoor, nextRoomUpDoor, nextRoomRightDoor, nextRoomLeftDoor; // 通往下一个房间的门

    void OnValidate()
    {
        if (currentUpDoor != null) currentUpDoor.SetActive(up);
        if (currentDownDoor != null) currentDownDoor.SetActive(down);
        if (currentLeftDoor != null) currentLeftDoor.SetActive(left);
        if (currentRightDoor != null) currentRightDoor.SetActive(right);

        if (nextRoomDownDoor != null) nextRoomDownDoor.SetActive(up);
        if (nextRoomUpDoor != null) nextRoomUpDoor.SetActive(down);
        if (nextRoomRightDoor != null) nextRoomRightDoor.SetActive(left);
        if (nextRoomLeftDoor != null) nextRoomLeftDoor.SetActive(right);
    }

    public GameObject GetCorrespondingDoorInNextRoom(Door.Direction currentDoorDirection)
    {
        switch (currentDoorDirection)
        {
            case Door.Direction.Up:
                return nextRoomDownDoor;
            case Door.Direction.Down:
                return nextRoomUpDoor;
            case Door.Direction.Left:
                return nextRoomRightDoor;
            case Door.Direction.Right:
                return nextRoomLeftDoor;
            default:
                return null;
        }
    }

    public GameObject GetCorrespondingDoor(Door.Direction currentDoorDirection)
    {
        switch (currentDoorDirection)
        {
            case Door.Direction.Up:
                return currentUpDoor;
            case Door.Direction.Down:
                return currentDownDoor;
            case Door.Direction.Left:
                return currentLeftDoor;
            case Door.Direction.Right:
                return currentRightDoor;
            default:
                return null;
        }
    }

    public bool HasDoor(Door.Direction direction)
    {
        switch (direction)
        {
            case Door.Direction.Up:
                return down;
            case Door.Direction.Down:
                return up;
            case Door.Direction.Left:
                return right;
            case Door.Direction.Right:
                return left;
            default:
                return false;
        }
    }

    public void BindDoor(Door.Direction direction, GameObject door)
    {
        switch (direction)
        {
            case Door.Direction.Up:
                nextRoomDownDoor = door;
                break;
            case Door.Direction.Down:
                nextRoomUpDoor = door;
                break;
            case Door.Direction.Left:
                nextRoomRightDoor = door;
                break;
            case Door.Direction.Right:
                nextRoomLeftDoor = door;
                break;
        }
    }
}
