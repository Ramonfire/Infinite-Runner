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
    [SerializeField] PlayerController playerController;
    float Timeleft = 0;
    bool isGameOver = false;
    public bool IsGameOver{ get { return isGameOver; } }
    private void Awake()
    {
        GameOverCanvas.enabled = false;
        Time.timeScale = 1f;
        Timeleft = StartTime;
    }
    private void FixedUpdate()
    {
        if (isGameOver)
            return;


        Timeleft -= Time.deltaTime;

        if (TimeText != null)
            TimeText.SetText("Time Left: " + Timeleft.ToString("F1"));
        if (Timeleft <= 0) 
        {
            TimeText.SetText("Time Left: 0 " );
            GameOver();
        }

    }

    private void GameOver()
    {
        GameOverCanvas.enabled =true;
        Time.timeScale = 0.1f;
        playerController.enabled = false;
        isGameOver = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // reloading the level after 2 seconds
    }

    public void AddTime(int inTime)
    {
        Timeleft += inTime;
    }
}
