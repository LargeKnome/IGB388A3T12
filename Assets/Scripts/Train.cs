using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField] private Animator[] doors;
    // Start is called before the first frame update

    private float trainSpeed = 0;
    private float targetSpeed = 0;
    
    void Start()
    {
        foreach (Animator door in doors) 
        {
            door.SetBool("IsOpen", true);
        }
    }

    public void Depart()
    {
        StartCoroutine(DepartRoutine());
    }

    IEnumerator DepartRoutine()
    {
        yield return new WaitForSeconds(1);
        foreach (Animator door in doors)
        {
            door.SetBool("IsOpen", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * (targetSpeed * Time.deltaTime);
    }
}
