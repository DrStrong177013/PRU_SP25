using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

public class FireBossAffect : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase newTile;
    [SerializeField] private float effectDuration = 3f;

    private Enemy_FireBoss enemy => GetComponentInParent<Enemy_FireBoss>();
    private Dictionary<Vector3Int, Coroutine> activeTiles = new Dictionary<Vector3Int, Coroutine>();
    private Dictionary<Vector3Int, TileBase> originalTiles = new Dictionary<Vector3Int, TileBase>();

    private void Update()
    {
        ChangeTile();
    }

    private void ChangeTile()
    {
        Vector3Int tilePosition = tilemap.WorldToCell(enemy.groundCheck.position);
        TileBase currentTile = tilemap.GetTile(tilePosition);
        Debug.Log("Enemy:" + currentTile);

        if (currentTile != null && currentTile != newTile)
        {
            if (!originalTiles.ContainsKey(tilePosition))
            {
                originalTiles[tilePosition] = currentTile;
            }

            tilemap.SetTile(tilePosition, newTile);

            if (activeTiles.ContainsKey(tilePosition))
            {
                StopCoroutine(activeTiles[tilePosition]);
            }

            activeTiles[tilePosition] = StartCoroutine(ResetTileAfterTime(tilePosition));
        }
    }

    private IEnumerator ResetTileAfterTime(Vector3Int tilePosition)
    {
        yield return new WaitForSeconds(effectDuration);

        if (originalTiles.ContainsKey(tilePosition))
        {
            tilemap.SetTile(tilePosition, originalTiles[tilePosition]);
            activeTiles.Remove(tilePosition);
            originalTiles.Remove(tilePosition);
        }
    }
}