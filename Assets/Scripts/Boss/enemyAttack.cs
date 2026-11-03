using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAttackState : BossState
{
    private Boss_Skeleton boss;
    public MinionAttackState(Boss _bossBase, BossStateMachine _stateMachine, string _animBoolName, Boss_Skeleton boss) : base(_bossBase, _stateMachine, _animBoolName)
    {
        this.boss = boss;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = Time.time; // 初始化计时器
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        boss.SetVelocity(0, rb.velocity.y);
        if(triggerCalled)
        stateMachine.ChangeState(boss.battleState); // 播放完动画后返回战斗状态
        
    }

    private void PlayAttackAnimation()
    {
        // 在这里播放小怪的攻击动画
        boss.anim.SetTrigger("Attack"); // 假设小怪的动画中有一个“Attack”触发器
    }
}
