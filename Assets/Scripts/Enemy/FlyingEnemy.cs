using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.UI;

public class FlyingEnemy : MonoBehaviour
{
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform attackCheck;
    [SerializeField] private Slider healthBar;
    public float wallCheckDistance;
    public float attackCheckRadius;
    public float moveSpeed = 2f;
    public float flightRange = 5f;
    public int damage = 10;

    private float startX;
    public int direction = -1;
    private float health;
    private float maxHealth = 50f;
    private bool hitWall = false;

    private void Start()
    {
        startX = transform.position.x;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        health = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
    }

    private void Update()
    {
        if (IsWallDetected() && !hitWall)
        {
            hitWall = true;
            Flip();
        }

        transform.position += Vector3.right * direction * moveSpeed * Time.deltaTime;

        if (Mathf.Abs(transform.position.x - startX) >= flightRange)
        {
            startX = transform.position.x;
            hitWall = false;
        }

        AttackTrigger();
    }

    private void Flip()
    {
        direction *= -1;
        anim.transform.localScale = new Vector3(-anim.transform.localScale.x, anim.transform.localScale.y, anim.transform.localScale.z);
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
                PlayerStats target = hit.GetComponent<PlayerStats>();
                DoDamage(target);
            }
        }
    }

    private void DoDamage(CharacterStats _targetStats)
    {
        int totalDamage = damage;
        totalDamage = CheckTargetArmor(_targetStats, totalDamage);
        _targetStats.TakeDamage(totalDamage);
    }

    private int CheckTargetArmor(CharacterStats _targetStats, int totalDamage)
    {
        if (_targetStats.isChilled)
            totalDamage -= Mathf.RoundToInt(_targetStats.armor.GetValue() * .8f);
        else
            totalDamage -= _targetStats.armor.GetValue();


        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        return totalDamage;
    }

    private bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * direction, wallCheckDistance);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
}