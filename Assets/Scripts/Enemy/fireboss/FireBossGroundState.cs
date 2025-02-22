using UnityEngine;

public class FireBossGroundState : EnemyState
{
    protected Enemy_FireBoss enemy;
    protected Transform player;

    public FireBossGroundState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_FireBoss _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = GameObject.Find("Player").transform;

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position, player.transform.position) < 2)
            stateMachine.ChangeState(enemy.battleState);
    }

}