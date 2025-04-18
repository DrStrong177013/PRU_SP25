using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{

    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(0, null);

    }

    public override void Exit()
    {
        base.Exit();
        //AudioManager.instance.StopSFX(0);
    }

    public override void Update()
    {

        base.Update();
        player.dust.Play();
        player.SetVelocity(xInput * player.moveSpeed, rb.linearVelocity.y);


        if (xInput == 0 || player.IsWallDetected())
            stateMachine.ChangeState(player.idleState);
    }
}
