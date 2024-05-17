using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    public static Radio i;

    [SerializeField] private Transform audioPoint;
    [SerializeField] private AudioClip startClip;
    
    private AudioSource source;

    private void Awake()
    {
        if (i == null)
            i = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayAudio(startClip);
    }

    public void PlayAudio(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, audioPoint.position);
    }
}
