using UnityEngine;
using UnityEngine.SceneManagement; // Import thư viện quản lý Scene

public class EffectTrigger : MonoBehaviour
{
    [SerializeField] private string sceneName; // Tên màn hình cần chuyển

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Kiểm tra nếu Player chạm vào
        {
            SceneManager.LoadScene(sceneName); // Chuyển sang màn hình mới
        }
    }
}
