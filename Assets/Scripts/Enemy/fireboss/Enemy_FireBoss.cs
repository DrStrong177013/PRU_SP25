using UnityEngine;

public class Enemy_FireBoss : Enemy
{
    [Header("Attack details")]
    public float pureDamage = 10f;

    #region States

    public FireBossIdleState idleState { get; private set; }
    public FireBossMoveState moveState { get; private set; }
    public FireBossBattleState battleState { get; private set; }
    public FireBossAttackState attackState { get; private set; }
    public FireBossStunnedState stunnedState { get; private set; }
    public FireBossDeadState deadState { get; private set; }

    #endregion
    protected override void Awake()
    {
        base.Awake();

        idleState = new FireBossIdleState(this, stateMachine, "Idle", this);
        moveState = new FireBossMoveState(this, stateMachine, "Walk", this);
        battleState = new FireBossBattleState(this, stateMachine, "Walk", this);
        attackState = new FireBossAttackState(this, stateMachine, "Cleave", this);
        deadState = new FireBossDeadState(this, stateMachine, "Die", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.T))
            stateMachine.ChangeState(stunnedState);
    }

    public override bool CanbeStunned()
    {
        if (base.CanbeStunned())
        {
            stateMachine.ChangeState(stunnedState);
            return true;
        }
        return false;
    }
    
    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }

    public override void TriggerHitAnimation()
    {
        anim.SetTrigger("TakeHit");
    }
}
