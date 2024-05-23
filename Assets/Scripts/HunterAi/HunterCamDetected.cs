using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterCamDetected : BaseState
{
    public HunterCamDetected(Hunter hunter, StateMachine stateMachine, string animBoolName) : base(hunter, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void Exit()
    {
        base.Exit();
        hunter.camAlert = false;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        hunter.agent.destination = hunter.CamAlertPos;
        if ((hunter.agent.transform.position - hunter.CamAlertPos).magnitude < 1f)
        {
            stateMachine.ChangeState(hunter.searchingState);
        }
        if (hunter.GetComponent<FieldOfView>().canSeePlayer)
        {
            stateMachine.ChangeState(hunter.detectedState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
