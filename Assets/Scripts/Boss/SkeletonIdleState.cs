using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleState : skeletonGroundState
{
    public SkeletonIdleState(Boss _bossBase, BossStateMachine _stateMachine, string _animBoolName, Boss_Skeleton boss) : base(_bossBase, _stateMachine, _animBoolName, boss)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = boss.idletime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(stateTimer < 0)
        {
            stateMachine.ChangeState(boss.moveState);   
        }

    }
}
