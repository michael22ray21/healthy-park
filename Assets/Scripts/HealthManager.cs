using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;

    [SerializeField] private GameObject[] hearts;
    [SerializeField] private GameObject noHeartsText;
    private int health = 3;
    private bool useHearts = false;


    public void Awake()
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecreaseHealth()
    {
        if (useHearts && health > 0) {
            hearts[--health].SetActive(false);
        }
    }
}
