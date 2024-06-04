using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;

    [SerializeField] private GameObject gameWonText;
    [SerializeField] private GameObject gameLostText;
    // Start is called before the first frame update
    void Start()
    {
        //Load Best Time
        float time = GameManager.i.gameTime;
        string timeTextStr = GameManager.i.CalcSeconds(time);
        timeText.text = timeTextStr;
        
        gameWonText.SetActive(GameManager.i.gameWon);
        gameLostText.SetActive(!GameManager.i.gameWon);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
