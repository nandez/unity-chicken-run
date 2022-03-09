using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float startIddleTime = 5f;
    void Start()
    {
        Time.timeScale = 0;
        StartCoroutine(StartScene());
    }

    void Update()
    {
        
    }

    protected IEnumerator StartScene()
    {
        yield return new WaitForSecondsRealtime(startIddleTime);
        Time.timeScale = 1;
    }
}
