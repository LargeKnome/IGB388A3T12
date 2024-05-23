using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void Canvas(Canvas canvas)
    {
        if (canvas.enabled)
        {
            canvas.enabled = false;
        }
        else
        {
            canvas.enabled = true;
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
