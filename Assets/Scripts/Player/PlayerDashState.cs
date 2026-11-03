using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.dashDuration;
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        float dashDir = Input.GetAxisRaw("Horizontal");
        if (dashDir == 0)
        {
            dashDir = player.facingDir;
        }
        player.SetVelocity(player.dashSpeed * dashDir, 0);
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (player.isWallDetected())
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
    }
}
