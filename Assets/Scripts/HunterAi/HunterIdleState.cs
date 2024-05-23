using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterIdleState : BaseState
{
    private Transform[] Destinations;
    public HunterIdleState(Hunter hunter, StateMachine stateMachine, string animBoolName, Transform[] destinations) : base(hunter, stateMachine, animBoolName)
    {
        Destinations = destinations;
    }

    public override void Enter()
    {
        base.Enter();
        bool hasDest = true;
        if (Destinations.Length != 0)
        {
            foreach (Transform t in Destinations)
            {
                if (t == null)
                {
                    hasDest = false;
                }
            }
            if (hasDest)
            {
                stateMachine.ChangeState(hunter.walkingState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
