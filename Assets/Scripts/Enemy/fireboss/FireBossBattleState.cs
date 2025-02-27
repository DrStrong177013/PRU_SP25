using UnityEngine;

public class FireBossBattleState : EnemyState
{
    private Transform player;
    private Enemy_FireBoss enemy;
    private int moveDir;
    public FireBossBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_FireBoss _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = GameObject.Find("Player").transform;
    }

    public override void Update()
    {
        base.Update();
        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.battleTime;
            enemy.anim.SetBool("Idle", false);
            enemy.anim.SetBool("Walk", true);
            if (enemy.IsPlayerDetected().distance <= enemy.attackDistance)
            {

                if (CanAttack())
                {
                    stateMachine.ChangeState(enemy.attackState);

                }
                else
                {
                    enemy.SetZeroVelocity();
                    enemy.anim.SetBool("Walk", false);
                    enemy.anim.SetBool("Idle", true);

                    return;
                }
            }
        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 7)
                stateMachine.ChangeState(enemy.idleState);
        }
        if (player.position.x > enemy.transform.position.x)
            moveDir = 1;
        else if (player.position.x < enemy.transform.position.x)
            moveDir = -1;

        enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.linearVelocity.y);
    }
    public override void Exit()
    {
        base.Exit();
    }

    private bool CanAttack()
    {
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            enemy.lastTimeAttacked = Time.time;
            return true;
        }
        return false;
    }
}