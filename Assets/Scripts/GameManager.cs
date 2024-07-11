using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] TMP_Text scoreText, timerText, noHeartsText;
    [SerializeField] GameObject timerUI, gameOverUI, newHighscoreUI;
    bool useHearts = false;
    float timeInSeconds;
    int score = 0;

    public bool playing = false;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameOverUI.SetActive(false);
        newHighscoreUI.SetActive(true);
        useHearts = PlayerPrefs.GetInt("use_hearts") == 1;
        if (!useHearts) {
            timeInSeconds = PlayerPrefs.GetInt("timer_time");
            int minutes = Mathf.FloorToInt(timeInSeconds/60);
            int seconds = Mathf.FloorToInt(timeInSeconds%60);
            timerText.text = $"{minutes:D2}:{seconds:D2}";
        } else {
            timerUI.SetActive(false);
        }
        scoreText.text = $"Score: {score}";
        playing = true;
        StartCoroutine(DropsSpawner.instance.StartDrops());
    }

    void Update()
    {
        if (!useHearts && timeInSeconds > 0) UpdateTimer();
    }

    void UpdateTimer()
    {
        timeInSeconds -= Time.deltaTime;
        if (timeInSeconds <= 0) EndGame();
        int minutes = Mathf.FloorToInt(timeInSeconds/60);
        int seconds = Mathf.FloorToInt(timeInSeconds%60);
        timerText.text = $"{minutes:D2}:{seconds:D2}";
    }

    public void AddPoints(int points)
    {
        score += points;
        scoreText.text = $"Score: {score}";
    }

    public void EndGame()
    {
        if (playing) {
            playing = false;
            scoreText.alpha = 100;
            noHeartsText.alpha = 100;
            gameOverUI.SetActive(true);
            PlayerPrefs.SetInt("score", score);

            //* if highscore is reached then record the highscore and do the animation
            if (score > PlayerPrefs.GetInt("highscore")) {
                PlayerPrefs.SetInt("highscore", score);
                newHighscoreUI.SetActive(true);
                newHighscoreUI.GetComponentInChildren<Animator>().StartPlayback();
            }

            StartCoroutine(WaitThenPlayNextScene());
        }
    }

    IEnumerator WaitThenPlayNextScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
