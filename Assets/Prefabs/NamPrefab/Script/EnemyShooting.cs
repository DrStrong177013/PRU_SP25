using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private float shootingRange = 5f;
    [SerializeField] private float minShootingDistance = 5f;
    [SerializeField] private float fireRate = 10f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Animator animator;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= shootingRange && distanceToPlayer >= minShootingDistance)
        {
            animator.SetTrigger("Attack");
        }
    }

    public void Shoot()
    {
        if (firePoint == null)
        {
            Debug.LogError("FirePoint chưa được gán!");
            return;
        }
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        float direction = Mathf.Sign(player.position.x - transform.position.x);
        bulletScript.SetDirection(direction);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, minShootingDistance);
    }
}