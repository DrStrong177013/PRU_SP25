using UnityEngine;

public class BoneWizardDeadState : EnemyState
{
    private Enemy_BoneWizard enemy;
    public BoneWizardDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_BoneWizard _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.anim.SetBool(enemy.lastAnimBoolName, true);
        enemy.anim.speed = 0;
        enemy.cd.enabled = false;
        stateTimer = .2f;
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer > 0)
            rb.linearVelocity = new Vector2(0, 10);
    }
}
