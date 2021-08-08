using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unisave.Facades;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unisave.Examples.PlayerAuthentication.Backend;

public class MenuScript : MonoBehaviour
{
    [Header ("Для анимаций и переходов")]
    public GameObject MenuCamera;
    public GameObject Robot;
    public GameObject FireLight;
    public GameObject BlackScreen;
    public GameObject MainMenuButtons;
    public Button ContinueButton;
    [Header("Авторизован ли пользователь")]
    [HideInInspector]
    public bool IsPlayerLoggedIn;

    private GameObject loginAndRegistrationPanel;
    private GameObject playerInfoPanel;
    private LoadLevel levelLoader;
    private GlobalMapBehaviour mapBehaviour;

    #region Переходы между экранами меню

    public void GoTo_Settings() => StartCoroutine(GoTo_Settings_COR());

    public void GoTo_Stats() => StartCoroutine(GoTo_Stats_COR());

    public void GoTo_Levels() => StartCoroutine(GoTo_Levels_COR());

    public void ReturnToMainMenuFrom_Settings() => StartCoroutine(ReturnToMainMenu_Settings_COR());

    public void ReturnToMainMenuFrom_Stats() => StartCoroutine(ReturnToMainMenu_Stats_COR());

    public void ReturnToMainMenuFrom_Levels() => StartCoroutine(ReturnToMainMenu_Levels_COR());

    public void Exit() => Application.Quit();

    #endregion

    #region Запуски уровней

    public void Continue() => StartCoroutine(Continue_COR());

    public void Start_Level_Training() => StartCoroutine(Start_Level_COR(SceneManager.sceneCountInBuildSettings - 1));

    public void Start_Level_1() => StartCoroutine(Start_Level_COR(1));

    public void Start_Level_2() => StartCoroutine(Start_Level_COR(2));

    public void Start_Level_3() => StartCoroutine(Start_Level_COR(3));

    public void Start_Level_4() => StartCoroutine(Start_Level_COR(4));

    public void Start_Level_5() => StartCoroutine(Start_Level_COR(5));

    #endregion

    public void ChangeMainMenuButtonsAvailability(bool areAvailable)
    {
        var textColor = areAvailable ? Color.white : Color.gray;
        for (var i = 0; i < MainMenuButtons.transform.childCount - 1; i++)
        {
            var button = MainMenuButtons.transform.GetChild(i).GetComponent<Button>();          
            button.interactable = areAvailable;
            button.transform.GetChild(0).GetComponent<Text>().color = textColor;
        }
    }

    private IEnumerator GoTo_Settings_COR()
    {
        yield return StartCoroutine(ChangeMenuChapter_COR(1));
        for (var i = 1; i <= 2; i++)
        {
            GameObject.Find("SettingsBackground_Part_" + i).GetComponent<Animator>().Play("DrawChapter");
            yield return new WaitForSeconds(0.15f);
        }
        GameObject.Find("SettingsBackground_BackToMenu").GetComponent<Animator>().Play("DrawChapter");
        GameObject.Find("SettingsButtons").GetComponent<Animator>().Play("MoveSettingsDown");
        GameObject.Find("BackToMainMenuButton_Settings").GetComponent<Animator>().Play("MoveBackToMenuButtonUp");
    }

    private IEnumerator GoTo_Stats_COR()
    {
        yield return StartCoroutine(ChangeMenuChapter_COR(2));
        gameObject.GetComponent<PlayerPanelBehaviour>().UpdateLeaderboard();
        GameObject.Find("StatsPanel").GetComponent<Animator>().Play("MoveStatsDown");
        GameObject.Find("BackToMainMenuButton_Stats").GetComponent<Animator>().Play("MoveBackToMenuButtonUp");
    }

    private IEnumerator GoTo_Levels_COR()
    {
        yield return StartCoroutine(ChangeMenuChapter_COR(3));
        StartCoroutine(mapBehaviour.ShowGlobalMap_COR());
    }

    private IEnumerator ReturnToMainMenu_Settings_COR()
    {
        GameObject.Find("SettingsBackground_BackToMenu").GetComponent<Animator>().Play("EraseChapter");
        GameObject.Find("SettingsButtons").GetComponent<Animator>().Play("MoveSettingsUp");
        GameObject.Find("BackToMainMenuButton_Settings").GetComponent<Animator>().Play("MoveBackToMenuButtonDown");
        yield return new WaitForSeconds(0.5f);
        for (var i = 2; i >= 1; i--)
        {
            GameObject.Find("SettingsBackground_Part_" + i).GetComponent<Animator>().Play("EraseChapter");
            yield return new WaitForSeconds(0.15f);
        }
        yield return StartCoroutine(ReturnToMainMenu_COR(1));
    }

