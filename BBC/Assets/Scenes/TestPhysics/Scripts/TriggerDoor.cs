using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor : MonoBehaviour
{
    public string tagOpenDoor;
    public Rigidbody Door_Rigidbody;
    private void Start()
    {
        Door_Rigidbody.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == tagOpenDoor)
        {
            Door_Rigidbody.isKinematic = false;
            Debug.Log("Дверь открыта");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == tagOpenDoor)
        {
            Door_Rigidbody.isKinematic = true;
            Debug.Log("Дверь закрыта");
        }
    }
}
