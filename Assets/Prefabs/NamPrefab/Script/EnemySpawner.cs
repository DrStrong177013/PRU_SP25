using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public List<GameObject> enemyPrefabs;

    public void SpawnEnemy()
    {
        if (enemyPrefabs.Count == 0) return;

        int randomIndex = Random.Range(0, enemyPrefabs.Count);
        GameObject enemyPrefab = enemyPrefabs[randomIndex];
        Vector3 spawnPosition = transform.position;
        Instantiate(enemyPrefab, spawnPosition, transform.rotation);
        Debug.Log("Enemy spawned at " + spawnPosition);
    }
}