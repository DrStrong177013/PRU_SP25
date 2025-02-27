using UnityEngine;

public class FireBossDeadState : EnemyState
{
    private Enemy_FireBoss enemy;
    public FireBossDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_FireBoss _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.anim.SetBool("Die", true);
    }

    public override void Update()
    {
        base.Update();
    }
}