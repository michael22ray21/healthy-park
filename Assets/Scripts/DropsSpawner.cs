using UnityEngine;
using System.Collections;
using NuitrackSDK.Tutorials.ZombieVR;

public class DropsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] dropsPrefab;
    [SerializeField] private float respawnTime = 1.5f;
    private Vector2 screenBounds;
    private float xPos;
    private readonly float padding = .85f;
    private bool useNegItems = false;
    private bool useHearts = false;
    private int baseChance = 1;
    private int currentChance;

    public bool playing = true;

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
        while (playing)
        {
            yield return new WaitForSeconds(respawnTime);
            SpawnDrops();
        }
    }

    private void SpawnDrops()
    {
        if (useHearts && respawnTime > .8f) {
            if (Random.Range(0, 10) < currentChance) {
                respawnTime -= .2f;
                baseChance++;
                currentChance = baseChance;
            } else {
                currentChance++;
            }
        }
        GameObject selected = dropsPrefab[Random.Range(0, useNegItems ? dropsPrefab.Length : 4)];
        GameObject _ = Instantiate(selected, new Vector2(xPos, screenBounds.y * 1.1f), Quaternion.identity);
        var range = screenBounds.x * 0.5f;
        xPos += Random.Range(-range, range);
        xPos = Mathf.Clamp(xPos, -screenBounds.x * padding, screenBounds.x * padding);
        if (Random.Range(0, 5) == 1) {
            xPos *= -1;
        }
    }
}
