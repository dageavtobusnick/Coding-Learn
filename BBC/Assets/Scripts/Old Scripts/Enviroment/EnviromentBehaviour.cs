using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject.Find("Snowman").GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject.Find("Snowman").GetComponent<SpriteRenderer>().sortingOrder = 2;
    }
}
