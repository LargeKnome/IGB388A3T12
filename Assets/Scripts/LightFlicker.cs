using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isFlickering = false;
    public float timeDelay;
    Light myLight;
    public float lightIntensity = 1;
    public float firstDelayRange1 = 0.01f;
    public float firstDelayRange2 = 2.0f;
    public float secondDelayRange1 = 0.01f;
    public float secondDelayRange2 = 0.5f;
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
        myLight.intensity = Mathf.PingPong(Time.time, lightIntensity);
        timeDelay = Random.Range(firstDelayRange1, firstDelayRange2);
        yield return new WaitForSeconds(timeDelay);
        myLight.enabled = true;
        timeDelay = Random.Range(secondDelayRange1, secondDelayRange2);
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;
    }
}
