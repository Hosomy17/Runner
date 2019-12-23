using UnityEngine;

namespace GamepadFramework
{
    public enum TouchStates
    {
        Idle,
        Down,
        Hold,
        Up
    }

    public class GamepadInfo
    {
        public Vector2 position;
        public Vector3 acceleration;
        public Vector2 swipeDirection;
        public TouchStates touchState;
    }
}