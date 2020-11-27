using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public Dropdown dropdown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DropD()
    {
        if (dropdown.value == 0)
        {
            Screen.SetResolution(1024, 768, true);
        }
        if (dropdown.value == 1)
        {
            Screen.SetResolution(1366, 768, true);
        }
        if (dropdown.value == 2)
        {
            Screen.SetResolution(1440,960, true);
        }
        if (dropdown.value == 3)
        {
            Screen.SetResolution(1600, 900, true);
        }
        if (dropdown.value == 4)
        {
            Screen.SetResolution(1920, 1080, true);
        }
    }
}
