using UnityEngine;

public class GameData : MonoBehaviour
{
    #region Constants

    public const string VERTICAL_AXIS = "Vertical";
    public const string HORIZONTAL_AXIS = "Horizontal";

    #endregion

    public static GameData Instance;

    public int currentScore = 0;
    public int currentRound = 0;

    public int hightScore;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void SaveData()
    {
        if (currentScore > hightScore)
        {
            PlayerPrefs.SetInt("Score", currentScore);
        }
    }
    public void LoadData()
    {
        hightScore = PlayerPrefs.GetInt("Score");
    }
}
