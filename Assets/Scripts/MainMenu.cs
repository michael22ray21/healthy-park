using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject whoosh;
    [SerializeField] private GameObject optionsMenu;

    void Start()
    {
        whoosh.SetActive(false);
        optionsMenu.SetActive(false);
    }

    public void PlayGame()
    {
        Animator whooshAnimator = whoosh.GetComponent<Animator>();
        whooshAnimator.StartPlayback();
        whoosh.SetActive(true);
        StartCoroutine(WaitThenPlayNextScene());
    }

    IEnumerator WaitThenPlayNextScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
