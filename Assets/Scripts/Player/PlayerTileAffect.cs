using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerTileAffect : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    [SerializeField] private Tilemap tilemap;
    [SerializeField] private List<TileBase> dangerousTiles;
    [SerializeField] private float damagePerSecond = 10f;
    [SerializeField] private float damageCooldown = 1f;
    private bool isOnDangerTile = false;
    private float damageTimer = 0f;

    private void Update()
    {
        CheckTile();
        if (isOnDangerTile && damageTimer <= 0f)
        {
            StepOn(damagePerSecond);
            damageTimer = damageCooldown;
        }

        if (damageTimer > 0f)
        {
            damageTimer -= Time.deltaTime;
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