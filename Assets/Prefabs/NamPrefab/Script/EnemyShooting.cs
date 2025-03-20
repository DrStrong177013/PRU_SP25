
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private float shootingRange = 5f;         // Khoảng cách bắn tối đa
    [SerializeField] private float minShootingDistance = 5f;   // Khoảng cách tối thiểu để bắn
    [SerializeField] private float fireRate = 10f;             // Tốc độ bắn
    [SerializeField] private GameObject bulletPrefab;         // Prefab đạn
    [SerializeField] private Transform firePoint;             // Điểm bắn

    [Header("References")]
    [SerializeField] private Transform player;                // Player
    [SerializeField] private Animator animator;               // Animator của enemy

    private float nextFireTime = 0f;

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

        // Kiểm tra khoảng cách nằm trong khoảng cho phép bắn
        if (distanceToPlayer <= shootingRange &&
            distanceToPlayer >= minShootingDistance &&
            Time.time >= nextFireTime)
        {
            animator.SetTrigger("Attack"); // Kích hoạt animation attack
            nextFireTime = Time.time + 2f / fireRate;
        }
    }

    // Hàm gọi từ Animation Event
    public void Shoot()
    {
        if (firePoint == null)
        {
            Debug.LogError("FirePoint chưa được gán!");
            return;
        }

        // Tạo đạn
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        // Xác định hướng đạn chỉ dựa trên trục X
        float direction = Mathf.Sign(player.position.x - transform.position.x);
        bulletScript.SetDirection(direction);

        // Debug để kiểm tra
        Debug.Log($"Player X: {player.position.x:F2} | Enemy X: {transform.position.x:F2} | Direction: {direction}");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, minShootingDistance);
    }
}



