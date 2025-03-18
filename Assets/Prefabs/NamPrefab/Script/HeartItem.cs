using UnityEngine;

public class HeartItem : MonoBehaviour
{
    [SerializeField] private int healAmount = 10;
    [SerializeField] private GameObject healEffect; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterStats playerStats = collision.GetComponent<CharacterStats>();

        if (playerStats != null)
        {
            playerStats.Heal(healAmount); 

           
            if (healEffect != null)
            {
                Instantiate(healEffect, playerStats.transform.position, Quaternion.identity);
            }

            Destroy(gameObject); 
        }
    }
}
