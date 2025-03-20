using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    public int damage = 10; 
    public string targetTag = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra xem object va chạm có tag "Player" không
        if (collision.CompareTag(targetTag))
        {
            CharacterStats characterStats = collision.GetComponent<CharacterStats>();
            if (characterStats != null)
            {
                characterStats.TakeDamage(damage);
                Debug.Log("Player bị dính bẫy! Gây " + damage + " sát thương.");
            }
        }
    }
}
