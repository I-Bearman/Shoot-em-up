using UnityEngine;
using UnityEngine.SceneManagement;

public class QuickMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    public static QuickMenu Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Time.timeScale = 1;
    }

    public void OnPause()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
    public void OffPause()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void ToMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void OnQuit()
    {
        GameData.Instance.SaveData();
        Application.Quit();
    }

}
