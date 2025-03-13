using UnityEngine;

public class Enemy_BoneWizard : Enemy
{
    [Header("Teleport details")]
    [SerializeField] private BoxCollider2D arena;
    [SerializeField] private Vector2 surroundingCheckSize;
    #region States
    public BoneWizardBattleState battleState { get; private set; }
    public BoneWizardAttackState attackState { get; private set; }
    public BoneWizardIdleState idleState { get; private set; }
    public BoneWizardDeadState deadState { get; private set; }
    public BoneWizardSpellCastState spellCastState { get; private set; }
    public BoneWizardTeleportState teleportState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();
        idleState = new BoneWizardIdleState(this, stateMachine, "Idle", this);
        battleState = new BoneWizardBattleState(this, stateMachine, "Move", this);
        attackState = new BoneWizardAttackState(this, stateMachine, "Attack", this);
        deadState = new BoneWizardDeadState(this, stateMachine, "Idle", this);
        spellCastState = new BoneWizardSpellCastState(this, stateMachine, "SpellCast", this);
        teleportState = new BoneWizardTeleportState(this, stateMachine, "Teleport", this);

    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }
    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }
    public void FindPosition()
    {
        float x = Random.Range(arena.bounds.min.x + 3, arena.bounds.max.x - 3);
        float y = Random.Range(arena.bounds.min.y + 3, arena.bounds.max.y - 3);

        transform.position = new Vector3(x, y);
        transform.position = new Vector3(transform.position.x, transform.position.y - GroundBelow().distance + (cd.size.y / 2));

        if (!GroundBelow() || SomethingIsAround())
        {
            //Debug.Log("Looking for new position");
            FindPosition();
        }
    }
    private RaycastHit2D GroundBelow() => Physics2D.Raycast(transform.position, Vector2.down, 100, whatIsGround);
    private bool SomethingIsAround() => Physics2D.BoxCast(transform.position, surroundingCheckSize, 0, Vector2.zero, 0, whatIsGround);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - GroundBelow().distance));
        Gizmos.DrawWireCube(transform.position, surroundingCheckSize);
    }
}
