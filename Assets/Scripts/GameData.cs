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
    [SerializeField] private List<AudioSource> sounds;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text waveNumText;
    [SerializeField] private TMP_Text timeToNextWaveText;

    public static GameData Instance;

    private int currentScore = 0;
    private int currentWave = 0;

    private float musicVol = 1;
    private float soundsVol = 1;

    private int hightScore;

    public int CurrentScore => currentScore;
    public int CurrentWave => currentWave;
    public List<AudioSource> Sounds => sounds;
    public float MusicVol => musicVol;
    public float SoundsVol => soundsVol;
    public int HightScore => hightScore;
    public TMP_Text ScoreText => scoreText;
    public TMP_Text WaveNumText => waveNumText;
    public TMP_Text TimeToNextWaveText => timeToNextWaveText;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void IncreaseScore(int additive)
    {
        currentScore += additive;
    }
    public void IncreaseWave()
    {
        currentWave++;
    }
    public void ChangeMusicVol(float newValue)
    {
        musicVol = newValue;
    }
    public void ChangeSoundsVol(float newValue)
    {
        soundsVol = newValue;
    }

    public void SaveData()
    {
        if (currentScore > hightScore)
        {
            hightScore = currentScore;
        }
        PlayerPrefs.SetInt("Score", hightScore);
        PlayerPrefs.SetFloat("MusicVolume", musicVol);
        PlayerPrefs.SetFloat("SoundsVolume", soundsVol);
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
