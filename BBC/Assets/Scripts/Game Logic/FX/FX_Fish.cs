using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX_Fish : MonoBehaviour
{
    public bool isFoodFish;
    public GameObject[] arrayFishFx;
    public GameObject positionFx;

    void Start()
    {
        isFoodFish = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFoodFish)
        {
            for(int i = 0; i < arrayFishFx.Length; i++)
            {
                arrayFishFx[i].transform.position = positionFx.transform.position;
            }
        }
    }
}
