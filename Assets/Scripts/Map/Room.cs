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
        // 根据当前门的方向，返回下一个房间对应方向的门
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
}
