using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audible : MonoBehaviour
{
    [SerializeField] private LayerMask listeners;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CreateSound(Vector3 position, float radius)
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 2.5f, transform.forward, 0f, listeners);
        foreach (RaycastHit hit in hits)
        {
            GameObject hitObject = hit.transform.gameObject;
            hitObject.GetComponent<Listener>().Ping();
        }
    }
}
