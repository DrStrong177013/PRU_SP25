using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerTileAffect : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    [SerializeField] private Tilemap tilemap;
    [SerializeField] private List<TileBase> dangerousTiles;
    [SerializeField] private float damagePerSecond = 10f;
    private bool isOnDangerTile = false;

    private void Update()
    {
        CheckTile();
        if (isOnDangerTile)
        {
            StepOn(damagePerSecond * Time.deltaTime);
        }
    }

    private void CheckTile()
    {
        Vector3Int playerTilePos = tilemap.WorldToCell(player.groundCheck.position);
        TileBase currentTile = tilemap.GetTile(playerTilePos);

        isOnDangerTile = dangerousTiles.Contains(currentTile);
    }

    private void StepOn(float amount)
    {
        player.stats.TakeDamage(Mathf.RoundToInt(amount));
    }
}
