using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> itemPrefabs; // Danh sách vật phẩm spawn
    [SerializeField] private float spawnRate = 2f; // Tốc độ spawn (có thể chỉnh trên Unity)
    [SerializeField] private float spawnRangeX = 8f; // Phạm vi spawn (có thể chỉnh trên Unity)

    void Start()
    {
        StartCoroutine(SpawnItems());
    }

    IEnumerator SpawnItems()
    {
        while (true)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(-spawnRangeX, spawnRangeX), transform.position.y);
            int randomIndex = Random.Range(0, itemPrefabs.Count); // Chọn vật phẩm ngẫu nhiên
            Instantiate(itemPrefabs[randomIndex], spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
