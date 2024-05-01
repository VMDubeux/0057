using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    //public static AudioController Controller;

    [Header("AudioClips:")]
    public Sound[] MusicSounds, SfxSounds;

    [Header("Audio Sources:")]
    public AudioSource MusicSource, SfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        MusicVolume(PlayerPrefs.GetFloat("MusicVolumeValue"));
        PlayMusic("Theme");
        SFXVolume(PlayerPrefs.GetFloat("SfxVolumeValue"));
        ToggleMusic();
        ToggleSFX();
    }

    public void PlayMusic(string Name)
    {
        Sound s = Array.Find(MusicSounds, x => x.Name == Name);
        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            MusicSource.clip = s.Clip;
            MusicSource.Play();
        }
    }

    public void PlaySFX(string Name, float Volume)
    {
        Sound s = Array.Find(SfxSounds, s => s.Name == Name);

        SfxSource.clip = s.Clip;
        SfxSource.PlayOneShot(s.Clip, Volume);

    }

    public void ToggleMusic()
    {
        PlayerPrefs.GetInt("musicToggleValue");
    }

    public void ToggleSFX()
    {
        PlayerPrefs.GetInt("sfxToggleValue");
    }

    public void MusicVolume(float volume)
    {
        MusicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        SfxSource.volume = volume;
    }
}