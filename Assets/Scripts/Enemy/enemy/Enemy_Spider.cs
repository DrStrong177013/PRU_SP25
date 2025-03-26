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
    private bool isCollsion = false;
    private Animator animator;
    private Rigidbody2D playerRb;
    private PlayerStats playerStats;
    private Collider2D currentPlayer;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckForGrab();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isCollsion = true;
            currentPlayer = collision;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isCollsion = false;
            currentPlayer = null;
        }
    }

    public void CheckForGrab()
    {
        if (currentPlayer != null && isCollsion)
        {
            StartCoroutine(GrabPlayer(currentPlayer.gameObject));
        }
    }

    private IEnumerator GrabPlayer(GameObject player)
    {
        isGrabbing = true;
        playerRb = player.GetComponent<Rigidbody2D>();
        playerStats = player.GetComponent<PlayerStats>();

        if (playerRb != null)
        {
            playerRb.bodyType = RigidbodyType2D.Kinematic;
            playerRb.gravityScale = 0;
        }

        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.3f);

        Vector3 targetPosition = player.transform.position;
        targetPosition.y = holdPositionY;

        while (Mathf.Abs(player.transform.position.y - holdPositionY) > 0.01f)
        {
            if (!isCollsion) break;
            player.transform.position = Vector3.Lerp(player.transform.position, targetPosition, liftSpeed * Time.deltaTime);
            yield return null;
        }

        float damageTime = 0f;
        while (damageTime < grabDuration)
        {
            if (!isCollsion) break;
            yield return new WaitForSeconds(damageInterval);
            playerStats?.TakeDamage(damagePerSecond);
            damageTime += damageInterval;
        }

        if (playerRb != null)
        {
            playerRb.bodyType = RigidbodyType2D.Dynamic;
            playerRb.gravityScale = 1;
        }

        isGrabbing = false;
        currentPlayer = null;
    }
}