using UnityEngine;

public class ItemSpawnerDame : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CharacterStats characterStats = collision.GetComponent<CharacterStats>();
            if (characterStats != null)
            {
                characterStats.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}