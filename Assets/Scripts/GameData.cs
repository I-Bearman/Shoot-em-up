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

    public float musicVol = 1;
    public float soundsVol = 1;

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
            PlayerPrefs.SetFloat("MusicVolume", musicVol);
            PlayerPrefs.SetFloat("SoundsVolume", soundsVol);
        }
    }
    public void LoadData()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            hightScore = PlayerPrefs.GetInt("Score");
            musicVol = PlayerPrefs.GetFloat("MusicVolume");
            soundsVol = PlayerPrefs.GetFloat("SoundsVolume");
        }
    }
}
