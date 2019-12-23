using UnityEngine;
using ClassFramework;
using ManagerFrameWork;

public class CubeClass : ClassGeneric
{
    public Vector3 movePoint;
    public float maxPoint;
    public float minPoint;
    public float moveSpeed;
    public float pointStep;
    public float forceJump;

    private bool onFloor;

    void FixedUpdate()
    {
        movePoint.y = transform.localPosition.y;
        transform.localPosition = Vector3.Lerp(transform.localPosition, movePoint, Time.deltaTime * moveSpeed);
    }
    void OnTriggerEnter(Collider collider)
    {
        switch (collider.tag)
        {
            case "Coin":
                if (collider.name.Contains("Gold"))
                    GameScript.Instance.BigPoint(collider.gameObject);
                else
                    GameScript.Instance.Point(collider.gameObject);
                break;
            case "Bomb":
                GameScript.Instance.Damage(collider.gameObject);
                break;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Floor":
                onFloor = true;
                break;
        }
    }

    public void SetMovePoint(Vector2 direction)
    {
        if (direction == Vector2.right)
            movePoint.x += pointStep;
        else if (direction == Vector2.left)
            movePoint.x -= pointStep;

        if (movePoint.x > maxPoint)
            movePoint.x = maxPoint;
        else if (movePoint.x < minPoint)
            movePoint.x = minPoint;
    }

    public void Jump()
    {
        if (CanJump())
        {
            SoundManager.Instance.Play("SFX/Jump");
            BehaviourPhysics.Force(gameObject, Vector3.up, forceJump);
            onFloor = false;
        }
    }

    private bool CanJump()
    {
        return onFloor;
    }
}
