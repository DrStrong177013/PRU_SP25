using UnityEngine;

public class BossShooting : MonoBehaviour
{
    [SerializeField] private float shootingRange = 8f;
    [SerializeField] private float minShootingDistance = 3f;
    [SerializeField] private GameObject bossBulletPrefab;
    [SerializeField] private GameObject bigBulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private int shotsBeforeBigBullet = 5;

    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private float spawnActivationRange = 2f;
    [SerializeField] private float spawnCooldown = 15f;

    [SerializeField] private Transform player;
    [SerializeField] private Animator animator;

    private float nextSpawnTime = 0f;
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

        if (distanceToPlayer <= spawnActivationRange && Time.time >= nextSpawnTime)
        {
            if (spawner != null)
            {
                spawner.SpawnEnemy();
                nextSpawnTime = Time.time + spawnCooldown;
            }
            else
            {
                Debug.LogWarning("EnemySpawner chưa được gán vào BossShooting!");
            }
        }

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
            Instantiate(bulletPrefabToUse, firePoint.position, Quaternion.identity, null);
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