using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheoryImageBehaviour : MonoBehaviour
{
    public Sprite[] imagesTheme1;
    public Sprite[] imagesTheme2;
    public Sprite[] imagesTheme3;
    public Sprite[] imagesTheme4;
    public Sprite[] imagesTheme5;
    public int themeNumber = 1;
    public int imageNumber = 0;

    void Update()
    {
        switch (themeNumber)
        {
            case 1:
                gameObject.GetComponent<Image>().sprite = imagesTheme1[imageNumber];
                break;
            case 2:
                gameObject.GetComponent<Image>().sprite = imagesTheme2[imageNumber];
                break;
            case 3:
                gameObject.GetComponent<Image>().sprite = imagesTheme3[imageNumber];
                break;
            case 4:
                gameObject.GetComponent<Image>().sprite = imagesTheme4[imageNumber];
                break;
            case 5:
                gameObject.GetComponent<Image>().sprite = imagesTheme5[imageNumber];
                break;
        }
    }
}
