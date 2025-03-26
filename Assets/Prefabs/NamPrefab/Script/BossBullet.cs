using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [SerializeField] private float speed = 7f;
    [SerializeField] private float lifetime = 5f;
    [SerializeField] private int damage = 20;
    [SerializeField] private float knockbackForceX = 6f;
    [SerializeField] private float knockbackForceY = 4f;

    private Rigidbody2D playerRb;
    private float directionX;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {

            directionX = Mathf.Sign(player.transform.position.x - transform.position.x);


            Vector3 scale = transform.localScale;
            if (directionX < 0)
            {
                transform.localScale = new Vector3(-Mathf.Abs(scale.x), scale.y, scale.z);
            }
            else
            {
                transform.localScale = new Vector3(Mathf.Abs(scale.x), scale.y, scale.z);
            }
        }
        else
        {
            directionX = 1f;
            Vector3 scale = transform.localScale;
            transform.localScale = new Vector3(Mathf.Abs(scale.x), scale.y, scale.z);
        }

        Destroy(gameObject, lifetime);
    }

    private void Update()
    {

        transform.Translate(new Vector2(directionX, 0) * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CharacterStats playerHealth = collision.GetComponent<CharacterStats>();
            playerRb = collision.GetComponent<Rigidbody2D>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                ApplyKnockback();
            }
            Destroy(gameObject);
        }
    }

    private void ApplyKnockback()
    {
        if (playerRb != null)
        {
            float knockbackDirX = Mathf.Sign(playerRb.transform.position.x - transform.position.x);
            Vector2 knockback = new Vector2(knockbackDirX * knockbackForceX, knockbackForceY);
            playerRb.linearVelocity = Vector2.zero;
            playerRb.AddForce(knockback, ForceMode2D.Impulse);
        }
    }
}