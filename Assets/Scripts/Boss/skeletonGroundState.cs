using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletonGroundState : BossState
{
    protected Boss_Skeleton boss;
    public skeletonGroundState(Boss _bossBase, BossStateMachine _stateMachine, string _animBoolName, Boss_Skeleton boss) : base(_bossBase, _stateMachine, _animBoolName)
    {
        this.boss = boss;
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

        if (boss.IsPlayerDetected())
        {
            stateMachine.ChangeState(boss.battleState);
        }
    }
}