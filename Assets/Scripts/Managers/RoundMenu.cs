using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class RoundsMenu : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform contentParent;
    public string[] sceneNames;

    void Start()
    {
        PopulateRounds();
    }

    void PopulateRounds()
    {
        foreach (string sceneName in sceneNames)
        {
            GameObject newButton = Instantiate(buttonPrefab, contentParent);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = sceneName;
            newButton.GetComponent<Button>().onClick.AddListener(() => LoadScene(sceneName));
        }
    }

    void LoadScene(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }
}