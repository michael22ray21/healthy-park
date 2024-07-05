using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText, timeOption, complexityDescription;
    [SerializeField] private Button increaseTimer, decreaseTimer;
    [SerializeField] private TMP_Dropdown complexityDropdown;
    private int timeInSeconds = 120;


    void Start()
    {
        UpdateTimeText();
        UpdateComplexity(complexityDropdown.value);
    }

    public void UpdateTimeText()
    {
        PlayerPrefs.SetInt("timer_time", timeInSeconds);
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
        if (val) {
            PlayerPrefs.SetInt("use_hearts", 1);
            timeText.fontStyle = FontStyles.Strikethrough;
            timeOption.fontStyle = FontStyles.Strikethrough;
            increaseTimer.enabled = false;
            decreaseTimer.enabled = false;
        } else {
            PlayerPrefs.SetInt("use_hearts", 0);
            timeText.fontStyle = FontStyles.Normal;
            timeOption.fontStyle = FontStyles.Normal;
            decreaseTimer.enabled = true;
            increaseTimer.enabled = true;
        }
    }

    public void UpdateComplexity(int option)
    {
        PlayerPrefs.SetInt("complexity", option);
        complexityDescription.text = option switch {
            0 => "Basic\nThis mode only uses the positive items.",
            1 => "Advanced\nThis mode uses the full range of drops, including the negative ones.",
            _ => "a description of the selected complexity level.",
        };
    }
}
