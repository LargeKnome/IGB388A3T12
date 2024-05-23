using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterRunningState : BaseState
{
    public HunterRunningState(Hunter hunter, StateMachine stateMachine, string animBoolName) : base(hunter, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        hunter.agent.destination = hunter.player.transform.position;
    }

    public override void Exit()
    {
        base.Exit();
        hunter.GetComponent<FieldOfView>().playerDetected = false;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (hunter.GetComponent<FieldOfView>() != null)
        {
            if (!hunter.GetComponent<FieldOfView>().canSeePlayer)
            {
                stateMachine.ChangeState(hunter.idleState);
            }
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
