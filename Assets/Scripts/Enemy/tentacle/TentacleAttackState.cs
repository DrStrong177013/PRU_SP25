using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleAttackState : EnemyState
{
    private Enemy_Tentacle enemy;
    private Transform player;

    public TentacleAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Tentacle _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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
        enemy.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        enemy.SetZeroVelocity();

        if (triggerCalled)
        {
            if (IsPlayerInAttackZone())
            {
                PullDownPlayer();
            }
            stateMachine.ChangeState(enemy.battleState);
        }
    }

    private bool IsPlayerInAttackZone()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
                return true;
            }
        }
        return false;
    }

    private void PullDownPlayer()
    {
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();

        if (playerRb != null && enemyRb != null)
        {
            int ignoreGroundLayer = LayerMask.NameToLayer("IgnoreGround");

            player.gameObject.layer = ignoreGroundLayer;
            enemy.gameObject.layer = ignoreGroundLayer;
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, -enemy.pullDownForce);
            enemyRb.linearVelocity = new Vector2(enemyRb.linearVelocity.x, -enemy.pullDownForce);

            enemy.StartCoroutine(RestoreLayer());
        }
    }

    private IEnumerator RestoreLayer()
    {
        yield return new WaitForSeconds(2f);
        int playerLayer = LayerMask.NameToLayer("Player");
        if (playerLayer != -1)
        {
            player.gameObject.layer = playerLayer;
        }
    }
}