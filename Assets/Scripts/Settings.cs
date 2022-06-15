using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundsSlider;

    private void OnEnable()
    {
        musicSlider.value = GameData.Instance.musicVol;
        soundsSlider.value = GameData.Instance.soundsVol;
    }
    
    private void Update()
    {
        if(GameData.Instance.musicVol != musicSlider.value)
        {
            GameData.Instance.musicVol = musicSlider.value;
        }
        if (GameData.Instance.soundsVol != soundsSlider.value)
        {
            GameData.Instance.soundsVol = soundsSlider.value;
        }

    }
}
