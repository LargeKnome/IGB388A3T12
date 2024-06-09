using Oculus.Interaction.Samples;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Hunter : MonoBehaviour
{
    public StateMachine stateMachine;
    public HunterIdleState idleState { get; private set; }
    public HunterWalkingState walkingState { get; private set; }

    public HunterTurningState turningState { get; private set; }
    public HunterDetectedState detectedState { get; private set; }
    public HunterRunningState runningState { get; private set; }
    public HunterCamDetected camDetectedState { get; private set; }
    public HunterSearchingState searchingState { get; private set; }


    public NavMeshAgent agent { get; private set; }
    public Animator animator { get; private set; }

    public GameObject player { get; private set; }

    private float speed;

    public Transform[] Destinations;

    public int LocNum;

    public bool camAlert;
    public Vector3 CamAlertPos;

    public float rotate;

    public bool isSearching = false;

    public float searchingTime;

    private Quaternion startRotation;
    private Quaternion endRotation;
    private Quaternion targetRotation;

    public float walkSpeed;
    public float runSpeed;
    // Start is called before the first frame update
    void Start()
    {
        startRotation = Quaternion.Euler(Vector3.up * rotate);
        endRotation = Quaternion.Euler(Vector3.up * -rotate);
        targetRotation = startRotation;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        stateMachine = new StateMachine();

        speed = agent.speed;

        idleState = new HunterIdleState(this, stateMachine, "isIdle", Destinations);
        walkingState = new HunterWalkingState(this, stateMachine, "isWalking", Destinations);
        turningState = new HunterTurningState(this, stateMachine, "isTurning");
        detectedState = new HunterDetectedState(this, stateMachine, "isDetected");
        runningState = new HunterRunningState(this, stateMachine, "isChasing");
        camDetectedState = new HunterCamDetected(this, stateMachine, "isCameraDetected");
        searchingState = new HunterSearchingState(this, stateMachine, "isSearching");




        stateMachine.Initialize(idleState);

        LocNum = 0;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        stateMachine.CurrentState.UpdateLogic();
    }
    public virtual void FixedUpdate()
    {
        stateMachine.CurrentState.UpdatePhysics();
    }

    public void MoveToCameraPos(Vector3 CameraPos)
    {
        camAlert = true;
        CamAlertPos = CameraPos;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LevelManager.i.EndLevel(false);
        }
    }
    public void Looking()
    {
        StartCoroutine(LookAround());
    }
    public IEnumerator LookAround()
    {
        isSearching = true;
        yield return new WaitForSeconds(searchingTime);
        isSearching = false;
        camAlert = false;

    }
    public void Search()
    {
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, 2 * Time.deltaTime);
        if (Quaternion.Angle(transform.localRotation, targetRotation) < 0.1f)
        {
            if (targetRotation == endRotation)
            {
                targetRotation = startRotation;
            }
            else
            {
                targetRotation = endRotation;
            }
        }
    }
}
