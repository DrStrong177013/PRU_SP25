using UnityEngine;

public class CheckpointStatueBehavior : MonoBehaviour
{
    public float healInterval = 1f;
    public int healAmount = 10;
    public Animator animator;
    private float nextHealTime = 0f;
    private Vector2 checkpointPosition;

    [SerializeField] public GameManager gameManager;

    private void Start()
    {
        checkpointPosition = transform.position;
    }

    private void Update()
    {
        SavePoint();
        HealPlayer();
    }

    private void HealPlayer()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, 2f, LayerMask.GetMask("Player"));
        if (player != null)
        {
            animator.SetBool("Active", true);
            if (Time.time >= nextHealTime)
            {
                PlayerStats playerStats = player.GetComponent<PlayerStats>();
                if (playerStats.currentHealth < playerStats.maxHealth.GetValue())
                {
                    playerStats.currentHealth += healAmount;
                }
                nextHealTime = Time.time + healInterval;
            }
        }
    }

    private void SavePoint()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, 2f, LayerMask.GetMask("Player"));
        if (player != null)
        {
            gameManager.SetCheckpoint(checkpointPosition);
        }
    }
}