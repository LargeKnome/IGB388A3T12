using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager i;
    [SerializeField] private OVRScreenFade screenFade;

    private float gameTime;
    private string gameTimeStr;
    private bool tickTimer;

    public enum GameStates
    {
        Menu,
        Game
    }

    public GameStates gameState;

    void Awake()
    {
        if (i == null)
        {
            i = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
        
    }

    public void Start()
    {
        CalcSeconds(578);
    }

    public void StartGame()
    {
        ChangeSceneLong("TrainStationScene");
        gameState = GameStates.Game;
    }

    public void EndGame()
    {
        ChangeSceneLong("WinScene");
        tickTimer = false;
    }

    public void CalcSeconds(int time)
    {
        int gameTimeInt = Mathf.FloorToInt(time);
        gameTimeStr = Mathf.FloorToInt(gameTimeInt / 60).ToString("D2") + ": " + (gameTimeInt % 60).ToString("D2");
        Debug.Log(gameTimeStr);
    }

    public void ChangeSceneLong(string sceneName)
    {
        StartCoroutine(LongSceneTransition(sceneName, 3));
    }

    IEnumerator LongSceneTransition(string name, float lerpTime, float offsetTime = 0f)
    {
        yield return new WaitForSeconds(offsetTime);

        float lerpTimer = 0f;
        while (lerpTimer < lerpTime)
        {
            yield return new WaitForEndOfFrame();
            lerpTimer += Time.deltaTime;

            float ratio = lerpTimer / lerpTime;
            
            screenFade.SetUIFade(ratio);
        }

        yield return new WaitForSeconds(0.5f);
        
        screenFade.SetUIFade(0);
        
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }
}
