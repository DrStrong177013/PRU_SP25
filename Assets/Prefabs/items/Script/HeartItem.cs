using UnityEngine;

public class HeartItem : MonoBehaviour
{
    [SerializeField] private int healAmount = 10; // Lượng máu hồi
    [SerializeField] private GameObject healEffect; // Prefab hiệu ứng hồi máu

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterStats playerStats = collision.GetComponent<CharacterStats>();

        if (playerStats != null)
        {
            playerStats.Heal(healAmount); // Hồi máu cho player

            // Hiển thị hiệu ứng hồi máu (nếu có Prefab)
            if (healEffect != null)
            {
                Instantiate(healEffect, playerStats.transform.position, Quaternion.identity);
            }

            Destroy(gameObject); // Xóa vật phẩm sau khi nhặt
        }
    }
}
