using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeTracker : MonoBehaviour
{
    [SerializeField] float StartTime = 30f;
    [SerializeField] TMP_Text TimeText;

    [SerializeField] TMP_Text GameOverText;
    [SerializeField] Canvas GameOverCanvas;

    [SerializeField] TMP_Text BestScore;
    [SerializeField] TMP_Text BestTime;

    [SerializeField] PlayerController playerController;

    [SerializeField] SaveManager saveManager;
    [SerializeField] ScoreManager scoreManager;

    float Timeleft = 0;
    float TimeSurvived = 0;
    bool isGameOver = false;
    public bool IsGameOver { get { return isGameOver; } }



    float bestTime=0;
    int bestScore=0;
    bool didScoreChange = false;







    private void Awake()
    {
        GameOverCanvas.enabled = false;
        Time.timeScale = 1f;
        Timeleft = StartTime;
        saveManager = FindFirstObjectByType<SaveManager>();
        saveManager.Load(out bestScore,out bestTime);
        scoreManager = GetComponent<ScoreManager>();
    }
    private void FixedUpdate()
    {
        if (isGameOver)
            return;

        CheckScoreChange();
        ManageTime();

    }

    private void CheckScoreChange()
    {
        if (didScoreChange)
        {
            saveManager.Load(out bestScore, out bestTime);
            didScoreChange = false;
        }
    }

    private void ManageTime()
    {
        Timeleft -= Time.deltaTime;
        TimeSurvived += Time.deltaTime;
        if (TimeText != null)
            TimeText.SetText("Time Left: " + Timeleft.ToString("F1"));
        if (Timeleft <= 0)
        {
            TimeText.SetText("Time Left: 0 ");
            GameOver();
        }

        if (Timeleft <= 10)
            TimeText.color = Color.red;
        else
            TimeText.color = Color.black;
    }

    private void GameOver()
    {
        GameOverCanvas.enabled = true;
        Time.timeScale = 0.1f;
        playerController.enabled = false;
        isGameOver = true;

        CheckNewScore();

    }

    private void CheckNewScore()
    {
        float newBestTime = bestTime;
        int newBestScore = bestScore;

        if (bestTime < TimeSurvived) newBestTime = TimeSurvived;
        if (bestScore < scoreManager.CurrentScore) newBestScore = scoreManager.CurrentScore;

        if(newBestTime!=bestTime || newBestScore != bestScore) 
        {
            saveManager.Save(newBestScore, newBestTime);
            didScoreChange = true;

        }

        BestScore.SetText("best Score: " + newBestScore);
        BestTime.SetText("bestTime: " + newBestTime.ToString("F2") + "seconds");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // reloading the level after 2 seconds
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void AddTime(int inTime)
    {
        Timeleft += inTime;
    }
}
