using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player;
    public TextMeshProUGUI text;
    public Canvas menu;
    public GameObject submenu;
    public string message;
    public bool victory = false;
    private bool isPaused = false;
    private float outOfMapThreshold = -16f;
    public Vector2 checkpointPosition;
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
        PlayerDie();
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

    public void PlayerDie()
    {
        if (player != null)
        {
            PlayerStats playerStats = player.GetComponent<PlayerStats>();
            if (playerStats.currentHealth <= 0)
            {
                if (checkpointPosition != Vector2.zero)
                {
                    player.transform.position = checkpointPosition;
                    playerStats.currentHealth = playerStats.GetMaxHealthValue();
                    player.stateMachine.ChangeState(player.idleState);
                }
                else
                {
                    message = "Too much damgage? \n Maybe better luck next time";
                    ShowCanvas();
                }
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

    public void RoundsMenu()
    {
        submenu.gameObject.SetActive(true);
    }

    public void ShowCanvas()
    {
        Time.timeScale = 0;
        isPaused = true;
        if (menu != null)
        {
            if (text != null)
            {
                text.text = message;
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

    internal void SetCheckpoint(Vector2 checkpointPosition)
    {
        this.checkpointPosition = checkpointPosition;
    }
}