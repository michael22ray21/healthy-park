using UnityEngine;
using System.Collections.Generic;

public class EndScreenManager : MonoBehaviour
{
    [SerializeField] GameObject leaderboardUI, nameBoardUI;

    // Start is called before the first frame update
    void Start()
    {
        leaderboardUI.SetActive(false);
        nameBoardUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SaveScore(string name)
    {
        SortedList<int, string> leaderboard = JsonUtility.FromJson<SortedList<int, string>>(PlayerPrefs.GetString("leaderboard"));
        leaderboard.Add(PlayerPrefs.GetInt("score"), name);
        leaderboard.Capacity = 15;
    }
}
