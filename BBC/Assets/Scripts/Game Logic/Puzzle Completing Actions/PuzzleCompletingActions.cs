using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCompletingActions : MonoBehaviour
{
    public void OpenDoor(GameObject door)
    {
        door.GetComponent<Rigidbody>().isKinematic = false;
    }

    public void ActivateInteractivePuzzle(InteractivePuzzle puzzle)
    {
        puzzle.gameObject.SetActive(true);
    }
    public void EnableObject(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    public void DisableObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
}
