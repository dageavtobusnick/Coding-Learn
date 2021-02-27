using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
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
        SceneManager.LoadScene(7);
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
        SceneManager.LoadScene(6);
    }
    public void Level3d1Pressed()
    {
        SceneManager.LoadScene(9);
    }
    public void Level3d2Pressed()
    {
        SceneManager.LoadScene(10);
    }
    public void Partners()
    {
        SceneManager.LoadScene(8);
    }
    public void ExitPressed()
    {
        Application.Quit();
    }

}
