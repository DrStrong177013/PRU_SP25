using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> itemPrefabs;
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private float spawnRangeX = 8f;

    void Start()
    {
        StartCoroutine(SpawnItems());
    }

    IEnumerator SpawnItems()
    {
        while (true)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(-spawnRangeX, spawnRangeX), transform.position.y);
            int randomIndex = Random.Range(0, itemPrefabs.Count);
            Instantiate(itemPrefabs[randomIndex], spawnPosition, transform.rotation);
            yield return new WaitForSeconds(spawnRate);
        }
    }
}