using Oculus.Platform;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] Destinations;
    private NavMeshAgent agent;
    private int LocNum;
    private bool detected;

    private bool change = false;

    private bool camDetect = false;

    public bool camAlert = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        LocNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!camAlert)
        {
            detected = GetComponent<FieldOfView>().canSeePlayer;
            if (!detected)
            {
                if (Destinations.Length != 0)
                {
                    agent.destination = Destinations[LocNum].position;
                }
                if (!change)
                {
                    if ((agent.transform.position - Destinations[LocNum].position).magnitude < 1f)
                    {
                        change = true;
                        StartCoroutine(Wait());
                    }
                }
            }
        }
    }
    public IEnumerator Wait()
    {
        float delay = 0.5f;
        WaitForSeconds wait = new WaitForSeconds(delay);
        yield return wait;
        if (LocNum == Destinations.Length - 1)
        {
            LocNum = 0;
        }
        else
        {
            LocNum++;
        }
        change = false;
    }
    public void MoveToCameraPos(Vector3 CameraPos)
    {
        camAlert = true;
        agent.destination = CameraPos;
        if ((agent.transform.position - Destinations[LocNum].position).magnitude < 1f)
        {
            StartCoroutine(LookAround());
        }
    }
    public IEnumerator LookAround()
    {
        yield return new WaitForSeconds(3f);
        camAlert = false;

    }
}
