using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCompletingActions : MonoBehaviour
{
    public void OpenDoor(GameObject door)
    {
        door.GetComponent<Rigidbody>().isKinematic = false;
    }
}
