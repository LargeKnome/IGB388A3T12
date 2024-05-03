using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Punchable : MonoBehaviour
{
    [SerializeField] private UnityEvent onCollide;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("Hand") || other.gameObject.CompareTag("Throwable"))
        {
            //Debug.Log("Collide");
            Rigidbody otherRB = other.gameObject.GetComponent<Rigidbody>();
            if (other.relativeVelocity.magnitude > 1)
            {
                onCollide.Invoke();
            }
        }
    }
}
