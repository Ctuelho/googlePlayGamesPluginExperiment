using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class LevelHUD : MonoBehaviour {

    public static LevelHUD Instance = null;

    public Text scoreBoard;
    private int score = 0;

    public Text highestScoreBoard;
    private int highestScore = 50;

    // Use this for initialization
    void Start () {
        Instance = this;
        scoreBoard.text = "Score: 0";
        score = 0;
        UpdateHighestScore();
    }

    private void UpdateHighestScore()
    {
        if (Social.localUser != null)
        {
            PlayGamesPlatform.Instance.LoadScores(
                "CgkIp6qI8OkfEAIQBw",
                LeaderboardStart.PlayerCentered,
                1,
                LeaderboardCollection.Public,
                LeaderboardTimeSpan.AllTime,
                (LeaderboardScoreData data) => {
                    Debug.Log(data.Valid);
                    Debug.Log(data.Id);
                    Debug.Log(data.PlayerScore);
                    Debug.Log(data.PlayerScore.userID);
                    Debug.Log(data.PlayerScore.formattedValue);
                    if (data.PlayerScore.value > 0)
                    {
                        highestScore = (int)data.PlayerScore.value;
                        highestScoreBoard.text = "Highest Score: " + highestScore.ToString();
                    }
                });
        }
    }

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

    }

    public void AddScore (int value) {
        score += value;
        scoreBoard.text = "Score: " + score.ToString();
        if (score > highestScore)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI("Cfji293fjsie_QA");
            highestScore = score;
            highestScoreBoard.text = "Highest Score: " + highestScore.ToString();
        }
        ManageConquistas();
    }

    public void Retry()
    {
        score = 0;
        scoreBoard.text = "Score: " + score.ToString();
        //UpdateHighestScore();
    }

    public void ManageConquistas()
    {
        if(Social.localUser != null)
        {
            bool unlock = false;
            if (score >= 1000)
            {
                Social.ReportProgress("CgkIp6qI8OkfEAIQBg", 100.0f, (bool success) => {
                    // handle success or failure
                    if(success && !unlock)
                    {
                        unlock = true;
                    }
                });
            }
            if (score >= 500)
            {
                Social.ReportProgress("CgkIp6qI8OkfEAIQBQ", 100.0f, (bool success) => {
                    // handle success or failure
                    if (success && !unlock)
                    {
                        unlock = true;
                    }
                });
            }
            if (score >= 400)
            {
                Social.ReportProgress("CgkIp6qI8OkfEAIQBA", 100.0f, (bool success) => {
                    // handle success or failure
                    if (success && !unlock)
                    {
                        unlock = true;
                    }
                });
            }
            if (score >= 300)
            {
                Social.ReportProgress("CgkIp6qI8OkfEAIQAw", 100.0f, (bool success) => {
                    // handle success or failure
                    if (success && !unlock)
                    {
                        unlock = true;
                    }
                });
            }
            if (score >= 200)
            {
                Social.ReportProgress("CgkIp6qI8OkfEAIQAg", 100.0f, (bool success) => {
                    // handle success or failure
                    if (success && !unlock)
                    {
                        unlock = true;
                    }
                });
            }
            if (score >= 100)
            {
                Social.ReportProgress("CgkIp6qI8OkfEAIQAQ", 100.0f, (bool success) => {
                    // handle success or failure
                    if (success && !unlock)
                    {
                        unlock = true;
                    }
                });
            }

            Social.ReportScore(score, "CgkIp6qI8OkfEAIQBw", (bool success) => {
                if (success)
                {
                    Debug.Log("scorou");
                }
                else
                {
                    Debug.Log("não scorou");
                }
            });

            if (unlock)
            {
                Social.ShowAchievementsUI();
            }
        }
        
    }
}
