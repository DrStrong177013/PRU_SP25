using UnityEngine;

public class Enemy_Tentacle : Enemy
{
    [Header("Attack details")]
    public float pullDownForce = 5f;

    #region States

    public TentacleIdleState idleState { get; private set; }
    public TentacleAttackState attackState { get; private set; }
    public TentacleBattleState battleState { get; private set; }
    public TentacleMoveState moveState { get; private set; }

    #endregion
    protected override void Awake()
    {
        base.Awake();

        idleState = new TentacleIdleState(this, stateMachine, "Idle", this);
        attackState = new TentacleAttackState(this, stateMachine, "Attack", this);
        battleState = new TentacleBattleState(this, stateMachine, "Idle", this);
        moveState = new TentacleMoveState(this, stateMachine, "Idle", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }
}
