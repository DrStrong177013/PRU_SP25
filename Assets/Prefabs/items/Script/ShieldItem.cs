using UnityEngine;

public class ShieldItem : MonoBehaviour
{
    [SerializeField] private int armorBonus = 10; // Lượng giáp tăng
    [SerializeField] private float duration = 15f; // Thời gian hiệu lực
    [SerializeField] private GameObject effectPrefab; // Prefab hiệu ứng tăng giáp

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterStats playerStats = collision.GetComponent<CharacterStats>();

        if (playerStats != null)
        {
            playerStats.AddTemporaryArmor(armorBonus, duration); // Tăng giáp tạm thời

            // Hiển thị hiệu ứng tăng giáp (nếu có Prefab)
            if (effectPrefab != null)
            {
                Instantiate(effectPrefab, playerStats.transform.position, Quaternion.identity);
            }

            Destroy(gameObject); // Xóa vật phẩm sau khi nhặt
        }
    }
}
