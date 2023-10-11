using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider volumeSlider;

    private void Start()
    {
        volumeSlider.value = AudioManager.instance.backgroundMusic.volume;
    }

    public void OnVolumeSliderChanged()
    {
        AudioManager.instance.SetBackgroundMusicVolume(volumeSlider.value);
    }
}