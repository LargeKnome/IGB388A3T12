using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterWalkingState : BaseState
{
    private Transform[] Destinations;

    public HunterWalkingState(Hunter hunter, StateMachine stateMachine, string animBoolName, Transform[] Destinations) : base(hunter, stateMachine, animBoolName)
    {
        this.Destinations = Destinations;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (hunter.camAlert)
        {
            stateMachine.ChangeState(hunter.camDetectedState);
        }
        if (hunter.GetComponent<FieldOfView>() != null)
        {
            if (hunter.GetComponent<FieldOfView>().canSeePlayer && !hunter.GetComponent<FieldOfView>().playerDetected)
            {
                stateMachine.ChangeState(hunter.detectedState);
            }
            else
            {
                if (Destinations.Length != 0)
                {
                    hunter.agent.destination = Destinations[hunter.LocNum].position;
                }
                else
                {
                    stateMachine.ChangeState(hunter.idleState);
                }
                if ((hunter.agent.transform.position - Destinations[hunter.LocNum].position).magnitude < 1f)
                {
                    if (hunter.LocNum == hunter.Destinations.Length - 1)
                    {
                        hunter.LocNum = 0;
                    }
                    else
                    {
                        hunter.LocNum++;
                    }
                    stateMachine.ChangeState(hunter.turningState);

                }
            }
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
