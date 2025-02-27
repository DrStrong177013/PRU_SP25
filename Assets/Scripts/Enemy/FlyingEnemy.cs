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
    }

    private void Flip()
    {
        direction *= -1;
        anim.transform.localScale = new Vector3(-anim.transform.localScale.x, anim.transform.localScale.y, anim.transform.localScale.z);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.value = health;

        if (health <= 0)
            Destroy(gameObject);
    }

    private bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * direction, wallCheckDistance);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
}