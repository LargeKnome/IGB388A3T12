using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager i;
    [SerializeField] private OVRScreenFade screenFade;
    [NonSerialized] public float gameTime = 0;

    [NonSerialized] public bool gameWon = true;

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
        screenFade = FindObjectOfType<OVRScreenFade>();
    }
    public string CalcSeconds(float time)
    {
        int gameTimeInt = Mathf.FloorToInt(time);
        string gameTimeStr = Mathf.FloorToInt(gameTimeInt / 60).ToString("D2") + ": " + (gameTimeInt % 60).ToString("D2");
        return gameTimeStr;
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
        SceneManager.LoadScene(name);
    }
}
