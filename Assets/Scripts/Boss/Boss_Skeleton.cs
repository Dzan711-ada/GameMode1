using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Skeleton : Boss
{
    #region States
    public GameObject spikePrefab; // ÓÃÓÚ´æ´¢¼â´ÌµÄ Prefab
    public SkeletonIdleState idleState { get; private set; }
    public SkeletonMoveState moveState { get; private set; }
    public SkeletonBattleState battleState { get; private set; }
    public SkeletonAttackState RollattackState { get; private set; }
    public SkeletonSpikeAttackState SpikeAttackState { get; private set; }
    public MinionAttackState attackState { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();

        idleState = new SkeletonIdleState(this,stateMachine,"Idle",this);
        moveState = new SkeletonMoveState(this, stateMachine, "Move", this);
        battleState = new SkeletonBattleState(this, stateMachine, "Move", this);
        RollattackState = new SkeletonAttackState(this, stateMachine, "RollAttack",this);
        SpikeAttackState = new SkeletonSpikeAttackState(this, stateMachine, "SpikeAttack", this);
        attackState = new MinionAttackState(this, stateMachine, "Attack", this);
    }

    protected override void Start()
    {
        base.Start();
        
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }
}
