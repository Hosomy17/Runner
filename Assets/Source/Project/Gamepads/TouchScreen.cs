using UnityEngine;
using GamepadFramework;

public class TouchScreen : GamepadGeneric
{
    public float sensibility;
    private GamepadInfo gamepadInfo;
    private Vector2 startSwipePosition;
    private Vector2 lastTouchPosition;

    void Awake()
    {
        gamepadInfo = new GamepadInfo
        {
            position = Vector2.zero,
            touchState = TouchStates.Idle
        };

        lastTouchPosition = Vector2.zero;
    }

    void Update()
    {
        gamepadInfo.touchState = GetTouchState(gamepadInfo.touchState);
        gamepadInfo.position = GetPosition();
        gamepadInfo.acceleration = GetAcceleration();
        gamepadInfo.swipeDirection = GetSwipeDirection();

        controller.NewInputs(gamepadInfo);
    }

    private Vector2 GetSwipeDirection()
    {
        var swipeDirection = Vector2.zero;
        if (Input.touchCount > 0)
        {
            switch (Input.touches[0].phase)
            {
                case TouchPhase.Began:
                    startSwipePosition = Input.touches[0].position;
                    break;
                case TouchPhase.Moved:
                    lastTouchPosition = Input.touches[0].position;
                    swipeDirection = lastTouchPosition - startSwipePosition;
                    break;
                case TouchPhase.Ended:
                    startSwipePosition = Vector2.zero;
                    break;
                case TouchPhase.Canceled:
                    startSwipePosition = Vector2.zero;
                    break;
                
            }
        }

        if (swipeDirection.magnitude > sensibility && startSwipePosition != Vector2.zero)
        {
            swipeDirection = swipeDirection.normalized;
            startSwipePosition = Vector2.zero;
        }
        else
        {
            swipeDirection = Vector2.zero;
        }

        return swipeDirection;
    }

    private Vector2 GetPosition()
    {
        return Input.touchCount != 0 ? Input.GetTouch(0).position : gamepadInfo.position;
    }

    private TouchStates GetTouchState(TouchStates touchState)
    {
        if (Input.touchCount != 0)
        {
            switch (touchState)
            {
                case TouchStates.Idle:
                    touchState = TouchStates.Down;
                    break;
                case TouchStates.Down:
                    touchState = TouchStates.Hold;
                    break;
            }
        }
        else
        {
            switch (touchState)
            {
                case TouchStates.Down:
                    touchState = TouchStates.Up;
                    break;
                case TouchStates.Hold:
                    touchState = TouchStates.Up;
                    break;
                case TouchStates.Up:
                    touchState = TouchStates.Idle;
                    break;
            }
        }

        return touchState;
    }

    private Vector3 GetAcceleration()
    {
        return Input.acceleration;
    }

}