using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class SkeletonSpikeAttackState : BossState
{
    private Boss_Skeleton boss;

    private float spikeChargeTime = 1.5f; // 前摇持续时间
    private float spikeAttackDuration = 0.5f; // 主动作持续时间
    private float _stateTimer;
    private int phase; // 0: 前摇, 1: 主动作, 2: 后摇

    public SkeletonSpikeAttackState(Boss _bossBase, BossStateMachine _stateMachine, string _animBoolName, Boss_Skeleton boss) : base(_bossBase, _stateMachine, _animBoolName)
    {
        this.boss = boss;
    }

    public override void Enter()
    {
        base.Enter();
        phase = 0; // 进入前摇阶段
        _stateTimer = Time.time; // 初始化计时器
        boss.SetVelocity(0, rb.velocity.y); // 停止移动
        PerformSpikeAttack(); // 执行尖刺攻击逻辑
    }

    public override void Exit()
    {
        base.Exit();
        boss.SetVelocity(0, rb.velocity.y); // 确保退出时停止移动
    }

    public override void Update()
    {
        base.Update();

        switch (phase)
        {
            case 0: // 前摇阶段
                if (Time.time >= _stateTimer + spikeChargeTime)
                {
                    phase = 1; // 进入主动作阶段
                    _stateTimer = Time.time; // 重置计时器

                }
                break;

            case 1: // 主动作阶段
                if (Time.time >= _stateTimer + spikeAttackDuration)
                {
                    
                    phase = 2; // 进入后摇阶段
                }
                break;

            case 2: // 后摇阶段

                    stateMachine.ChangeState(boss.battleState); // 返回战斗状态
                
                break;
        }
    }

    private void PerformSpikeAttack()
    {
        // 在这里实现尖刺攻击的逻辑，例如生成尖刺或触发伤害范围
        Debug.Log("Spike Attack Triggered!");

        // 确保 spikePrefab 已经赋值
        if (boss.spikePrefab != null)
        {
            Vector3 spawnPosition = boss.sp.position; // 使用 Boss 子物体 sp 的位置

            // 实例化尖刺
            GameObject spike = Object.Instantiate(boss.spikePrefab, spawnPosition, Quaternion.identity);
            Spike spikeScript = spike.GetComponent<Spike>();

            // 判断 Boss 面朝的方向并传递给尖刺
            Vector2 direction = boss.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

            // 激活尖刺并传入发射方向
            spikeScript.Activate(direction);
        }
        else
        {
            Debug.LogError("SpikePrefab is not assigned in Boss_Skeleton!");
        }
    }


    private bool AnimationFinished()
    {
        return boss.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1;
    }
}
