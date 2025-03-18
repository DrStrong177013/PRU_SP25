using UnityEngine;

public class ShieldItem : MonoBehaviour
{
    [SerializeField] private int armorBonus = 10; 
    [SerializeField] private float duration = 15f; 
    [SerializeField] private GameObject effectPrefab; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterStats playerStats = collision.GetComponent<CharacterStats>();

        if (playerStats != null)
        {
            playerStats.AddTemporaryArmor(armorBonus, duration); 

            if (effectPrefab != null)
            {
                Instantiate(effectPrefab, playerStats.transform.position, Quaternion.identity);
            }

            Destroy(gameObject); 
        }
    }
}
