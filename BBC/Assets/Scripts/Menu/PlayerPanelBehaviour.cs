using System;
using System.Collections;
using System.Collections.Generic;
using Unisave.Facades;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanelBehaviour : MonoBehaviour
{
    public GameObject LoginAndRegistrationPanel;
    public GameObject ChooseNicknamePanel;
    public GameObject LoginForm;
    public GameObject RegistrationForm;
    public GameObject PlayerInfoPanel;
    public GameObject Leaderboard;
    public GameObject LeadboardLetterPrefab;
    public Text PlayerName;
    public Text PlayerScore;

    public void ShowLoginForm() => ShowForm(LoginForm);

    public void ShowRegistrationForm() => ShowForm(RegistrationForm);

    public void ReturnToLoginAndRegistrationPanel_LoginForm() => ReturnToLoginAndRegistrationPanel(LoginForm);

    public void ReturnToLoginAndRegistrationPanel_RegistrationForm() => ReturnToLoginAndRegistrationPanel(RegistrationForm);

    public void ShowPlayerInfo()
    {
        LoginForm.GetComponent<Animator>().Play("MoveForm_Right");
        PlayerInfoPanel.GetComponent<Animator>().Play("MovePlayerInfoPanel_Left");
        gameObject.GetComponent<MenuScript>().ChangeMainMenuButtonsAvailability(true);
    }

    public void ShowPlayerInfo_PostRegistration()
    {
        ChooseNicknamePanel.GetComponent<Animator>().Play("MoveChooseNicknamePanel_Right");
        PlayerInfoPanel.GetComponent<Animator>().Play("MovePlayerInfoPanel_Left");
        gameObject.GetComponent<MenuScript>().ChangeMainMenuButtonsAvailability(true);
    }

    public void ShowPlayerInfo_PlayerAlreadyLoggedIn(string nickname, int totalScore)
    {
        PlayerName.text = nickname;
        PlayerScore.text = totalScore.ToString();
        LoginAndRegistrationPanel.GetComponent<Animator>().Play("MoveLoginAndRegistrationPanel_Right");
        PlayerInfoPanel.GetComponent<Animator>().Play("MovePlayerInfoPanel_Left");
        gameObject.GetComponent<MenuScript>().ChangeMainMenuButtonsAvailability(true);
    }

    public IEnumerator SwitchForms()
    {
        foreach (var button in RegistrationForm.GetComponentsInChildren<Button>())
            button.interactable = false;
        yield return new WaitForSeconds(1f);
        ChooseNicknamePanel.GetComponent<Animator>().Play("MoveChooseNicknamePanel_Left");
        RegistrationForm.GetComponent<Animator>().Play("MoveForm_Right");
        yield return new WaitForSeconds(0.583f);
        foreach (var button in RegistrationForm.GetComponentsInChildren<Button>())
            button.interactable = true;
    }

    public void ReturnToLoginAndRegistrationPanel_PlayerInfoPanel()
    {
        LoginAndRegistrationPanel.GetComponent<Animator>().Play("MoveLoginAndRegistrationPanel_Left");
        PlayerInfoPanel.GetComponent<Animator>().Play("MovePlayerInfoPanel_Right");
    }

    public async void UpdateLeaderboard()
    {
        var leaderboardData = await OnFacet<PlayerDataFacet>.CallAsync<List<Tuple<string, int>>>(
            nameof(PlayerDataFacet.GetLeaderboardData));
        for (var i = 0; i < leaderboardData.Count; i++)
        {
            var newLetter = Instantiate(LeadboardLetterPrefab, Leaderboard.transform);
            newLetter.transform.GetChild(0).GetComponent<Text>().text = (i + 1).ToString();
            newLetter.transform.GetChild(1).GetComponent<Text>().text = leaderboardData[i].Item1;
            newLetter.transform.GetChild(2).GetComponent<Text>().text = leaderboardData[i].Item2.ToString();
        }
        Leaderboard.transform.GetChild(0).GetComponent<Image>().color = new Color(0.651f, 0.549f, 0f);
        Leaderboard.transform.GetChild(1).GetComponent<Image>().color = new Color(0.494f, 0.494f, 0.494f);
        Leaderboard.transform.GetChild(2).GetComponent<Image>().color = new Color(0.804f, 0.498f, 0.196f);

    }

    public void DeleteLeaderboard()
    {
        for (var i = 1; i < Leaderboard.transform.childCount; i++)
            Destroy(Leaderboard.transform.GetChild(i).gameObject);
    } 

    private void ShowForm(GameObject form)
    {
        foreach (var field in form.GetComponentsInChildren<InputField>())
            field.text = "";
        LoginAndRegistrationPanel.GetComponent<Animator>().Play("MoveLoginAndRegistrationPanel_Right");
        form.GetComponent<Animator>().Play("MoveForm_Left");
    }

    private void ReturnToLoginAndRegistrationPanel(GameObject form)
    {
        form.GetComponent<Animator>().Play("MoveForm_Right");
        LoginAndRegistrationPanel.GetComponent<Animator>().Play("MoveLoginAndRegistrationPanel_Left");       
    }   
}
