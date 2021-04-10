using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBehaviour : MonoBehaviour
{
    private float moveSpeed = 3f;
    private float rotateSpeed = 3f;

    void FixedUpdate()
    {
        var deltaX = Input.GetAxis("Horizontal");
        var deltaZ = Input.GetAxis("Vertical");
        transform.Rotate(0f, deltaX * rotateSpeed, 0f);
        transform.Translate(new Vector3(0f, 0f, -deltaZ * moveSpeed * Time.deltaTime));   
    }
}
