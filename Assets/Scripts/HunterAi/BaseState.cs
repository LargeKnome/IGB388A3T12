using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;
using UnityEngine;

public class BaseState
{
    public Hunter hunter;
    protected StateMachine stateMachine;

    protected float startTime;

    protected string animBoolName;
    public BaseState(Hunter hunter, StateMachine stateMachine, string animBoolName)
    {
        this.hunter = hunter;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }
    public virtual void Enter()
    {
        startTime = Time.time;
        hunter.animator.SetBool(animBoolName, true);

    }
    public virtual void UpdateLogic()
    {

    }
    public virtual void UpdatePhysics()
    {

    }
    public virtual void Exit()
    {
        hunter.animator.SetBool(animBoolName, false);
    }

}
