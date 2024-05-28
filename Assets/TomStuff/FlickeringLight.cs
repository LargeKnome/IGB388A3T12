using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    [SerializeField] private GameObject onState;
    [SerializeField] private GameObject offState;
    

    public bool flickerEnabled = false;

    [SerializeField] private int minFlickers=1;
    [SerializeField] private int maxFlickers=5;
    
    [SerializeField] private float flickerDuration;
    
    [SerializeField] private float maxFlickerInterval;
    [SerializeField] private float minFlickerInterval;

    [SerializeField] private float minTimeBetweenFlickers;
    [SerializeField] private float maxTimeBetweenFlickers;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FlickerLoop());
    }

    IEnumerator FlickerLoop()
    {
        while (true)
        {
            if (flickerEnabled)
            {
                Debug.Log(gameObject.name);
                float flickerInterval = Random.Range(minFlickerInterval, maxFlickerInterval);
                int flickers = Random.Range(minFlickers, maxFlickers);
                
                Debug.Log(flickerInterval);
                
                yield return new WaitForSeconds(flickerInterval);
                for (int i = 0; i < flickers; i++)
                {
                    float timeBetweenFlickers = Random.Range(minTimeBetweenFlickers, maxTimeBetweenFlickers);
                    yield return new WaitForSeconds(timeBetweenFlickers);
                    
                    onState.SetActive(false);
                    offState.SetActive(true);
                    yield return new WaitForSeconds(flickerDuration);
                    onState.SetActive(true);
                    offState.SetActive(false);
                }
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
