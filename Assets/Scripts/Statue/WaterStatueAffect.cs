using UnityEngine;

public class WaterStatueAffect : MonoBehaviour
{
    public Animator animator;
    public int iceDamageGive = 50;
    public int health = 200;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStats playerStats = collision.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                GivePlayerBuff(playerStats);
                animator.SetBool("Active", true);
            }
        }
    }

    private void GivePlayerBuff(PlayerStats playerStats)
    {
        playerStats.iceDamage.SetDefaultValue(iceDamageGive);
        playerStats.maxHealth.SetDefaultValue(playerStats.maxHealth.GetValue() + health);
        playerStats.currentHealth += health;
    }
}
