using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterSearchingState : BaseState
{
    public HunterSearchingState(Hunter hunter, StateMachine stateMachine, string animBoolName) : base(hunter, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        hunter.Looking();
        hunter.agent.enabled = false;
        hunter.isSearching = true;
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (hunter.isSearching)
        {
            hunter.Search();
        }
        else
        {

            stateMachine.ChangeState(hunter.idleState);
            hunter.agent.enabled = true;
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
