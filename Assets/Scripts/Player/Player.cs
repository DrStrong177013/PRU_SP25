using System.Collections;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    [Header("Attack details")]
    public Vector2[] attackMovement;
    public float counterAttackDuration = .2f;

    public bool isBusy { get; private set; }

    [Header("Move info")]
    [SerializeField] private float waterSlowFactor = 0.5f;
    private float defaultMoveSpeed;
    public float moveSpeed = 4f;
    public float jumpForce;
    public ParticleSystem dust;

    [Header("Dash info")]
    [SerializeField] private float dashCoolDown;
    private float dashUsageTimer;
    public float dashSpeed;
    public float dashDuration;
    public float dashDir { get; private set; }

    #region States
    public PlayerStateMachine stateMachine
    { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerWallSlideState wallSlide { get; private set; }
    public PlayerWallJumpState wallJump { get; private set; }
    public PlayerDashState dashState { get; private set; }

    public PlayerPrimaryAttackState primaryAttack { get; private set; }
    public PlayerCounterAttackState counterAttack { get; private set; }

    public PlayerDeathState deadState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlide = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJump = new PlayerWallJumpState(this, stateMachine, "Jump");

        primaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        counterAttack = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
        deadState = new PlayerDeathState(this, stateMachine, "Die");
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
        defaultMoveSpeed = moveSpeed;
    }


    protected override void Update()
    {
        base.Update();
        CheckForDashInput();
        HandleWaterInteraction();
        stateMachine.currentState.Update();
    }

    public IEnumerator BusyFor(float _secounds)
    {
        isBusy = true;
        yield return new WaitForSeconds(_secounds);
        isBusy = false;
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    private void CheckForDashInput()
    {
        if (IsWallDetected())
            return;

        dashUsageTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer < 0)
        {
            dashUsageTimer = dashCoolDown;
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0)
            {
                dashDir = facingDir;
            }
            stateMachine.ChangeState(dashState);

        }
    }

    private void HandleWaterInteraction()
    {
        if (IsInWater())
        {
            moveSpeed = defaultMoveSpeed * waterSlowFactor;
        }
        else
        {
            moveSpeed = defaultMoveSpeed;
        }
    }

    private bool IsInWater()
    {
        return Physics2D.OverlapBox(transform.position, new Vector2(1f, 1f), 0, LayerMask.GetMask("Water")) != null;
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }
}