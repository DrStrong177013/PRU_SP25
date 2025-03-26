using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lifetime = 5f;
    [SerializeField] private int damage = 10;
    private float direction = 1f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void SetDirection(float dir)
    {
        direction = Mathf.Sign(dir);
        float scaleSize = 3f;
        transform.localScale = new Vector3(direction * scaleSize, scaleSize, scaleSize);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CharacterStats playerHealth = collision.GetComponent<CharacterStats>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}