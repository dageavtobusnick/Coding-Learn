using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RubyCounterBehaviour : MonoBehaviour
{
    public int rubyCount = 0;

    void Update()
    {
        gameObject.GetComponent<Text>().text = rubyCount.ToString();
    }
}
