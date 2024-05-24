using Oculus.Platform.Samples.VrHoops;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterDetectedState : BaseState
{
    public HunterDetectedState(Hunter hunter, StateMachine stateMachine, string animBoolName) : base(hunter, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        hunter.agent.enabled = false;
        hunter.GetComponent<FieldOfView>().DetectingPlayer();
    }

    public override void Exit()
    {
        base.Exit();
        hunter.agent.enabled = true;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        var targetRotation = Quaternion.LookRotation(hunter.player.transform.position - hunter.transform.position);

        // Smoothly rotate towards the target point.
        hunter.transform.rotation = Quaternion.Slerp(hunter.transform.rotation, targetRotation, 5 * Time.deltaTime);
        if (hunter.GetComponent<FieldOfView>().playerDetected)
        {
            stateMachine.ChangeState(hunter.runningState);
        }
        else if (!hunter.GetComponent<FieldOfView>().canSeePlayer)
        {
            stateMachine.ChangeState(hunter.idleState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }

}
