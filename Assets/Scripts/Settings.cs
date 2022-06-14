using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundsSlider;
    private GameData gameData;

    private void Start()
    {
        musicSlider.value = gameData.musicVol;
        soundsSlider.value = gameData.soundsVol;
    }
    
    private void Update()
    {
        if(gameData.musicVol == musicSlider.value)
        {
            gameData.musicVol = musicSlider.value;
        }
        if (gameData.soundsVol == soundsSlider.value)
        {
            gameData.soundsVol = soundsSlider.value;
        }

    }
}
