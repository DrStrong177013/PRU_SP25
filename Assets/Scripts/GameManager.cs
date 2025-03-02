using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player;
    public TextMeshPro text;
    public Canvas menu;
    public string message;
    public bool victory = false;
    private bool isPaused = false;
    private float outOfMapThreshold = -16f;
    public Enemy_FireBoss finalBoss;

    void Start()
    {
        if (menu != null)
            menu.gameObject.SetActive(false);
    }

    void Update()
    {
        InteractMenu();
        BossDie();
        EndGame();
        CheckPlayerOutOfMap();
    }

    public void BossDie()
    {
        if (finalBoss.GetComponent<EnemyStats>().currentHealth <= 0)
        {
            victory = true;
        }
    }

    public void WinGame(string message)
    {
        this.message = message;
        ShowCanvas();
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
            if (playerCurrentHealth <= 0)
            {
                ShowCanvas();
            }
        }
    }

    private void CheckPlayerOutOfMap()
    {
        if (player != null && player.transform.position.y < outOfMapThreshold)
        {
            ShowCanvas();
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
        if (menu != null)
        {
            TextMeshProUGUI textMesh = menu.GetComponentInChildren<TextMeshProUGUI>();
            if (textMesh != null)
            {
                textMesh.text = message;
            }
            menu.gameObject.SetActive(true);
        }
    }

    public void HideCanvas()
    {
        Time.timeScale = 1;
        isPaused = false;
        if (menu != null)
            menu.gameObject.SetActive(false);
    }
}