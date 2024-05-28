using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrainTrigger : MonoBehaviour
{
    [SerializeField] private Train parentTrain;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            parentTrain.Depart();

            gameObject.SetActive(false);
            MySceneManager.i.ChangeSceneLong("WinScene");
        }
    }
}
