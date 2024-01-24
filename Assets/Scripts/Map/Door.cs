using UnityEngine;

public class Door : MonoBehaviour
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public Direction doorDirection;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(doorDirection + " Door Triggered");
            LevelMapBuilder levelMapBuilder = GameObject.Find("RoomManager").GetComponent<LevelMapBuilder>();
            levelMapBuilder.OnDoorTriggered(gameObject);
        }
    }

    public static Direction GetOppositeDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return Direction.Down;
            case Direction.Down:
                return Direction.Up;
            case Direction.Left:
                return Direction.Right;
            case Direction.Right:
                return Direction.Left;
            default:
                return Direction.Up;
        }
    }
}
