using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    public PlayerCounterAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.counterAttackDuration;
        player.anim.SetBool("SuccessfulCounterAttack", false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        Collider2D[] colloders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in colloders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                if (hit.GetComponent<Enemy>().CanbeStunned())
                {
                    stateTimer = 10;
                    player.anim.SetBool("SuccessfulCounterAttack", true);
                }

            }
        }
        if (stateTimer < 0 || triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}
