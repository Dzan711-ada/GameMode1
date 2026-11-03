using UnityEngine;

public class SkeletonBattleState : BossState
{
    private Transform player;
    private Boss_Skeleton boss;
    private int moveDir;
    private float attackChargeTime = 3f; // 充能时间（每5秒充能一次）
    private float nextAttackChargeTime; // 下一次充能时间

    public SkeletonBattleState(Boss _bossBase, BossStateMachine _stateMachine, string _animBoolName, Boss_Skeleton boss) : base(_bossBase, _stateMachine, _animBoolName)
    {
        this.boss = boss;
    }

    public override void Enter()
    {
        base.Enter();
        player = GameObject.Find("Player").transform;
        nextAttackChargeTime = Time.time + attackChargeTime; // 初始化充能时间
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        // 检查是否是Boss
        if (boss.IsBoss) // Boss特有的行为
        {
            HandleBossBehavior();
        }
        else // 小怪行为
        {
            HandleMinionBehavior();
        }
    }

    private void HandleBossBehavior()
    {
        // 玩家检测逻辑
        if (boss.IsPlayerDetected())
        {
            float distanceToPlayer = boss.IsPlayerDetected().distance;

            if (CanAttack()) // 检查是否可以攻击
            {
                if (distanceToPlayer > boss.spikeAttackDistance) // 如果距离超过 spikeAttack 的阈值
                {
                    stateMachine.ChangeState(boss.SpikeAttackState);
                }
                else // 如果距离在 rollAttack 的范围内
                {
                    stateMachine.ChangeState(boss.RollattackState);
                }
            }
        }

        // 移动方向逻辑
        if (player.position.x > boss.transform.position.x)
        {
            moveDir = 1;
        }
        else if (player.position.x < boss.transform.position.x)
        {
            moveDir = -1;
        }

        boss.SetVelocity(boss.movespeed * moveDir, boss.rb.velocity.y);
    }

    private void HandleMinionBehavior()
    {
        // 小怪的攻击行为，可能只有简单攻击
        if (boss.IsPlayerDetected().distance < boss.attackDistance)
        {
            stateMachine.ChangeState(boss.attackState); // 小怪执行简单攻击
        }

        // 小怪的移动逻辑
        if (player.position.x > boss.transform.position.x)
        {
            moveDir = 1;
        }
        else if (player.position.x < boss.transform.position.x)
        {
            moveDir = -1;
        }

        boss.SetVelocity(boss.movespeed * moveDir, boss.rb.velocity.y);
    }

    private bool CanAttack()
    {
        // 检查是否达到充能时间
        if (Time.time >= nextAttackChargeTime)
        {
            nextAttackChargeTime = Time.time + attackChargeTime; // 更新下一次充能时间
            return true;
        }
        return false;
    }
}