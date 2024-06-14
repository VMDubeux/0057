using Main_Folders.Scripts.Managers;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Main_Folders.Scripts.Audio
{
    public class AudioControllerLevels : MonoBehaviour
    {
        public AudioMixer AudioMixer;
        private Toggle MusicToggle, SfxToggle;
        private Slider MusicSlider, SfxSlider;
        private AudioSource MusicSource, SfxSource;

        private void Awake()
        {
            MusicSlider = GameObject.Find("CanvasPause/Container/Music/SliderMusic").GetComponent<Slider>();
            MusicToggle = GameObject.Find("CanvasPause/Container/Music/MusicToggle").GetComponent<Toggle>();
            SfxSlider = GameObject.Find("CanvasPause/Container/SFX/SliderSFX").GetComponent<Slider>();
            SfxToggle = GameObject.Find("CanvasPause/Container/SFX/SfxToggle").GetComponent<Toggle>();
        }

        private void Start()
        {
            MusicSource = GameObject.FindGameObjectWithTag("MusicSource").GetComponent<AudioSource>();
            SfxSource = GameObject.FindGameObjectWithTag("SfxSource").GetComponent<AudioSource>();

            LoadMusicToggleValue();
            LoadSfxToggleValue();
            LoadMusicValue();
            LoadSfxValue();
        }
        
        private void LateUpdate()
        {
            LoadMusicToggleValue();
            LoadSfxToggleValue();
            LoadMusicValue();
            LoadSfxValue();
        }

        public void ToggleMusic()
        {
            switch (MusicToggle.isOn)
            {
                case true:
                    PlayerPrefs.SetInt("musicToggleValue", 0);
                    MusicSource.mute = true;
                    break;

                case false:
                    PlayerPrefs.SetInt("musicToggleValue", 1);
                    MusicSource.mute = false;
                    break;
            }

            AudioManager.Instance.ToggleMusic(PlayerPrefs.GetInt("musicToggleValue"));
            LoadMusicToggleValue();
        }

        void LoadMusicToggleValue()
        {
            if (PlayerPrefs.HasKey("musicToggleValue"))
            {
                switch (PlayerPrefs.GetInt("musicToggleValue"))
                {
                    case 0:
                        MusicToggle.isOn = true;
                        MusicSource.mute = true;
                        break;

                    default:
                        MusicToggle.isOn = false;
                        MusicSource.mute = false;
                        break;
                }
            }
            else
            {
                const int musicToggleValue = 1;
                PlayerPrefs.SetInt("musicToggleValue", musicToggleValue);
                MusicToggle.isOn = false;
                MusicSource.mute = false;
                ToggleMusic();
            }
        }

        public void ToggleSFX()
        {
            switch (SfxToggle.isOn)
            {
                case true:
                    PlayerPrefs.SetInt("sfxToggleValue", 0);
                    SfxSource.mute = true;
                    break;

                case false:
                    PlayerPrefs.SetInt("sfxToggleValue", 1);
                    SfxSource.mute = false;
                    break;
            }

            AudioManager.Instance.ToggleSfx(PlayerPrefs.GetInt("sfxToggleValue"));
            LoadSfxToggleValue();
        }

        void LoadSfxToggleValue()
        {
            if (PlayerPrefs.HasKey("sfxToggleValue"))
            {
                switch (PlayerPrefs.GetInt("sfxToggleValue"))
                {
                    case 0:
                        SfxToggle.isOn = true;
                        SfxSource.mute = true;
                        break;

                    default:
                        SfxToggle.isOn = false;
                        SfxSource.mute = false;
                        break;
                }
            }
            else
            {
                const int sfxToggleValue = 1;
                PlayerPrefs.SetInt("sfxToggleValue", sfxToggleValue);
                SfxToggle.isOn = false;
                SfxSource.mute = false;
                ToggleSFX();
            }
        }

        public void MusicVolume()
        {
            AudioManager.Instance.MusicVolume(MusicSlider.value);
            float musicVolumeValue = MusicSlider.value;
            PlayerPrefs.SetFloat("MusicVolumeValue", musicVolumeValue);
            LoadMusicValue();
        }

        private void LoadMusicValue()
        {
            if (PlayerPrefs.HasKey("MusicVolumeValue"))
            {
                float musicVolumeValue = PlayerPrefs.GetFloat("MusicVolumeValue");
                MusicSlider.value = musicVolumeValue;
            }
            else
            {
                PlayerPrefs.SetFloat("MusicVolumeValue", AudioManager.MusicParam);
                MusicSlider.value = AudioManager.MusicParam;
                MusicVolume();
            }
        }

        private void SFXVolume()
        {
            AudioManager.Instance.SfxVolume(SfxSlider.value);
            float sfxVolumeValue = SfxSlider.value;
            PlayerPrefs.SetFloat("SfxVolumeValue", sfxVolumeValue);
            LoadMusicValue();
        }

        private void LoadSfxValue()
        {
            if (PlayerPrefs.HasKey("SfxVolumeValue"))
            {
                float sfxVolumeValue = PlayerPrefs.GetFloat("SfxVolumeValue");
                SfxSlider.value = sfxVolumeValue;
            }
            else
            {
                PlayerPrefs.SetFloat("SfxVolumeValue", AudioManager.MusicParam);
                SfxSlider.value = AudioManager.MusicParam;
                SFXVolume();
            }
        }
    }
}