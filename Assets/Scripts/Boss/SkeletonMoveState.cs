using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMoveState : skeletonGroundState
{
    public SkeletonMoveState(Boss _bossBase, BossStateMachine _stateMachine, string _animBoolName, Boss_Skeleton boss) : base(_bossBase, _stateMachine, _animBoolName, boss)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        boss.SetVelocity( boss.movespeed * boss.facingDir, rb.velocity.y );
        if (boss.isWallDetected() || !boss.isGroundedDetected())
        {
            boss.Flip();
            stateMachine.ChangeState(boss.idleState);   
        }
        

    }
}
