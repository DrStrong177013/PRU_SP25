using UnityEngine;

public class StatueBehavior : MonoBehaviour
{
    public float healInterval = 1f;
    public int healAmount = 10;
    public Animator animator;
    private float nextHealTime = 0f;

    [SerializeField] public GameManager gameManager;

    private void Update()
    {
        WinGame();
        SavePoint();
        HealPlayer();
    }

    private void HealPlayer()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, 2f, LayerMask.GetMask("Player"));
        if (player != null && Time.time >= nextHealTime)
        {
            PlayerStats playerStats = player.GetComponent<PlayerStats>();
            if (playerStats.currentHealth < playerStats.maxHealth.GetValue())
            {
                playerStats.currentHealth += healAmount;
            }
            nextHealTime = Time.time + healInterval;
        }
    }
    
    private void WinGame()
    {
        if (gameManager.victory)
        {
            animator.SetBool("Active", true);
        }
    }

    private void SavePoint()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, 2f, LayerMask.GetMask("Player"));
        if (gameManager.victory && player != null)
        {
            gameManager.WinGame("Victory");
        }
    }
}