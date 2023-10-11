using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource backgroundMusic;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlayBackgroundMusic(AudioClip musicClip)
    {
        backgroundMusic.clip = musicClip;
        backgroundMusic.Play();
    }

    public void SetBackgroundMusicVolume(float volume)
    {
        backgroundMusic.volume = volume;
    }
}