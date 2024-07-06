using System;
using NuitrackSDK.Tutorials.ZombieVR;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private TMP_Text scoreText, timerText;
    [SerializeField] private GameObject timerUI;
    private bool useHearts = false;
    private float timeInSeconds;
    private int score = 0;

    public void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        useHearts = PlayerPrefs.GetInt("use_hearts") == 1;
        if (!useHearts) {
            timeInSeconds = PlayerPrefs.GetInt("timer_time");
        } else {
            timerUI.SetActive(false);
        }
        int minutes = Mathf.FloorToInt(timeInSeconds/60);
        int seconds = Mathf.FloorToInt(timeInSeconds%60);
        timerText.text = $"{minutes:D2}:{seconds:D2}";
        scoreText.text = $"Score: {score}";
    }

    void Update()
    {
        if (!useHearts && timeInSeconds > 0) UpdateTimer();
    }

    private void UpdateTimer()
    {
        timeInSeconds -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(timeInSeconds/60);
        int seconds = Mathf.FloorToInt(timeInSeconds%60);
        timerText.text = $"{minutes:D2}:{seconds:D2}";
    }

    public void AddPoints(int points)
    {
        score += points;
        scoreText.text = $"Score: {score}";
    }
}
