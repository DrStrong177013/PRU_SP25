

using UnityEngine;

public class BossShooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private float shootingRange = 8f;
    [SerializeField] private float minShootingDistance = 3f;
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private GameObject bossBulletPrefab;
    [SerializeField] private GameObject bigBulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private int shotsBeforeBigBullet = 5;

    [Header("Spawner Settings")]
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private float spawnActivationRange = 2f;
    [SerializeField] private float spawnCooldown = 5f; // Thời gian chờ giữa các lần spawn enemy

    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Animator animator;

    private float nextFireTime = 0f;
    private float nextSpawnTime = 0f; // Thời gian để spawn enemy tiếp theo
    private int shootCount = 0;

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
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Debug khoảng cách
        Debug.Log($"Distance to player: {distanceToPlayer:F2} | Spawn Range: {spawnActivationRange}");

        // Spawn enemy khi player ở gần boss và đã qua thời gian cooldown
        if (distanceToPlayer <= spawnActivationRange && Time.time >= nextSpawnTime)
        {
            if (spawner != null)
            {
                spawner.SpawnEnemy(); // Gọi hàm spawn trực tiếp
                nextSpawnTime = Time.time + spawnCooldown; // Đặt lại thời gian spawn tiếp theo
                Debug.Log("Boss triggers enemy spawn!");
            }
            else
            {
                Debug.LogWarning("EnemySpawner chưa được gán vào BossShooting!");
            }
        }

        // Logic bắn đạn
        if (distanceToPlayer <= shootingRange &&
            distanceToPlayer >= minShootingDistance &&
            Time.time >= nextFireTime)
        {
            animator.SetTrigger("Attack");
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    public void Shoot()
    {
        if (firePoint == null)
        {
            Debug.LogError("FirePoint chưa được gán!");
            return;
        }

        shootCount++;
        GameObject bulletPrefabToUse;

        if (shootCount >= shotsBeforeBigBullet)
        {
            bulletPrefabToUse = bigBulletPrefab;
            shootCount = 0;
            Debug.Log("Boss shoots a BIG bullet!");
        }
        else
        {
            bulletPrefabToUse = bossBulletPrefab;
            Debug.Log($"Boss shoots normal bullet. Shot count: {shootCount}");
        }

        if (bulletPrefabToUse != null)
        {
            GameObject bullet = Instantiate(bulletPrefabToUse, firePoint.position, Quaternion.identity, null);
        }
        else
        {
            Debug.LogError("Bullet prefab chưa được gán!");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, minShootingDistance);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, spawnActivationRange);
    }
}