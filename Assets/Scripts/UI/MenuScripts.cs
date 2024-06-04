using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScripts : MonoBehaviour
{

    public void ChangeScene(string newScene)
    {
        GameManager.i.ChangeSceneLong(newScene);
    }
}
