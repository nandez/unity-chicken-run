using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float startIddleTime = 5f;

    private int totalFruits = 0;
    private int fruitCollected = 0;

    void Start()
    {
        totalFruits = GameObject.FindGameObjectsWithTag("Fruit").Length;

        Time.timeScale = 0;
        StartCoroutine(StartScene());
    }

    protected IEnumerator StartScene()
    {
        yield return new WaitForSecondsRealtime(startIddleTime);
        Time.timeScale = 1;
    }

    public void OnFruitCollected()
    {
        fruitCollected++;

        if(fruitCollected == totalFruits)
        {
            Debug.Log("WIN!");
        }
    }

    public void OnPlayerDeath()
    {
        Debug.Log("GAME OVER!");
    }
}
