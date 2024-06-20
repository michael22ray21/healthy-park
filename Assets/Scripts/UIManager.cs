using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TMP_Text scoreText;

    int score = 0;

    public void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = $"Score: {score}";
    }

    public void AddPoints(int points)
    {
        score += points;
        scoreText.text = $"Score: {score}";
    }
}
