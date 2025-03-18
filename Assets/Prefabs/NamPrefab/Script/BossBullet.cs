////using UnityEngine;

////public class BossBullet : MonoBehaviour
////{
////    [SerializeField] private float speed = 7f;           // Tốc độ đạn nhanh hơn
////    [SerializeField] private float lifetime = 5f;        // Thời gian sống ngắn hơn
////    [SerializeField] private int damage = 20;           // Sát thương lớn hơn
////    [SerializeField] private float knockbackForce = 5f; // Lực hất tung
////    [SerializeField] private float knockbackDuration = 0.3f; // Thời gian bị hất

////    private float direction = 1f;
////    private Rigidbody2D playerRb;

////    private void Start()
////    {
////        Destroy(gameObject, lifetime);
////    }

////    public void SetDirection(float dir)
////    {
////        direction = Mathf.Sign(dir);
////        float scaleSize = 4f; // Đạn của boss lớn hơn
////        transform.localScale = new Vector3(direction * scaleSize, scaleSize, scaleSize);
////    }

////    private void Update()
////    {
////        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
////    }

////    private void OnTriggerEnter2D(Collider2D collision)
////    {
////        if (collision.CompareTag("Player"))
////        {
////            CharacterStats playerHealth = collision.GetComponent<CharacterStats>();
////            playerRb = collision.GetComponent<Rigidbody2D>();

////            if (playerHealth != null)
////            {
////                playerHealth.TakeDamage(damage);
////                ApplyKnockback();
////            }
////            Destroy(gameObject);
////        }
////    }

////    private void ApplyKnockback()
////    {
////        if (playerRb != null)
////        {
////            // Tính toán vector lực hất tung
////            Vector2 knockbackDirection = (playerRb.transform.position - transform.position).normalized;
////            Vector2 knockback = knockbackDirection * knockbackForce;

////            // Áp dụng lực
////            playerRb.velocity = Vector2.zero; // Reset vận tốc hiện tại
////            playerRb.AddForce(knockback, ForceMode2D.Impulse);

////            // TODO: Nếu player có animation hoặc controller, có thể thêm trạng thái bị knockback
////        }
////    }
////}





//using UnityEngine;

//public class BossBullet : MonoBehaviour
//{
//    [SerializeField] private float speed = 7f;           // Tốc độ đạn
//    [SerializeField] private float lifetime = 5f;        // Thời gian sống
//    [SerializeField] private int damage = 20;           // Sát thương
//    [SerializeField] private float knockbackForceX = 6f; // Lực hất ngang
//    [SerializeField] private float knockbackForceY = 4f; // Lực hất dọc (bật lên)
//    [SerializeField] private float knockbackDuration = 0.3f; // Thời gian bị hất

//    private float direction = 1f;
//    private Rigidbody2D playerRb;

//    private void Start()
//    {
//        Destroy(gameObject, lifetime);
//    }

//    public void SetDirection(float dir)
//    {
//        direction = Mathf.Sign(dir);
//        float scaleSize = 4f;
//        transform.localScale = new Vector3(direction * scaleSize, scaleSize, scaleSize);
//    }

//    private void Update()
//    {
//        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.CompareTag("Player"))
//        {
//            CharacterStats playerHealth = collision.GetComponent<CharacterStats>();
//            playerRb = collision.GetComponent<Rigidbody2D>();

//            if (playerHealth != null)
//            {
//                playerHealth.TakeDamage(damage);
//                ApplyKnockback();
//            }
//            Destroy(gameObject);
//        }
//    }

//    private void ApplyKnockback()
//    {
//        if (playerRb != null)
//        {

//            float knockbackDirX = Mathf.Sign(playerRb.transform.position.x - transform.position.x);
//            Vector2 knockback = new Vector2(knockbackDirX * knockbackForceX, knockbackForceY);


//            playerRb.linearVelocity = Vector2.zero;
//            playerRb.AddForce(knockback, ForceMode2D.Impulse);


//        }
//    }
//}


using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [SerializeField] private float speed = 7f;           // Tốc độ đạn
    [SerializeField] private float lifetime = 5f;        // Thời gian sống
    [SerializeField] private int damage = 20;           // Sát thương
    [SerializeField] private float knockbackForceX = 6f; // Lực hất ngang
    [SerializeField] private float knockbackForceY = 4f; // Lực hất dọc (bật lên)
    [SerializeField] private float knockbackDuration = 0.3f; // Thời gian bị hất

    private Vector2 direction;
    private Rigidbody2D playerRb;

    private void Start()
    {
        // Tìm player và tính hướng
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            direction = (player.transform.position - transform.position).normalized;
        }
        else
        {
            Debug.LogWarning("Không tìm thấy Player!");
            direction = Vector2.right; // Hướng mặc định nếu không tìm thấy player
        }

        // Cố định rotation để mũi hướng xuống (giả định sprite gốc mũi hướng lên)
        transform.rotation = Quaternion.Euler(0, 0, 180f);

        // Đặt scale cố định
        float scaleSize = 4f;
        transform.localScale = new Vector3(scaleSize, scaleSize, scaleSize);

        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Di chuyển theo hướng player
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
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