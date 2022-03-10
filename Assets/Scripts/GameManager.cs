using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float startIddleTime = 5f;

    public TMPro.TMP_Text txtFruits;
    public GameObject messageUI;

    private int totalFruits = 0;
    private int collectedFruits = 0;

    private void Start()
    {
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
    }

    public void OnPlayerDeath()
    {
        messageUI.GetComponentInChildren<TMPro.TMP_Text>().SetText("Oh snap! Game Over!");
        messageUI.SetActive(true);

        Time.timeScale = 0;
        StartCoroutine(NavigateToMainMenu());
    }

    public void OnHomeZoneEntered()
    {
        if (collectedFruits == totalFruits)
        {
            messageUI.GetComponentInChildren<TMPro.TMP_Text>().SetText("Well done! Welcome home!");
            messageUI.SetActive(true);

            Time.timeScale = 0;
            StartCoroutine(NavigateToMainMenu());
        }
        else
        {
            messageUI.GetComponentInChildren<TMPro.TMP_Text>().SetText("You need to collect all fruits!");
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
        Debug.Log("navigate to menu...");
    }

    protected IEnumerator DismmissMessageUI()
    {
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1;
        messageUI.SetActive(false);
    }
}