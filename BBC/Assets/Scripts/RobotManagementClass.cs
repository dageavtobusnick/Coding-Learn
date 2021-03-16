using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotManagementClass : MonoBehaviour
{
    public enum Direction
    {
        Forward,
        Right,
        Back,
        Left,
    }

    public int movesCount;
    public Direction direction = Direction.Forward;
    private float latency = 0.8f;
    private Animator animator;

    private IEnumerator Run(int requiredMovesCount)
    {
        movesCount = requiredMovesCount;
        switch (direction)
        {
            case Direction.Forward:
                StartCoroutine(Run_COR(new Vector3(0.0f, 0.0f, -2.0f)));
                break;
            case Direction.Left:
                StartCoroutine(Run_COR(new Vector3(2.0f, 0.0f, 0.0f)));
                break;
            case Direction.Right:
                StartCoroutine(Run_COR(new Vector3(-2.0f, 0.0f, 0.0f)));
                break;
            case Direction.Back:
                StartCoroutine(Run_COR(new Vector3(0.0f, 0.0f, 2.0f)));
                break;
        }
        yield break;
    }

    private IEnumerator Run_COR(Vector3 vector)
    {
        for (var i = 0; i < movesCount; i++)
        {
            gameObject.transform.position += vector;
            yield return new WaitForSeconds(latency);
        }     
    }

    private IEnumerator Rotate(string input)
    {
        Vector3 rotationVector;
        if (input == "Right")
        {
            rotationVector = new Vector3(0.0f, 90.0f, 0.0f);
            direction = (Direction)(((int)direction + 1) % 4);
        }
        else if (input == "Left")
        {
            rotationVector = new Vector3(0.0f, -90.0f, 0.0f);
            direction = (Direction)(((int)direction + 3) % 4);
        }
        else yield break;
        yield return StartCoroutine(Rotate_COR(rotationVector));
    }

    private IEnumerator Rotate_COR(Vector3 vector)
    {
        gameObject.transform.Rotate(vector, Space.Self);
        yield return new WaitForSeconds(latency);
    }
}
