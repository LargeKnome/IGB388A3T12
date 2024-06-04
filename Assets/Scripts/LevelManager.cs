using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private float gameTime;
    private bool tickTimer;

    public static LevelManager i;

    // Start is called before the first frame update

    void Awake()
    {
        if (i == null)
        {
            i = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        StartCoroutine(LevelTimer());
    }

    public void EndLevel(bool playerWon)
    {
        tickTimer = false;
        GameManager.i.gameTime = gameTime;
        GameManager.i.gameWon = playerWon;
        GameManager.i.ChangeSceneLong("WinScene");
    }

    IEnumerator LevelTimer()
    {
        tickTimer = true;
        while (tickTimer)
        {
            gameTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
