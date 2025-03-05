using UnityEngine;
using System.Collections.Generic;

public class Enemy_Bandit : Enemy
{
    [Header("Attack details")]
    public float pureDamage = 10f;

    [Header("Drop Items Settings")]
    public List<GameObject> dropItems; // Danh sách vật phẩm có thể rơi
    public float dropChance = 0.5f; // Xác suất rơi vật phẩm (50%)

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
        DropItem(); // Gọi hàm để rơi vật phẩm
        stateMachine.ChangeState(deadState);
    }

    /// <summary>
    /// Xử lý rơi vật phẩm khi enemy chết
    /// </summary>
    private void DropItem()
    {
        if (dropItems.Count == 0) return; // Kiểm tra nếu danh sách rỗng thì không làm gì

        float randomValue = Random.value; // Giá trị random từ 0 - 1
        if (randomValue <= dropChance) // Nếu nhỏ hơn xác suất dropChance, rơi vật phẩm
        {
            int randomIndex = Random.Range(0, dropItems.Count); // Chọn một vật phẩm ngẫu nhiên
            GameObject itemToDrop = dropItems[randomIndex];

            Vector3 dropPosition = transform.position + Vector3.up * 1.5f; // Vị trí trên đầu Enemy
            Instantiate(itemToDrop, dropPosition, Quaternion.identity);
        }
    }
}
