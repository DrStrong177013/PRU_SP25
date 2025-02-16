using UnityEngine;

public class Enemy_Bandit : Enemy
{

    #region States

    public BanditIdleState idleState { get; private set; }
    public BanditMoveState moveState { get; private set; }
    public BanditBattleState battleState { get; private set; }
    public BanditAttackState attackState { get; private set; }
    public BanditStunnedState stunnedState { get; private set; }
    public BanditDeadState deadState { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();

        idleState = new BanditIdleState(this, stateMachine, "Idle", this);
        moveState = new BanditMoveState(this, stateMachine, "Move", this);
        battleState = new BanditBattleState(this, stateMachine, "Move", this);
        attackState = new BanditAttackState(this, stateMachine, "Attack", this);
        stunnedState = new BanditStunnedState(this, stateMachine, "Stunned", this);
        deadState = new BanditDeadState(this, stateMachine, "Idle", this);
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
}
