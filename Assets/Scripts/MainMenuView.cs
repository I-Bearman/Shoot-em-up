using UnityEngine;
using TMPro;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hightScoreText;

    private void OnEnable()
    {
        //GameData.Instance.LoadData();
    }

    public void OnQuit()
    {
        GameData.Instance.SaveData();
        Application.Quit();
    }
}
