using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseTheoryButtonBehaviour : MonoBehaviour
{
    public void CloseTask()
    {
        GameObject player = GameObject.Find("Snowman");
        InputField theoryField = GameObject.Find("TheoryField").GetComponent<InputField>();
        theoryField.text = "";
        theoryField.transform.position = theoryField.GetComponent<TheoryFieldBehaviour>().TurnOffPosition;
        player.GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePosition;
    }
}
