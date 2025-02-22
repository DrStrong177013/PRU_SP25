using System.Collections;
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
        if (player != null)
        {
            int playerCurrentHealth = player.GetComponent<PlayerStats>().currentHealth;

            // TODO Slow load for player die animation
            if (playerCurrentHealth <= 0)
            {
                ShowCanvas();
            }
        }
    }

    public void RestartGame()
    {
        if (player != null)
        {
            Time.timeScale = 1;
            player.GetComponent<PlayerStats>().currentHealth = player.GetComponent<PlayerStats>().GetMaxHealthValue();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
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