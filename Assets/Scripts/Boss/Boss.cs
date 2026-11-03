using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Entity 
{
    [SerializeField]protected LayerMask whatIsPlayer;
    [Header("Move info")]
    public float movespeed;
    public float idletime;

    [Header("Attack info")]
    public float attackDistance;
    public float attackCooldown;
    public float spikeAttackDistance ; // 距离超过时进入 SpikeAttackState
    [HideInInspector] public float lastTimeAttacked;

    [SerializeField] public bool IsBoss;



    public BossStateMachine stateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new BossStateMachine();
        InitializeFacingDirection(-1);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    public virtual void AnimationFinishTriggers() => stateMachine.currentState.AnimationFinishTrigger();
    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position,Vector2.right * facingDir,50,whatIsPlayer);  

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position,new Vector3(transform.position.x + attackDistance * facingDir,transform.position.y));
    }
}
