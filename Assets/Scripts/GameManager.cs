using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Canvas myCanvas;
    private bool isPaused = false;

    void Start()
    {
        if (myCanvas != null)
            myCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        InteractMenu();
        EndGame();
    }

    public void InteractMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                HideCanvas();
            else
                ShowCanvas();
        }
    }

    public void EndGame()
    {
        if (player.health <= 0)
        {
            ShowCanvas();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        player.health = player.maxHealth;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowCanvas()
    {
        Time.timeScale = 0;
        isPaused = true;
        if (myCanvas != null)
            myCanvas.gameObject.SetActive(true);
    }

    public void HideCanvas()
    {
        Time.timeScale = 1;
        isPaused = false;
        if (myCanvas != null)
            myCanvas.gameObject.SetActive(false);
    }
}