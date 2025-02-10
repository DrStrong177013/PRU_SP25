using UnityEngine;

public class Enemy_Bandit : Enemy
{

    #region States

    public BanditIdleState idleState { get; private set; }
    public BanditMoveState moveState { get; private set; }
    public BanditBattleState battleState { get; private set; }
    public BanditAttackState attackState { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();

        idleState = new BanditIdleState(this, stateMachine, "Idle", this);
        moveState = new BanditMoveState(this, stateMachine, "Move", this);
        battleState = new BanditBattleState(this, stateMachine, "Move", this);
        attackState = new BanditAttackState(this, stateMachine, "Attack", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }
}
