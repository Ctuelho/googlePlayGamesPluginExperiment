using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUI : MonoBehaviour {

    public Transform menu;

    void OnEnable()
    {
        LevelGenerator.Retry += Retry;
        LevelGenerator.EndLevel += EndLevel;
    }

    void OnDisable()
    {
        LevelGenerator.Retry -= Retry;
        LevelGenerator.EndLevel -= EndLevel;
    }

    public void EndLevel()
    {
        ShowMenu();
    }

    public void Retry()
    {
        HideMenu();
    }

    public void ShowMenu()
    {
        menu.gameObject.SetActive(true);
    }

    public void HideMenu()
    {
        menu.gameObject.SetActive(false);
    }

    public void PlayAgain()
    {
        LevelGenerator.RetryLevelFirer();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(1);
    }
}
