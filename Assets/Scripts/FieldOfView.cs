using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0,360)]
    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    private NavMeshAgent agent;

    public bool playerDetected;

    private Vector3 hunterOrigin;

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
        agent = GetComponent<NavMeshAgent>();
        hunterOrigin = transform.position;

    }
    private void Update()
    {
        if (canSeePlayer & !playerDetected)
        {
            transform.LookAt(playerRef.transform.position);
            StartCoroutine(DetectPlayer());
        }
        else if (!canSeePlayer)
        {
            playerDetected = false;
        }
        if (playerDetected)
        {
            MoveTo();
        }
    }
    private IEnumerator FOVRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);
        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }
    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0 )
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if(Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    Debug.Log("CANSEE");
                    canSeePlayer = true;
                    GetComponent<EnemyMovement>().camAlert = false;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
    }
    private void MoveTo()
    {
        agent.destination = playerRef.transform.position;
    }

    private IEnumerator DetectPlayer()
    {
        WaitForSeconds wait = new WaitForSeconds(5f);
        yield return wait;
        if (canSeePlayer)
        {
            playerDetected = true;
        }
        yield return null;
    }
}
