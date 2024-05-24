using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Platform.Samples.VrHoops;
using UnityEngine;

public class RadioTrigger : MonoBehaviour
{
    [SerializeField] private HelpPoint _helpPoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _helpPoint.PlayAudio();
            gameObject.SetActive(false);
        }
    }
}
