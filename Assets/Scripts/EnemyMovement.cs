using Oculus.Platform;
using Oculus.Platform.Samples.VrHoops;
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

    private GameObject playerRef;

    private Vector3 CamAlertPos;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        LocNum = 0;
        playerRef = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (!camAlert)
        {
            detected = GetComponent<FieldOfView>().playerDetected;
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
        else
        {
            if ((agent.transform.position - CamAlertPos).magnitude < 1f)
            {
                StartCoroutine(LookAround());
            }
        }
        if (GetComponent<FieldOfView>().canSeePlayer)
        {
            agent.destination = transform.position;
        }
    }
    public void MoveTo()
    {
        agent.destination = playerRef.transform.position;
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
        CamAlertPos = CameraPos;
    }
    public IEnumerator LookAround()
    {
        yield return new WaitForSeconds(3f);
        camAlert = false;

    }
}
