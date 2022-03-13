using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float startIddleTime = 5f;

    public TMPro.TMP_Text txtFruits;
    public GameObject messageUI;

    public AudioClip gameOverSfx;
    public AudioClip levelCompleteSfx;
    public AudioClip fuitCollectedSfx;

    private int totalFruits = 0;
    private int collectedFruits = 0;
    private AudioSource audioSrc;
    private MusicController musicController;

    private void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        musicController = FindObjectOfType<MusicController>();

        totalFruits = GameObject.FindGameObjectsWithTag("Fruit").Length;
        UpdateFruitsText();

        messageUI.SetActive(true);

        Time.timeScale = 0;
        StartCoroutine(StartScene());
    }

    protected IEnumerator StartScene()
    {
        yield return new WaitForSecondsRealtime(startIddleTime);
        Time.timeScale = 1;
        messageUI.SetActive(false);
    }

    public void OnFruitCollected()
    {
        collectedFruits++;
        UpdateFruitsText();
        audioSrc.PlayOneShot(fuitCollectedSfx);
    }

    public void OnPlayerDeath()
    {
        musicController.PauseMusic();
        audioSrc.PlayOneShot(gameOverSfx);

        messageUI.GetComponentInChildren<TMPro.TMP_Text>().SetText("OH SNAP! GAME OVER!");
        messageUI.SetActive(true);

        Time.timeScale = 0;
        StartCoroutine(NavigateToMainMenu());
    }

    public void OnHomeZoneEntered()
    {
        if (collectedFruits == totalFruits)
        {
            musicController.PauseMusic();
            audioSrc.PlayOneShot(levelCompleteSfx);

            messageUI.GetComponentInChildren<TMPro.TMP_Text>().SetText("WELL DONE! LEVEL COMPLETED!");
            messageUI.SetActive(true);

            Time.timeScale = 0;
            StartCoroutine(NavigateToMainMenu());
        }
        else
        {
            messageUI.GetComponentInChildren<TMPro.TMP_Text>().SetText("YOU NEED TO COLLECT ALL FRUITS!");
            messageUI.SetActive(true);

            Time.timeScale = 0;
            StartCoroutine(DismmissMessageUI());
        }
    }

    protected void UpdateFruitsText()
    {
        txtFruits.SetText($"{collectedFruits} / {totalFruits}");
    }

    protected IEnumerator NavigateToMainMenu()
    {
        yield return new WaitForSecondsRealtime(3);
        SceneManager.LoadScene("MenuScene");
        musicController.ResumeMusic();
    }

    protected IEnumerator DismmissMessageUI()
    {
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1;
        messageUI.SetActive(false);
    }
}