using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hightScoreText;

    private void Start()
    {
        GameData.Instance.LoadData();
        hightScoreText.text = $"Hight score: {GameData.Instance.hightScore}";
    }

    public void OnPlay()
    {
        SceneManager.LoadScene(1);
    }

    public void OnQuit()
    {
        GameData.Instance.SaveData();
        Application.Quit();
    }
}
