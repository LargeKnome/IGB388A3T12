using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TicketMachine : MonoBehaviour
{
    [SerializeField] private Transform raycastTransform;
    [SerializeField] private string[] acceptedTags;
    [SerializeField] private float distance;
    [SerializeField] private float radius;

    private Animation _animation;
    
    [SerializeField] private UnityEvent onScan;
    private bool locked = true;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _animation = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (locked)
        {
            RaycastHit[] hits = Physics.SphereCastAll(raycastTransform.position, radius, raycastTransform.transform.up);
            foreach (RaycastHit hit in hits)
            {
                int tagId = Array.IndexOf(acceptedTags, hit.transform.tag);
                if (tagId != -1)
                {
                    locked = false;
                    StartCoroutine(OpenCR());
                }
            }
        }
    }

    IEnumerator OpenCR()
    {
        yield return new WaitForSeconds(.3f);
        onScan.Invoke();
    }

    public void PlayAnim()
    {
        _animation.Play();
    }
}
