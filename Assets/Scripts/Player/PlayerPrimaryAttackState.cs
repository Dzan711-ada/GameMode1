using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    private int comboCounter = 0;
    private float lastTimeAttack;
    private const float comboWindow = 0.5f;

    public override void Enter()
    {
        base.Enter();
        if (Time.time - lastTimeAttack > comboWindow)
            comboCounter = 0;
        player.anim.SetInteger("ComboCounter", comboCounter);
        stateTimer = 0.1f;
    }

    public override void Exit()
    {
        base.Exit();
        comboCounter = (comboCounter + 1) % 3;
        lastTimeAttack = Time.time;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            player.SetVelocity(0, rb.velocity.y);

        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
