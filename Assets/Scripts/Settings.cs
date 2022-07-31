using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundsSlider;

    private void OnEnable()
    {
        musicSlider.value = GameData.Instance.MusicVol;
        soundsSlider.value = GameData.Instance.SoundsVol;
    }
    
    private void Update()
    {
        if(GameData.Instance.MusicVol != musicSlider.value)
        {
            GameData.Instance.ChangeMusicVol(musicSlider.value);
            GameData.Instance.ChangeMusicVol();
        }
        if (GameData.Instance.SoundsVol != soundsSlider.value)
        {
            GameData.Instance.ChangeSoundsVol(soundsSlider.value);
            GameData.Instance.ChangeSoundsVol();
        }

    }
}
