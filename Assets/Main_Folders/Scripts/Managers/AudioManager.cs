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
        StartAudioSetup();
        PlayMusic("Soundtrack");
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

    public void ToggleMusic(int value)
    {
        if (value == 0) MusicSource.mute = true;
        else if (value == 1) MusicSource.mute = false;
    }

    public void ToggleSFX(int value)
    {
        if (value == 0) SfxSource.mute = true;
        else if (value == 1) SfxSource.mute = false;
    }

    public void MusicVolume(float volume)
    {
        MusicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        SfxSource.volume = volume;
    }

    private void StartAudioSetup()
    {
        // Music Toggle
        if (PlayerPrefs.HasKey("musicToggleValue"))
            ToggleMusic(PlayerPrefs.GetInt("musicToggleValue"));
        else
        {
            PlayerPrefs.SetInt("musicToggleValue", 0);
            ToggleMusic(PlayerPrefs.GetInt("musicToggleValue"));
        }

        // Sfx Toggle
        if (PlayerPrefs.HasKey("sfxToggleValue"))
            ToggleSFX(PlayerPrefs.GetInt("sfxToggleValue"));
        else
        {
            PlayerPrefs.SetInt("sfxToggleValue", 0);
            ToggleSFX(PlayerPrefs.GetInt("sfxToggleValue"));
        }

        // Music Volume
        if (PlayerPrefs.HasKey("MusicVolumeValue"))
            MusicVolume(PlayerPrefs.GetFloat("MusicVolumeValue"));
        else
        {
            PlayerPrefs.GetFloat("MusicVolumeValue", 0.5f);
            MusicVolume(PlayerPrefs.GetFloat("MusicVolumeValue"));
        }

        // Sfx Volume
        if (PlayerPrefs.HasKey("SfxVolumeValue"))
            SFXVolume(PlayerPrefs.GetFloat("SfxVolumeValue"));
        else
        {
            PlayerPrefs.GetFloat("SfxVolumeValue", 0.5f);
            SFXVolume(PlayerPrefs.GetFloat("SfxVolumeValue"));
        }
    }
}