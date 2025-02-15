using UnityEngine;

public class BanditAttackState : EnemyState
{
    private Enemy_Bandit enemy;
    public BanditAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Bandit _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        enemy.ZeroVelocity();

        if (triggerCalled)
            stateMachine.ChangeState(enemy.battleState);
    }
}
