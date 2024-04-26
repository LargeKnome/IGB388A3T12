using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Listener : MonoBehaviour
{
    [SerializeField] private GameObject trueObject;

    [SerializeField] private GameObject falseObject;
    // Start is called before the first frame update
    void Start()
    {
        trueObject.SetActive(true);
        falseObject.SetActive(false);
    }

    // Update is called once per frame
    public void Ping()
    {
        StartCoroutine(ReactToPing());
    }

    IEnumerator ReactToPing()
    {
        trueObject.SetActive(false);
        falseObject.SetActive(true);

        yield return new WaitForSeconds(1);
        
        trueObject.SetActive(true);
        falseObject.SetActive(false);
    }
}