    private IEnumerator ReturnToMainMenu_Stats_COR()
    {
        GameObject.Find("StatsPanel").GetComponent<Animator>().Play("MoveStatsUp");
        GameObject.Find("BackToMainMenuButton_Stats").GetComponent<Animator>().Play("MoveBackToMenuButtonDown");
        yield return new WaitForSeconds(0.583f);
        gameObject.GetComponent<PlayerPanelBehaviour>().DeleteLeaderboard();
        yield return StartCoroutine(ReturnToMainMenu_COR(2));
    }

    private IEnumerator ReturnToMainMenu_Levels_COR()
    {
        yield return StartCoroutine(mapBehaviour.HideGlobalMap_COR());
        yield return StartCoroutine(ReturnToMainMenu_COR(3));
    }

    private IEnumerator ReturnToMainMenu_COR(int cameraNumber)
    {
        MenuCamera.GetComponent<Animator>().Play("MoveBackCamera_" + cameraNumber);
        yield return new WaitForSeconds(2f);
        for (var i = 1; i <= 5; i++)
        {
            GameObject.Find("MainMenuBackground_Part_" + i).GetComponent<Animator>().Play("DrawMainMenu");
            yield return new WaitForSeconds(0.15f);
        }
        GameObject.Find("Content").GetComponent<Animator>().Play("MoveMainMenuDown");
        if (IsPlayerLoggedIn)
            playerInfoPanel.GetComponent<Animator>().Play("MovePlayerInfoPanel_Left");
        else loginAndRegistrationPanel.GetComponent<Animator>().Play("MoveLoginAndRegistrationPanel_Left");
    }

    private IEnumerator ChangeMenuChapter_COR(int cameraNumber)
    {
        GameObject.Find("Content").GetComponent<Animator>().Play("MoveMainMenuUp");
        if (IsPlayerLoggedIn)
            playerInfoPanel.GetComponent<Animator>().Play("MovePlayerInfoPanel_Right");
        else loginAndRegistrationPanel.GetComponent<Animator>().Play("MoveLoginAndRegistrationPanel_Right");
        yield return new WaitForSeconds(0.5f);
        for (var i = 5; i >= 1; i--)
        {
            GameObject.Find("MainMenuBackground_Part_" + i).GetComponent<Animator>().Play("EraseMainMenu");
            yield return new WaitForSeconds(0.15f);
        }
        MenuCamera.GetComponent<Animator>().Play("MoveCamera_" + cameraNumber);
        yield return new WaitForSeconds(2f);
    }

    private IEnumerator Continue_COR()
    {
        GameObject.Find("Content").GetComponent<Animator>().Play("MoveMainMenuUp");
        yield return new WaitForSeconds(0.5f);
        for (var i = 5; i >= 1; i--)
        {
            GameObject.Find("MainMenuBackground_Part_" + i).GetComponent<Animator>().Play("EraseMainMenu");
            yield return new WaitForSeconds(0.15f);
        }
        Robot.GetComponent<Animator>().Play("Walk_MainMenu");
        yield return new WaitForSeconds(5f);
        BlackScreen.GetComponent<Animator>().Play("AppearBlackScreen");
        yield return new WaitForSeconds(1.4f);
        StartCoroutine(levelLoader.LoadLevelAsync_COR(PlayerPrefs.GetInt("SceneIndex")));
    }

    private IEnumerator Start_Level_COR(int levelNumber)
    {
        PlayerPrefs.DeleteAll();
        yield return StartCoroutine(mapBehaviour.HideGlobalMap_COR());
        MenuCamera.GetComponent<Animator>().Play("MoveBackCamera_3");
        yield return new WaitForSeconds(2f);
        Robot.GetComponent<Animator>().Play("Walk_MainMenu");
        yield return new WaitForSeconds(5f);
        BlackScreen.GetComponent<Animator>().Play("AppearBlackScreen");
        yield return new WaitForSeconds(1.4f);
        StartCoroutine(levelLoader.LoadLevelAsync_COR(levelNumber));
    }

    private async void Awake()
    {
        var response = await OnFacet<PlayerDataFacet>.CallAsync<PlayerEntity>(
            nameof(PlayerDataFacet.IsPlayerAuthorized));
        IsPlayerLoggedIn = response != null;
        if (IsPlayerLoggedIn)
            gameObject.GetComponent<PlayerPanelBehaviour>().ShowPlayerInfo_PlayerAlreadyLoggedIn(response.nickname, response.totalScore);
        else ChangeMainMenuButtonsAvailability(false);      
    }
    
    private void Start()
    {
        mapBehaviour = gameObject.GetComponent<GlobalMapBehaviour>();
        levelLoader = gameObject.GetComponent<LoadLevel>();
        loginAndRegistrationPanel = gameObject.GetComponent<PlayerPanelBehaviour>().LoginAndRegistrationPanel;
        playerInfoPanel = gameObject.GetComponent<PlayerPanelBehaviour>().PlayerInfoPanel;
        FireLight.GetComponent<Animator>().Play("Fire");
        if (!PlayerPrefs.HasKey("PositionX"))
            ContinueButton.gameObject.SetActive(false);
    }
}
