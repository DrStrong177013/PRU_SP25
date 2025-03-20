//using UnityEngine;
//using System.Collections.Generic;

//public class EnemySpawner : MonoBehaviour
//{
//    [Header("Spawner Settings")]
//    public List<GameObject> enemyPrefabs; // Danh sách các prefab enemy
//    public float spawnInterval = 5f;      // Thời gian giữa các lần spawn (5 giây)

//    [Header("Activation Settings")]
//    public bool isActivated = false;      // Trạng thái kích hoạt spawner

//    private float timer;

//    void Start()
//    {
//        timer = spawnInterval; // Khởi tạo timer
//    }

//    void Update()
//    {
//        // Chỉ spawn khi spawner được kích hoạt
//        if (isActivated)
//        {
//            timer -= Time.deltaTime;

//            if (timer <= 0f)
//            {
//                SpawnEnemy();
//                timer = spawnInterval;
//            }
//        }
//    }

//    void SpawnEnemy()
//    {
//        if (enemyPrefabs.Count == 0) return;

//        int randomIndex = Random.Range(0, enemyPrefabs.Count);
//        GameObject enemyPrefab = enemyPrefabs[randomIndex];
//        Vector3 spawnPosition = transform.position;
//        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
//    }

//    // Hàm để kích hoạt spawner từ bên ngoài
//    public void ActivateSpawner()
//    {
//        isActivated = true;
//    }

//    // Hàm để tắt spawner (tùy chọn)
//    public void DeactivateSpawner()
//    {
//        isActivated = false;
//    }
//}




using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public List<GameObject> enemyPrefabs; // Danh sách các prefab enemy

    // Hàm spawn enemy được gọi từ BossShooting
    public void SpawnEnemy()
    {
        if (enemyPrefabs.Count == 0) return;

        int randomIndex = Random.Range(0, enemyPrefabs.Count);
        GameObject enemyPrefab = enemyPrefabs[randomIndex];
        Vector3 spawnPosition = transform.position;
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        Debug.Log("Enemy spawned at " + spawnPosition);
    }

    // Xóa ActivateSpawner và DeactivateSpawner vì không cần nữa
}