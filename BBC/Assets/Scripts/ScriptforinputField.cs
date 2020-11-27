using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptforinputField : MonoBehaviour
{
    public InputField InputStr;// Ввод текста
    public string inputText;
    public Text PoleInput;
    public float moveSpeed=   10f;
    public float turnSpeed = 50f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inputText == "Сдвинуться прямо()")
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        } else if (inputText == "Сдвинуться вниз()")
        {
            transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
        }
        else if (inputText == "Сдвинуться влево()")
        {
            transform.Translate(-Vector3.forward * -turnSpeed * Time.deltaTime);
        }
        else if (inputText == "Сдвинуться вправо()")
        {
            transform.Translate(-Vector3.forward * turnSpeed * Time.deltaTime);
        }
    }
    public void Reset()
    {
        InputStr.text = string.Empty;
    }
    public void GetInputInformation()
    {
        inputText = InputStr.GetComponent<Text>().text;
    }
    public void TransformPositionPlayer()
    {
        if(inputText =="Сдвинуться прямо()")
        {
            
        }
    }
}
