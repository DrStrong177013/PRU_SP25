using UnityEngine;

public class DameItem : MonoBehaviour
{
    [SerializeField] private int bonusDamage = 10; 
    [SerializeField] private GameObject effectPrefab; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterStats playerStats = collision.GetComponent<CharacterStats>();

        if (playerStats != null)
        {
            playerStats.AddBonusDamage(bonusDamage); 

          
            if (effectPrefab != null)
            {
                Instantiate(effectPrefab, playerStats.transform.position, Quaternion.identity);
            }

            Destroy(gameObject); 
        }
    }
}
