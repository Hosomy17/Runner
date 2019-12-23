using UnityEngine;
using ControllerFramework;
using GamepadFramework;

public class CubeController : ControllerGeneric
{
    private CubeClass cubeClass;

    public override void NewInputs(GamepadInfo gamepadInfo)
    {
        Jump(gamepadInfo.swipeDirection);
        Move(gamepadInfo.swipeDirection);
        Spin(gamepadInfo.swipeDirection);
    }

    public override void TrackObject(GameObject obj)
    {
        cubeClass = obj.GetComponent<CubeClass>();
    }
    
    private void Move(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
                cubeClass.SetMovePoint(Vector2.right);
            else
                cubeClass.SetMovePoint(Vector2.left);
        }
    }

    private void Jump(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
                cubeClass.Jump();
        }
    }

    private void Spin(Vector2 direction)
    {
        direction.x += direction.y;
        direction.y = direction.x - direction.y;
        direction.x = direction.x - direction.x;
        BehaviourPhysics.AddTorque(cubeClass.gameObject, direction, cubeClass.forceJump);
    }
}
