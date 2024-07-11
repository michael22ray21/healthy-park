using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Linq;
using Unity.VisualScripting;

public class EndScreenManager : MonoBehaviour
{
    [SerializeField] GameObject nameBoardUI;
    [SerializeField] GameObject leaderboardUI, entryArea, entryPrefab;
    SortedList<int, string> leaderboard;
    readonly int entryPrefabHeight = 50;
    readonly int maxEntries = 12;

    // Start is called before the first frame update
    void Start()
    {
        leaderboardUI.SetActive(false);
        nameBoardUI.SetActive(true);
        leaderboard = JsonUtility.FromJson<SortedList<int, string>>(PlayerPrefs.GetString("leaderboard"));
        leaderboard ??= new SortedList<int, string>();
        GenerateTestScores();
    }

    void GenerateTestScores()
    {
        leaderboard.Add(29, "G");
        leaderboard.Add(433, "AA");
        leaderboard.Add(43, "E");
        leaderboard.Add(12, "H");
        leaderboard.Add(32, "F");
        leaderboard.Add(453, "A");
        leaderboard.Add(100, "C");
        leaderboard.Add(333, "B");
        leaderboard.Add(500, "Winner");
        leaderboard.Add(10, "I");
        leaderboard.Add(353, "B");
        leaderboard.Add(55, "D");
    }

    public void SaveScore(string name)
    {
        leaderboard.Add(PlayerPrefs.GetInt("score"), name);
        nameBoardUI.SetActive(false);
        ShowLeaderboard();
    }

    public void ShowLeaderboard()
    {
        leaderboardUI.SetActive(true);
        int pos = 0;
        foreach (KeyValuePair<int, string> kvp in leaderboard.Reverse()) {
            GameObject entry = Instantiate(entryPrefab, entryArea.transform);
            entry.transform.GetChild(0).GetComponent<TMP_Text>().text = (pos + 1).ToString();
            entry.transform.GetChild(1).GetComponent<TMP_Text>().text = kvp.Value;
            entry.transform.GetChild(2).GetComponent<TMP_Text>().text = kvp.Key.ToString();
            RectTransform entryRect = entry.GetComponent<RectTransform>();
            entryRect.anchorMin = new Vector2(0.5f, 1);
            entryRect.anchorMax = new Vector2(0.5f, 1);
            entryRect.pivot = new Vector2(0.5f, 1);
            entryRect.anchoredPosition = new Vector2(0, -entryPrefabHeight * pos);
            entry.SetActive(true);
            pos++;
            if (pos == maxEntries) break;
        }
        // for (int i = 0; i < leaderboard.Count; i++) {
        //     GameObject entry = Instantiate(entryPrefab, entryArea.transform);
        //     entry.transform.GetChild(0).GetComponent<TMP_Text>().text = (i + 1).ToString();
        //     entry.transform.GetChild(1).GetComponent<TMP_Text>().text = leaderboard.Values[i];
        //     entry.transform.GetChild(2).GetComponent<TMP_Text>().text = leaderboard.Keys[i].ToString();
        //     RectTransform entryRect = entry.GetComponent<RectTransform>();
        //     entryRect.anchorMin = new Vector2(0.5f, 1);
        //     entryRect.anchorMax = new Vector2(0.5f, 1);
        //     entryRect.pivot = new Vector2(0.5f, 1);
        //     entryRect.anchoredPosition = new Vector2(0, -entryPrefabHeight * i);
        //     entry.SetActive(true);
        // }
    }
}
