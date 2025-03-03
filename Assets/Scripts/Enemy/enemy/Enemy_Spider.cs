using System.Collections;
using UnityEngine;

public class Enemy_Spider : MonoBehaviour
{
    public float grabDuration = 3f;
    public float damageInterval = 1f;
    public int damagePerSecond = 10;
    public float liftSpeed = 2f;
    public float holdPositionY = -11.77f; // Set the Y position to hold the player

    private bool isGrabbing = false;
    private Animator animator;
    private Rigidbody2D playerRb;
    private PlayerStats playerStats;
    private Collider2D currentPlayer;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isGrabbing && collision.CompareTag("Player"))
        {
            currentPlayer = collision;
            animator.SetTrigger("Grab");
        }
    }

    public void CheckForGrab()
    {
        if (currentPlayer != null && currentPlayer.CompareTag("Player"))
        {
            StartCoroutine(GrabPlayer(currentPlayer.gameObject));
        }
    }

    private IEnumerator GrabPlayer(GameObject player)
    {
        isGrabbing = true;
        playerRb = player.GetComponent<Rigidbody2D>();
        playerStats = player.GetComponent<PlayerStats>();
        playerRb.bodyType = RigidbodyType2D.Kinematic;

        if (playerRb != null)
        {
            playerRb.gravityScale = 0;
        }

        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.3f);

        Vector3 targetPosition = player.transform.position;
        targetPosition.y = holdPositionY; // Set the exact Y position

        while (Mathf.Abs(player.transform.position.y - holdPositionY) > 0.01f)
        {
            player.transform.position = Vector3.Lerp(player.transform.position, targetPosition, liftSpeed * Time.deltaTime);
            yield return null;
        }

        float damageTime = 0f;
        while (damageTime < grabDuration)
        {
            yield return new WaitForSeconds(damageInterval);
            playerStats?.TakeDamage(damagePerSecond);
            damageTime += damageInterval;
        }

        if (playerRb != null)
        {
            playerRb.gravityScale = 1;
        }

        isGrabbing = false;
        currentPlayer = null;

        playerRb.bodyType = RigidbodyType2D.Dynamic;
    }
}