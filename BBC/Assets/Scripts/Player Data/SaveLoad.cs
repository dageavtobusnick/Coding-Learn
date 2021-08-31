using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoad : MonoBehaviour
{
    public GameObject Canvas;

    public void Save()
    {
        var gameData = Canvas.GetComponent<GameData>();
        for (var i = 0; i < gameData.HasTasksCompleted.Count; i++)
            PlayerPrefs.SetInt("Task " + (i + 1) + " completed", gameData.HasTasksCompleted[i] ? 1 : 0);

        var player = gameData.Player;
        PlayerPrefs.SetFloat("PositionX", player.transform.position.x);
        PlayerPrefs.SetFloat("PositionY", player.transform.position.y);
        PlayerPrefs.SetFloat("PositionZ", player.transform.position.z);

        PlayerPrefs.SetFloat("RotationX", player.transform.eulerAngles.x);
        PlayerPrefs.SetFloat("RotationY", player.transform.eulerAngles.y);
        PlayerPrefs.SetFloat("RotationZ", player.transform.eulerAngles.z);

        PlayerPrefs.SetInt("CoinsCount", gameData.CoinsCount);
        PlayerPrefs.SetInt("TipsCount", gameData.TipsCount);
        PlayerPrefs.SetInt("TaskItemsCount", gameData.TaskItemsCount);
        PlayerPrefs.SetInt("SceneIndex", SceneManager.GetActiveScene().buildIndex);

        var availableTipsCounts = Canvas.GetComponent<PadBehaviour>().AvailableTipsCounts;
        for (var i = 0; i < availableTipsCounts.Count; i++)
            PlayerPrefs.SetInt("Available Tips Count (Task " + (i + 1) + ")", availableTipsCounts[i]);

        var coins = GameObject.Find("Coins");
        if (coins != null)
        {
            for (var i = 0; i < coins.transform.childCount; i++)
                PlayerPrefs.SetInt("Coin " + i + " collected", coins.transform.GetChild(i).gameObject.activeInHierarchy ? 1 : 0);
        }
        Debug.Log("Сохранено!");
    }

    public void Save_NextLevel()
    {
        PlayerPrefs.DeleteAll();
        var gameData = Canvas.GetComponent<GameData>();
        PlayerPrefs.SetInt("CoinsCount", gameData.CoinsCount);
        PlayerPrefs.SetInt("TipsCount", gameData.TipsCount);
        PlayerPrefs.SetInt("IsTransitToNextLevel", 1);
    }

    public void Load()
    {
        var gameData = Canvas.GetComponent<GameData>();
        var player = gameData.Player;
        player.transform.position = new Vector3(PlayerPrefs.GetFloat("PositionX"), PlayerPrefs.GetFloat("PositionY"), PlayerPrefs.GetFloat("PositionZ"));
        player.transform.eulerAngles = new Vector3(PlayerPrefs.GetFloat("RotationX"), PlayerPrefs.GetFloat("RotationY"), PlayerPrefs.GetFloat("RotationZ"));
        gameData.CoinsCount = PlayerPrefs.GetInt("CoinsCount");
        gameData.TipsCount = PlayerPrefs.GetInt("TipsCount");
        gameData.TaskItemsCount = PlayerPrefs.GetInt("TaskItemsCount");
        for (var i = 0; i < gameData.HasTasksCompleted.Count; i++)
        {
            gameData.HasTasksCompleted[i] = PlayerPrefs.GetInt("Task " + (i + 1) + " completed") == 1;
            var taskTriggers = gameData.Player.GetComponentInChildren<TriggersBehaviour>().TaskTriggers;
            if (gameData.HasTasksCompleted[i])
                taskTriggers.transform.GetChild(i).gameObject.SetActive(false);
        }
        var coins = GameObject.Find("Coins");
        if (coins != null)
        {
            for (var i = 0; i < coins.transform.childCount; i++)
                coins.transform.GetChild(i).gameObject.SetActive(PlayerPrefs.GetInt("Coin " + i + " collected") == 1);
        }
    }

    public void Load_NextLevel()
    {
        var gameData = Canvas.GetComponent<GameData>();
        gameData.CoinsCount = PlayerPrefs.GetInt("CoinsCount");
        gameData.TipsCount = PlayerPrefs.GetInt("TipsCount");
    }

    public void DeleteSavedDialogueData()
    {
        var sceneIndex = Canvas.GetComponent<GameData>().SceneIndex;
        var searchPattern = "DialogTrigger_" + sceneIndex + "_*";
        var dialogueDataFiles = Directory.GetFiles(Application.dataPath, searchPattern, SearchOption.AllDirectories);
        if (dialogueDataFiles.Length != 0)
        {
            foreach (var file in dialogueDataFiles)
                File.Delete(file);
            Debug.Log(string.Format("Файлы сохраненных диалогов ({0} шт.) удалены!", dialogueDataFiles.Length / 2));
        }
    }
}
