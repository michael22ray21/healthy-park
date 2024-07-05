using System.Collections;
using UnityEngine;

public class DropsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] dropsPrefab;
    [SerializeField] private float respawnTime = 1.5f;

    private Vector2 screenBounds;
    private float xPos;
    private readonly float padding = .9f;
    private readonly bool playing = true;

    // Start is called before the first frame update
    void Start()
    {
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
        GameObject selected = dropsPrefab[Random.Range(0, dropsPrefab.Length)];
        GameObject _ = Instantiate(selected, new Vector2(xPos, screenBounds.y * 1.1f), Quaternion.identity);
        var range = screenBounds.x * 0.5f;
        xPos += Random.Range(-range, range);
        xPos = Mathf.Clamp(xPos, -screenBounds.x * padding, screenBounds.x * padding);
        if (Random.Range(0, 2) == 1) {
            xPos *= -1;
        }
    }
}
