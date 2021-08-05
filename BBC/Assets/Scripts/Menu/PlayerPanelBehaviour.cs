using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanelBehaviour : MonoBehaviour
{
    public GameObject LoginAndRegistrationPanel;
    public GameObject ChooseNicknamePanel;
    public GameObject LoginForm;
    public GameObject RegistrationForm;
    public GameObject PlayerInfoPanel;
    public Text PlayerName;
    public Text PlayerScore;

    public void ShowLoginForm() => ShowForm(LoginForm);

    public void ShowRegistrationForm() => ShowForm(RegistrationForm);

    public void ShowPlayerInfo()
    {
        LoginForm.GetComponent<Animator>().Play("MoveForm_Right");
        PlayerInfoPanel.GetComponent<Animator>().Play("MovePlayerInfoPanel_Left");
    }

    public void ShowPlayerInfo_PostRegistration()
    {
        ChooseNicknamePanel.GetComponent<Animator>().Play("MoveChooseNicknamePanel_Right");
        PlayerInfoPanel.GetComponent<Animator>().Play("MovePlayerInfoPanel_Left");
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

    public void ReturnToLoginAndRegistrationPanel_LoginForm() => ReturnToLoginAndRegistrationPanel(LoginForm);

    public void ReturnToLoginAndRegistrationPanel_RegistrationForm() => ReturnToLoginAndRegistrationPanel(RegistrationForm);

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
