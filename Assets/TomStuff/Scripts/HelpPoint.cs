using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpPoint : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;
    [SerializeField] private Transform audioPoint;
    [SerializeField] private float clipLength = 10f;
    private bool canPlay = true;
    

    public void PlayAudio()
    {
        if (canPlay)
        {
            StartCoroutine(AudioTimer());
        }
    }

    IEnumerator AudioTimer()
    {
        canPlay = false;
        AudioSource.PlayClipAtPoint(_clip, audioPoint.position);
        yield return new WaitForSeconds(_clip.length);
        canPlay = true;
    }
}
