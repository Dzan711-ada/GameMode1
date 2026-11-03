using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttackState : BossState
{
    private Boss_Skeleton boss;
    private float rollDuration = 2f; // 主动作持续时间
    private float rollSpeedMultiplier = 4f; // 移动速度倍数

    private float _stateTimer;
    private int phase; // 0: 前摇, 1: 主动作, 2: 后摇
    public SkeletonAttackState(Boss _bossBase, BossStateMachine _stateMachine, string _animBoolName, Boss_Skeleton boss) : base(_bossBase, _stateMachine, _animBoolName)
    {
        this.boss = boss;
    }

    public override void Enter()
    {
        base.Enter();
        phase = 0; // 进入前摇阶段
        _stateTimer = Time.time; // 初始化计时器
        boss.SetZeroVelocity(); // 停止移动
    }

    public override void Exit()
    {
        base.Exit();
        boss.SetVelocity(0, rb.velocity.y);
        boss.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        switch (phase)
        {
            case 0: // 前摇阶段
                if (AnimationFinished())
                {
                    phase = 1; // 进入主动作阶段
                    stateTimer = Time.time; // 重置计时器
                }
                break;

            case 1: // 主动作阶段
                boss.SetVelocity(boss.movespeed * rollSpeedMultiplier * boss.facingDir, rb.velocity.y);
                if (Time.time >= stateTimer + rollDuration)
                {
                    phase = 2; // 进入后摇阶段
                    boss.SetVelocity(0, rb.velocity.y); // 停止移动
                }
                break;

            case 2: // 后摇阶段
                if (AnimationFinished())
                {
                    stateMachine.ChangeState(boss.battleState); // 返回战斗状态
                }
                break;
        }
    }
       private bool AnimationFinished()
    {
        return boss.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1;
    }
}
