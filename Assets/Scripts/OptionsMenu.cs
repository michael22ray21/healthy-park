using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] TMP_Text timeText, timeOption, complexityDescription;
    [SerializeField] Button increaseTimer, decreaseTimer;
    [SerializeField] TMP_Dropdown complexityDropdown;
    int timeInSeconds = 120;
    bool useHearts = false;
    int complexity = 0;


    void Start()
    {
        UpdateTimeText();
        UpdateComplexity(complexityDropdown.value);
    }

    void UpdateTimeText()
    {
        timeText.text = $"{timeInSeconds/60:D2}:{timeInSeconds%60:D2}";
    }

    public void IncreaseTimeText()
    {
        if (timeInSeconds < 180) {
            timeInSeconds += 5;
            UpdateTimeText();
        }
        if (timeInSeconds == 180) {
            increaseTimer.enabled = false;
        }
        if (timeInSeconds > 15) {
            decreaseTimer.enabled = true;
        }
    }

    public void DecreaseTimeText()
    {
        if (timeInSeconds > 15) {
            timeInSeconds -= 5;
            UpdateTimeText();
        }
        if (timeInSeconds < 180) {
            increaseTimer.enabled = true;
        }
        if (timeInSeconds == 15) {
            decreaseTimer.enabled = false;
        }
    }

    public void TimerOrHearts(bool val)
    {
        useHearts = val;
        if (val) {
            timeText.fontStyle = FontStyles.Strikethrough;
            timeOption.fontStyle = FontStyles.Strikethrough;
            increaseTimer.enabled = false;
            decreaseTimer.enabled = false;
        } else {
            timeText.fontStyle = FontStyles.Normal;
            timeOption.fontStyle = FontStyles.Normal;
            decreaseTimer.enabled = true;
            increaseTimer.enabled = true;
        }
    }

    public void UpdateComplexity(int option)
    {
        complexity = option;
        complexityDescription.text = option switch {
            0 => "Basic\nThis mode only uses the positive items.",
            1 => "Advanced\nThis mode uses the full range of drops, including the negative ones.",
            _ => "a description of the selected complexity level.",
        };
    }

    public void SavePreferences()
    {
        PlayerPrefs.SetInt("timer_time", timeInSeconds);
        PlayerPrefs.SetInt("use_hearts", useHearts ? 1 : 0);
        PlayerPrefs.SetInt("complexity", complexity);
        Debug.Log($"time: {timeInSeconds}, hearts? {useHearts}, complexity: {complexity}");
    }
}
