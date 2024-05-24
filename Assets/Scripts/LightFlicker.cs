using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isFlickering = false;
    public float timeDelay;
    Light myLight;

    void Start()
    {
        myLight = this.gameObject.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlickering == false)
        {
            StartCoroutine(FlickeringLight());
        }    
    }

    IEnumerator FlickeringLight()
    {
        isFlickering = true;
        myLight.enabled = false;
        myLight.intensity = Mathf.PingPong(Time.time, 1);
        timeDelay = Random.Range(0.01f, 2f);
        yield return new WaitForSeconds(timeDelay);
        myLight.enabled = true;
        timeDelay = Random.Range(0.01f, 0.5f);
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;
    }
}
