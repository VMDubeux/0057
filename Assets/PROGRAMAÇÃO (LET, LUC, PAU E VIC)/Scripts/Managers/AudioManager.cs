using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Main_Folders.Scripts.Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [FormerlySerializedAs("MusicSounds")] [Header("AudioClips:")]
        public Sound[] musicSounds;

        [FormerlySerializedAs("SfxSounds")] [Header("AudioClips:")]
        public Sound[] sfxSounds;

        [FormerlySerializedAs("MusicSource")] [Header("Audio Sources:")]
        public AudioSource musicSource;

        [FormerlySerializedAs("SfxSource")] [Header("Audio Sources:")]
        public AudioSource sfxSource;

        public const float MusicParam = 0.5f;
        
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
            PlayMusic("Soundtrack", MusicParam);
        }


        public void PlayMusic(string Name, float Volume)
        {
            Sound s = Array.Find(musicSounds, x => x.Name == Name);
            if (s == null)
            {
                Debug.Log("Sound Not Found");
            }
            else
            {
                musicSource.clip = s.Clip;
                musicSource.Play();
                musicSource.volume = Volume;
            }
        }

        public void PlaySfx(string Name, float Volume)
        {
            Sound s = Array.Find(sfxSounds, s => s.Name == Name);

            sfxSource.clip = s.Clip;
            sfxSource.PlayOneShot(s.Clip, Volume);
        }

        public void ToggleMusic(int value)
        {
            if (value == 0) musicSource.mute = true;
            else if (value == 1) musicSource.mute = false;
        }

        public void ToggleSfx(int value)
        {
            if (value == 0) sfxSource.mute = true;
            else if (value == 1) sfxSource.mute = false;
        }

        public void MusicVolume(float volume)
        {
            musicSource.volume = volume;
        }

        public void SfxVolume(float volume)
        {
            sfxSource.volume = volume;
        }
    }
}