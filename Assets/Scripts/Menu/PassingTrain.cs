using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassingTrain : MonoBehaviour
{
    [SerializeField] private float velocity = 15f;
    [SerializeField] private float lifespan = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LifespanTimer());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * (velocity * Time.deltaTime);
    }

    IEnumerator LifespanTimer()
    {
        yield return new WaitForSeconds(lifespan);
        Destroy(this.gameObject);
    }
}
