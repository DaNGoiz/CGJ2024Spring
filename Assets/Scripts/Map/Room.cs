using UnityEngine;

public class Room : MonoBehaviour
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        Null
    }

    public Direction entryDirection;
    public Direction exitDirection;

    // 入口和出口的具体位置等
}
