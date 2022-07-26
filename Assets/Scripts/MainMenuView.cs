using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hightScoreText;
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private Slider loadingSlider;

    private void Start()
    {
        GameData.Instance.LoadData();
        GameData.Instance.ChangeMusicVol();
        GameData.Instance.ChangeSoundsVol();
        hightScoreText.text = $"Hight score: {GameData.Instance.hightScore}";
    }

    public void OnPlay()
    {
        GameData.Instance.SaveData();
        loadingPanel.SetActive(true);
        StartCoroutine(LoaderAsync());
    }

    private IEnumerator LoaderAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        while (!operation.isDone)
        {
            loadingSlider.value = operation.progress;
            yield return null;
        }
    }

    public void OnQuit()
    {
        GameData.Instance.SaveData();
        Application.Quit();
    }
}
