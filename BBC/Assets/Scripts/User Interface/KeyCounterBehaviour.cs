using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyCounterBehaviour : MonoBehaviour
{
    public int keyCount = 0;

    private void Update()
    {
        gameObject.GetComponent<Text>().text = keyCount.ToString();
    }
}
