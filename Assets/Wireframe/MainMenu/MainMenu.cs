using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;

public class MainMenu : MonoBehaviour {

    public static MainMenu Instance = null;

    bool hasUser = false;

    public Button playButton;
    public Button signinButton;
    public Button signoutButton;
    public Button exitButton;

    public Text welcomeText;

    private void OnEnable()
    {
        GameEvents.UserLoginStatusUpdate += Refresh;
    }

    private void OnDisable()
    {
        GameEvents.UserLoginStatusUpdate += Refresh;
    }

    public void Refresh(bool status)
    {
        Instance.hasUser = status;

        Instance.UpdateUI();
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            Instance.hasUser = UserAndLoginManager.Instance.HasUser;

            Instance.UpdateUI();

            //SignIn();

            //welcomeText.text = "";
            //playButton.interactable = false;
            //signinButton.interactable = true;
            //signoutButton.interactable = false;
        }
    }

	void UpdateUI (string text = null) {
        Debug.Log("has user " + Instance.hasUser);
        if (hasUser)
        {
            if (text == null) welcomeText.text = Social.localUser.userName;
            else welcomeText.text = text;
            playButton.interactable = true;
            signinButton.interactable = false;
            signoutButton.interactable = true;
        }
        else
        {
            if (text == null) welcomeText.text = "Please, sign in to be able to register your score.";
            else welcomeText.text = text;
            playButton.interactable = false;
            signinButton.interactable = true;
            signoutButton.interactable = false;
        }
	}
	
	public void Play()
    {
        Debug.Log("play button hit");
        playButton.interactable = false;
        signinButton.interactable = false;
        signoutButton.interactable = false;
        SceneManager.LoadScene(2);
    }

    public void Exit()
    {
        Debug.Log("exit button hit");
        playButton.interactable = false;
        signinButton.interactable = false;
        signoutButton.interactable = false;
        Application.Quit();
    }

    public void SignIn()
    {
        Debug.Log("sign in button hit");
        playButton.interactable = false;
        signinButton.interactable = false;
        signoutButton.interactable = false;
        UserAndLoginManager.Instance.AuthUser();
    }

    public void SignOut()
    {
        Debug.Log("sign out button hit");
        playButton.interactable = false;
        signinButton.interactable = false;
        signoutButton.interactable = false;
        UserAndLoginManager.Instance.SignOut();
    }
}
