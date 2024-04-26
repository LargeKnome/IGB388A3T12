using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    [SerializeField] private float soundThreshold;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame

    private void OnCollisionEnter(Collision other)
    {
        if (rb.velocity.magnitude > soundThreshold)
        {
            gameObject.GetComponent<Audible>().CreateSound(other.contacts[0].point, rb.velocity.magnitude);
        } 
    }
}
