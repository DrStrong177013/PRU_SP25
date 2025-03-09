using UnityEngine;

public class DameItem : MonoBehaviour
{
    [SerializeField] private int bonusDamage = 10; // Sát thương cộng thêm khi nhặt item
    [SerializeField] private GameObject effectPrefab; // Prefab hiệu ứng tăng sát thương

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterStats playerStats = collision.GetComponent<CharacterStats>();

        if (playerStats != null)
        {
            playerStats.AddBonusDamage(bonusDamage); // Tăng sát thương cho player

            // Hiển thị hiệu ứng tăng sát thương (nếu có Prefab)
            if (effectPrefab != null)
            {
                Instantiate(effectPrefab, playerStats.transform.position, Quaternion.identity);
            }

            Destroy(gameObject); // Xóa vật phẩm sau khi nhặt
        }
    }
}
