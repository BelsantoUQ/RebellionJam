using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AudioPlayer : MonoBehaviour
{
    private AudioSource audiosource;
    public AudioClip[] Music;
    public int index = 0;
    public TextMeshProUGUI curentmusic;
    public GameObject prevarrow;
    public GameObject nextarrow;
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        audiosource.clip = Music[index];
        curentmusic.text = audiosource.clip.name;
        audiosource.Play();
    }

    void Update()
    {
        if (index == 0)
        {
            prevarrow.SetActive(false);
        }else if (index == 9)
        {
            nextarrow.SetActive(false);
        }
        else
        {
            prevarrow.SetActive(true);
            nextarrow.SetActive(true);
        }
    }

    public void NextSong()
    {
        audiosource.Stop();
        index++;
        audiosource.clip = Music[index];
        curentmusic.text = audiosource.clip.name;
        audiosource.Play();

    }

    public void PrevSong()
    {
        audiosource.Stop();
        index--;
        audiosource.clip = Music[index];
        curentmusic.text = audiosource.clip.name;
        audiosource.Play();
    }
}
