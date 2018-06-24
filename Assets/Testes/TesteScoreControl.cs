using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TesteScoreControl : MonoBehaviour {

    public Text highScore;
    public Text currentScore;

    private long score = 0;

	// Use this for initialization
	void Start () {
        currentScore.text = score.ToString();
        RefreshHighscore();
	}

    public void RefreshHighscore()
    {
        highScore.text = "Highscore " + DBManager.Instance.topScore.ToString();
    }

    void OnEnable()
    {
        DBManager.TopScoreUpdated += RefreshHighscore;

    }

    void OnDisble()
    {
        DBManager.TopScoreUpdated -= RefreshHighscore;

    }

    public void AddOneScore()
    {
        score++;
        RefreshCurrentScore();
        DBManager.Instance.LogScore(score);
    }

    public void AddTenScore()
    {
        score+=10;
        RefreshCurrentScore();
        DBManager.Instance.LogScore(score);
    }

    private void RefreshCurrentScore()
    {
        currentScore.text =  "Score " + score.ToString();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
