using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Platform.Samples.VrHoops;
using UnityEngine;

public class RadioTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Radio.i.PlayAudio(_clip);
            gameObject.SetActive(false);
        }
    }
}
