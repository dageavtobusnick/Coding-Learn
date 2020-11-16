using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void MainPressed()
    {
        SceneManager.LoadScene(0);
    }
    public void LevelMenuPressed()
    {
        SceneManager.LoadScene(1);
    }
    public void SettingsMenuPressed()
    {
        SceneManager.LoadScene(2);
    }
    public void Level1Pressed()
    {
         SceneManager.LoadScene(5);
    }
    public void Level2Pressed()
    {
       // SceneManager.LoadScene();
    }
    public void Level3Pressed()
    {
        SceneManager.LoadScene(3);
    }
    public void Level4Pressed()
    {
        SceneManager.LoadScene(4);
    }
    public void Level5Pressed()
    {
        //SceneManager.LoadScene();
    }
    public void ExitPressed()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
