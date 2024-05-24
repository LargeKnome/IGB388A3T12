using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private DiegeticRotator doorHandle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (doorHandle.CurrentValue <= 0.3f)
        {
            Debug.Log("Open yoooo");
        }
    }
}
