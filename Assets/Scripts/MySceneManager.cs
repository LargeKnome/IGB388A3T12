using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager i;
    [SerializeField] private OVRScreenFade screenFade;

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