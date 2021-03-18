using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevelGoNew : MonoBehaviour
{
    public int SceneIndex;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == ("Player")) 
        {
            SceneManager.LoadScene(SceneIndex);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
