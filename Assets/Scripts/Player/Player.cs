using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class Player : Entity
{
    public bool isBusy { get; private set; }

    [Header("CL")]// 碰撞体
    public Collider2D cl;

    [Header("Move info")]
    public float moveSpeed;
    public float jumpForce;

    [Header("Dash info")]
    [SerializeField] private float dashCoolDown;
    private float dashUsageTimer;
    public float dashSpeed;
    public float dashDuration;

    [Header("WallJump info")]
    public float wallJumpDuration;

    #region States
    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerPrimaryAttackState primaryAttackState { get; private set; }
    #endregion

    



    protected override void Awake()
    {
        base.Awake();

        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        primaryAttackState = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
    }



    // 恢复血量方法
    /*
     * public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        Debug.Log("Player healed " + healAmount + " health. Current health: " + currentHealth);
    }
    */

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        CheckForDashInput();
        if (Input.GetKeyDown(KeyCode.S))// X键按下穿墙
        {
            cl.enabled = false;
            
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            cl.enabled = true;
            
        }
    }
        public IEnumerator BusyFor(float second)
        {
            isBusy = true;
            yield return new WaitForSeconds(second);
            isBusy = false;
        }

        private void CheckForDashInput()
        {
            if (isWallDetected()) { return; }

            dashUsageTimer -= Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer < 0)
            {
                stateMachine.ChangeState(dashState);
                dashUsageTimer = dashCoolDown;
            }
        }

        public void AnimationTrigger() => stateMachine.currentState.AnimationTrigger();
    }

