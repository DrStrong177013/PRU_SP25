using UnityEngine;

public class ItemSpawnerDame : MonoBehaviour
{
    public int damage = 10; // Sát thương gây ra

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CharacterStats characterStats = collision.GetComponent<CharacterStats>();
            if (characterStats != null)
            {
                characterStats.TakeDamage(damage);
            }
            Destroy(gameObject); // Xóa vật phẩm sau khi gây damage
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject); // Xóa vật phẩm khi ra khỏi màn hình
    }
}
