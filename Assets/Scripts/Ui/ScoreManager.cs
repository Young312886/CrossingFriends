using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance = null;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI coinText;
    [SerializeField]
    private TextMeshProUGUI timeText;
    [SerializeField]
    private TextMeshProUGUI gameStatText;
    [SerializeField]
    private GameObject gameStatPanel;
    [SerializeField]
    private GameObject scorePanel;
    [SerializeField]
    private GameObject coinPanel;
    private int coin = 0;
    private int score = 0;
    private int gameTime = 0;
    public bool gamePause = false;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        StartTimeCalcRoutine();
    }
    void StartTimeCalcRoutine()
    {
        StartCoroutine("StartTimeCalc");
    }
    public void StopTimeCalcRoutine()
    {
        StopCoroutine("StartTimeCalc");
    }
    public void IncreaseCoin()
    {
        coin += 1;
        coinText.SetText(coin.ToString());

    }
    public void IncreaseScore()
    {
        score += 1;
        scoreText.SetText(score.ToString());

    }

    public void SetGamePause()
    {
        gamePause = true;
        StopTimeCalcRoutine();
        gameStatPanel.SetActive(true);
        scorePanel.SetActive(false);
        coinPanel.SetActive(false);
        gameStatText.SetText("Pause");

    }
    public void SetGameResume()
    {
        gamePause = false;
        StartTimeCalcRoutine();
        gameStatPanel.SetActive(false);
        scorePanel.SetActive(true);
        coinPanel.SetActive(true);
    }
    public void SetGameOver()
    {
        gamePause = false;
        StartTimeCalcRoutine();
        gameStatPanel.SetActive(false);
        scorePanel.SetActive(true);
        coinPanel.SetActive(true);
        gameStatText.SetText("GameOver");
    }
    IEnumerator StartTimeCalc()
    {
        while (true)
        {
            int timeHour = 0;
            int timeMin = 0;

            gameTime += 1;
            timeHour = gameTime / 60;
            timeMin = gameTime % 60;

            string timeFormat = string.Format("{0:D2}:{1:D2}", timeHour, timeMin);
            timeText.SetText(timeFormat.ToString());
            yield return new WaitForSeconds(1f);
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("UiScene");
    }

}