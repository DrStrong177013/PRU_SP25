using UnityEngine;

public class ObjectDamage : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().stats.TakeDamage(Mathf.RoundToInt(damage));
        }
    }
}
