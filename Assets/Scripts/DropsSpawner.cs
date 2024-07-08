using UnityEngine;
using System.Collections;

public class DropsSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] dropsPrefab;
    [SerializeField] float respawnTime = 1.5f;
    Vector2 screenBounds;
    float xPos;
    readonly float padding = .85f;
    bool useNegItems = false;
    bool useHearts = false;
    int baseChance = 1;
    int currentChance;
    int mirrorChance = 4;
    float rangeDistance = .5f;

    // Start is called before the first frame update
    void Start()
    {
        useHearts = PlayerPrefs.GetInt("use_hearts") == 1;
        useNegItems = PlayerPrefs.GetInt("complexity") == 1;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        xPos = screenBounds.x / 2;
        StartCoroutine(StartDrops());
    }

    IEnumerator StartDrops()
    {
        while (GameManager.instance.IsPlaying()) {
            yield return new WaitForSeconds(respawnTime);
            SpawnDrops();
        }
    }

    void SpawnDrops()
    {
        if (useHearts && respawnTime > .8f) {
            if (Random.Range(0, 10) < currentChance) {
                rangeDistance -= .1f;
                mirrorChance -= 1;
                respawnTime -= .2f;
                baseChance++;
                currentChance = baseChance;
            } else {
                currentChance++;
            }
        }
        GameObject selected = dropsPrefab[Random.Range(0, 4) + (useNegItems ? Random.Range(0, 5) : 0)];
        GameObject _ = Instantiate(selected, new Vector2(xPos, screenBounds.y * 1.1f), Quaternion.identity);
        var range = screenBounds.x * rangeDistance;
        xPos += Random.Range(-range, range);
        xPos = Mathf.Clamp(xPos, -screenBounds.x * padding, screenBounds.x * padding);
        if (Random.Range(0, 10) < mirrorChance) {
            xPos *= -1;
        }
    }
}
