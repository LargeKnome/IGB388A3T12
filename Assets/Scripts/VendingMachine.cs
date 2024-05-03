using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : MonoBehaviour
{
    [SerializeField] private Transform startPos;
    [SerializeField] private GameObject toSpawn;
    [SerializeField] private float laucnhForce;
    [SerializeField] private float spawnDelay = .5f;

    private bool canSpawn = true;
    
    public void OnCollide()
    {
        if (!canSpawn)
        {
            return;
        }
        
        GameObject newGameObject = Instantiate(toSpawn, startPos.position, startPos.rotation);
        newGameObject.GetComponent<Rigidbody>().AddForce(newGameObject.transform.forward * laucnhForce);
        StartCoroutine(SpawnDelayFunc());
    }

    IEnumerator SpawnDelayFunc()
    {
        canSpawn = false;
        yield return new WaitForSeconds(spawnDelay);
        canSpawn = true;
    }
}