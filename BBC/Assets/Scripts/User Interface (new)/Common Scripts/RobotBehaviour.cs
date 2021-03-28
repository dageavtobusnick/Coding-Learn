using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBehaviour : MonoBehaviour
{
    private float moveSpeed = 0.05f;
    private float rotateSpeed = 3f;
    private Vector3 movement;
    private Vector3 rotation;

    void FixedUpdate()
    {
        var deltaX = Input.GetAxis("Vertical") * moveSpeed;
        var deltaY = Input.GetAxis("Horizontal") * rotateSpeed;
        rotation = new Vector3(0, deltaY, 0);
        transform.Rotate(rotation);
        switch (transform.rotation.y % 180)
        {
            case 0:
                movement = new Vector3(0, 0, deltaX);
                break;
            case 90:
                movement = new Vector3(deltaX, 0, 0);
                break;
            default:
                movement = new Vector3(deltaX, 0, deltaX);
                break;
        }
        transform.Translate(movement);
    }
}
