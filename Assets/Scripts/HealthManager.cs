using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;

    [SerializeField] GameObject[] hearts;
    [SerializeField] GameObject noHeartsText;
    int health = 3;
    bool useHearts = false;


    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        useHearts = PlayerPrefs.GetInt("use_hearts") == 1;
        if (useHearts) {
            noHeartsText.SetActive(false);
            for (int i = 0; i < health; i++) hearts[i].SetActive(true);
        } else {
            noHeartsText.SetActive(true);
            for (int i = 0; i < health; i++) hearts[i].SetActive(false);
        }
    }

    public void DecreaseHealth()
    {
        if (health == 0) {
            GameManager.instance.EndGame();
        } else if (useHearts) {
            hearts[--health].SetActive(false);
        }
    }
}
