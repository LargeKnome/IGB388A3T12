using Oculus.Platform.Models;
using Oculus.Platform.Samples.VrHoops;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;
using UnityEngine;

public class HunterTurningState : BaseState
{
    private float facingThreshold = 10f;
    public HunterTurningState(Hunter hunter, StateMachine stateMachine, string animBoolName) : base(hunter, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        hunter.agent.enabled = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();


        Vector3 directionToTarget = (hunter.Destinations[hunter.LocNum].position - hunter.transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        hunter.transform.rotation = Quaternion.Slerp(hunter.transform.rotation, targetRotation, 5 * Time.deltaTime);
        if (isFacingTarget(directionToTarget))
        {
            hunter.agent.enabled = true;
            stateMachine.ChangeState(hunter.idleState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
    bool isFacingTarget(Vector3 directionToTarget)
    {
        // Calculate the angle between the character's forward direction and the target direction
        float angle = Vector3.Angle(hunter.transform.forward, directionToTarget);

        // Check if the angle is within the threshold
        return angle < facingThreshold;
    }
}
