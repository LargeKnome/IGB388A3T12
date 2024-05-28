using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Punchable : MonoBehaviour
{
    [SerializeField] private float minimumSpeed = 0.05f;
    [SerializeField] private AudioClip[] onPunchAudio;
    
    [SerializeField] private UnityEvent onCollide;
    // Start is called before the first frame update
    void Start()
    {
        
        AudioClip currentClip = onPunchAudio[Random.Range(0, onPunchAudio.Length)];
        Debug.Log(currentClip.name);
        AudioSource.PlayClipAtPoint(currentClip, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            PlayerFist playerFist;
            if (other.TryGetComponent<PlayerFist>(out playerFist))
            {
                float handSpeed = playerFist.GetHandSpeed();
                if (handSpeed >= minimumSpeed)
                {
                    if (onPunchAudio.Length > 0)
                    {
                        Debug.Log("playing audio");
                        AudioClip currentClip = onPunchAudio[Random.Range(0, onPunchAudio.Length)];
                        Debug.Log(currentClip.name);
                        AudioSource.PlayClipAtPoint(currentClip, Vector3.zero);
                    }
                    onCollide.Invoke();
                }
            }
        }
    }
}
