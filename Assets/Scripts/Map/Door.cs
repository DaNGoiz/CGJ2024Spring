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
}
