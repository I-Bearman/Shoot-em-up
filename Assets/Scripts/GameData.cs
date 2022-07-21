using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GameData : MonoBehaviour
{
    #region Constants

    public const string VERTICAL_AXIS = "Vertical";
    public const string HORIZONTAL_AXIS = "Horizontal";

    #endregion

    [SerializeField] private AudioSource music;
    public List<AudioSource> sounds;

    public static GameData Instance;

    public int currentScore = 0;
    public int currentWave = 0;

    public float musicVol = 1;
    public float soundsVol = 1;

    public int hightScore;

    public TMP_Text scoreText;
    public TMP_Text waveNumText;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        ChangeMusicVol();
        ChangeSoundsVol();
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

    public void ChangeMusicVol()
    {
        music.volume = musicVol;
    }
    public void ChangeSoundsVol()
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            sounds[i].volume = soundsVol;
        }
    }
}
