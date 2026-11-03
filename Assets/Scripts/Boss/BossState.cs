using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossState 
{
    protected BossStateMachine stateMachine;
    protected Boss bossBase;
    protected Rigidbody2D rb;

    protected float stateTimer;
    protected bool triggerCalled;

    private string animBoolName;
 

    public BossState(Boss _bossBase,BossStateMachine  _stateMachine,string _animBoolName)
    {
        this.bossBase = _bossBase;
        this.stateMachine  = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void Enter()
    {
        triggerCalled = false;
        rb = bossBase.rb;
        bossBase.anim.SetBool(animBoolName , true);
    }

    public virtual void Exit()
    {
        bossBase.anim.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
