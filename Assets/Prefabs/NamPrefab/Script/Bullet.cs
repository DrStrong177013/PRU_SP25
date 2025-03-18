
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lifetime = 10f;
    [SerializeField] private int damage = 10;
    private float direction = 1f; // 1f: sang phải, -1f: sang trái

    private void Start()
    {
        Destroy(gameObject, lifetime); // Tự hủy sau lifetime
    }

    // Hàm để thiết lập hướng di chuyển của đạn
    public void SetDirection(float dir)
    {
        direction = Mathf.Sign(dir); // Lấy dấu (1 hoặc -1)

        float scaleSize = 3f; // Kích thước đồng đều để hình tròn
        transform.localScale = new Vector3(direction * scaleSize, scaleSize, scaleSize);
    }


    private void Update()
    {
        // Chỉ di chuyển theo trục X
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




