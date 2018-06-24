using System.Collections;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SceneManagement;

public class GameSetup : MonoBehaviour {

    private void OnEnable()
    {
        GameEvents.UserLoginStatusUpdate += UserLoginStatusUpdateListener;
    }

    private void OnDisable()
    {
        GameEvents.UserLoginStatusUpdate -= UserLoginStatusUpdateListener;
    }

    // Use this for initialization
    void Awake () {

        Debug.Log("game setup initialized");

        Screen.orientation = ScreenOrientation.Portrait;

        //SceneManager.LoadScene(1);
    }

    public void UserLoginStatusUpdateListener(bool status)
    {
        Debug.Log("User login status changed");
        SceneManager.LoadScene(1);
    }
}
