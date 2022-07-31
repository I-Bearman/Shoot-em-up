using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class QuickMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private TMP_Text deathScoreText;
    [SerializeField] private GameObject newHightText;
    public static QuickMenu Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Time.timeScale = 1;
    }
    private void Start()
    {
        GameData.Instance.LoadData();
        GameData.Instance.ChangeMusicVol();
        GameData.Instance.ChangeSoundsVol();
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

    public void ReloadLevel()
    {
        loadingPanel.SetActive(true);
        StartCoroutine(LoaderAsync());
    }

    public IEnumerator LoaderAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        while (!operation.isDone)
        {
            loadingSlider.value = operation.progress;
            yield return null;
        }
    }

    public void DeathScore()
    {
        deathScoreText.text = GameData.Instance.ScoreText.text;
        if (GameData.Instance.CurrentScore > GameData.Instance.HightScore)
        {
            newHightText.SetActive(true);
        }
    }
}
