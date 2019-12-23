using UnityEngine;
using GamepadFramework;
using System.Collections.Generic;
using ControllerFramework;

public class CubeAI : MonoBehaviour
{
    //Down
    public Transform sensor1;
    public Transform sensor2;
    public Transform sensor3;

    //Up
    public Transform sensor4;
    public Transform sensor5;
    public Transform sensor6;

    private RaycastHit hit;
    private GameObject nearItem;
    private float distanceItem;
    private int lineItem;
    private bool airItem;

    public GamepadInfo gamepadInfo;

    private ControllerGeneric cubeController;

    private int line;

    void Awake()
    {
        gamepadInfo = new GamepadInfo();
        gamepadInfo.swipeDirection = Vector2.zero;
    }

    public void SetAI(ControllerGeneric controller)
    {
        cubeController = controller;

        //Put Cube on middle
        gamepadInfo.swipeDirection = Vector2.left;
        cubeController.NewInputs(gamepadInfo);
        gamepadInfo.swipeDirection = Vector2.left;
        cubeController.NewInputs(gamepadInfo);
        gamepadInfo.swipeDirection = Vector2.right;
        cubeController.NewInputs(gamepadInfo);
        line = 0;
    }

    void Update()
    {
        nearItem = null;
        if (Physics.Raycast(sensor1.position, Vector3.forward, out hit, 30, LayerMask.GetMask("Item")))
        {
            nearItem = hit.collider.gameObject;
            distanceItem = Vector3.Distance(sensor1.position, nearItem.transform.position);
            lineItem = -1;
            airItem = false;
        }
            

        if (Physics.Raycast(sensor2.position, Vector3.forward, out hit, 30, LayerMask.GetMask("Item")))
        {
            if(!nearItem || Vector3.Distance(sensor2.position, hit.collider.gameObject.transform.position) < distanceItem)
            {
                nearItem = hit.collider.gameObject;
                distanceItem = Vector3.Distance(sensor2.position, nearItem.transform.position);
                lineItem = 0;
                airItem = false;
            }
        }

        if (Physics.Raycast(sensor3.position, Vector3.forward, out hit, 30, LayerMask.GetMask("Item")))
        {
            if (!nearItem || Vector3.Distance(sensor3.position, hit.collider.gameObject.transform.position) < distanceItem)
            {
                nearItem = hit.collider.gameObject;
                distanceItem = Vector3.Distance(sensor3.position, nearItem.transform.position);
                lineItem = 1;
                airItem = false;
            }
        }

        if (Physics.Raycast(sensor4.position, Vector3.forward, out hit, 30, LayerMask.GetMask("Item")))
        {
            if (!nearItem || Vector3.Distance(sensor4.position, hit.collider.gameObject.transform.position) < distanceItem)
            {
                nearItem = hit.collider.gameObject;
                distanceItem = Vector3.Distance(sensor4.position, nearItem.transform.position);
                lineItem = -1;
                airItem = true;
            }
        }

        if (Physics.Raycast(sensor5.position, Vector3.forward, out hit, 30, LayerMask.GetMask("Item")))
        {
            if (!nearItem || Vector3.Distance(sensor5.position, hit.collider.gameObject.transform.position) < distanceItem)
            {
                nearItem = hit.collider.gameObject;
                distanceItem = Vector3.Distance(sensor5.position, nearItem.transform.position);
                lineItem = 0;
                airItem = true;
            }
        }

        if (Physics.Raycast(sensor6.position, Vector3.forward, out hit, 30, LayerMask.GetMask("Item")))
        {
            if (!nearItem || Vector3.Distance(sensor6.position, hit.collider.gameObject.transform.position) < distanceItem)
            {
                nearItem = hit.collider.gameObject;
                distanceItem = Vector3.Distance(sensor6.position, nearItem.transform.position);
                lineItem = 1;
                airItem = true;
            }
        }

        if(nearItem)
            SendInputs();
    }

    private void SendInputs()
    {
        gamepadInfo.swipeDirection = Vector2.zero;
        switch(nearItem.tag)
        {
            case "Coin":
                if(line > lineItem)
                {
                    gamepadInfo.swipeDirection = Vector2.left;
                    line--;
                }
                else if(line < lineItem)
                {
                    gamepadInfo.swipeDirection = Vector2.right;
                    line++;
                }
                break;
            case "Bomb":
                if (line == lineItem)
                {
                    if (lineItem == 1 || lineItem == 0)
                    {
                        gamepadInfo.swipeDirection = Vector2.left;
                        line--;
                    }
                    else
                    {
                        gamepadInfo.swipeDirection = Vector2.right;
                        line++;
                    }
                }
                break;
        }

        //Right or Left Move
        cubeController.NewInputs(gamepadInfo);

        //Jump or Not Jump
        if(airItem && nearItem.CompareTag("Coin"))
        {
            gamepadInfo.swipeDirection = Vector2.up;
            cubeController.NewInputs(gamepadInfo);
        }
    }


}
