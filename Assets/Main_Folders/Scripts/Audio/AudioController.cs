using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public AudioMixer AudioMixer;
    private Toggle MusicToggle, SfxToggle;
    private Slider MusicSlider, SfxSlider;
    private AudioSource MusicSource, SfxSource;
    //public static AudioController Instance;

    private void Awake()
    {
        MusicSlider = GameObject.Find("Canvas/OptionsMenu/Container/Music/SliderMusic").GetComponent<Slider>();
        MusicToggle = GameObject.Find("Canvas/OptionsMenu/Container/Music/MusicToggle").GetComponent<Toggle>();
        SfxSlider = GameObject.Find("Canvas/OptionsMenu/Container/SFX/SliderSFX").GetComponent<Slider>();
        SfxToggle = GameObject.Find("Canvas/OptionsMenu/Container/SFX/SfxToggle").GetComponent<Toggle>();
    }

    private void Start()
    {
        MusicSource = GameObject.FindGameObjectWithTag("MusicSource").GetComponent<AudioSource>();
        SfxSource = GameObject.FindGameObjectWithTag("SfxSource").GetComponent<AudioSource>();
    }

    void Update()
    {
        LoadMusicToggleValue();
        LoadSfxToggleValue();
        LoadMusicValue();
        LoadSfxValue();
    }

    public void ToggleMusic()
    {
        if (MusicToggle.isOn == true)
        {
            PlayerPrefs.SetInt("musicToggleValue", 0);
            MusicSource.mute = false;
        }
        else if (MusicToggle.isOn == false)
        {
            PlayerPrefs.SetInt("musicToggleValue", 1);
            MusicSource.mute = true;
        }
        AudioManager.Instance.ToggleMusic(PlayerPrefs.GetInt("musicToggleValue"));
        LoadMusicToggleValue();
    }

    void LoadMusicToggleValue()
    {
        if (PlayerPrefs.HasKey("musicToggleValue"))
        {
            if (PlayerPrefs.GetInt("musicToggleValue") == 0) MusicToggle.isOn = true;
            else if (PlayerPrefs.GetInt("musicToggleValue") == 1) MusicToggle.isOn = false;
        }
        else PlayerPrefs.SetInt("musicToggleValue", 0);
    }

    public void ToggleSFX()
    {
        if (SfxToggle.isOn == true)
        {
            PlayerPrefs.SetInt("sfxToggleValue", 0);
            SfxSource.mute = false;
        }
        else if (SfxToggle.isOn == false)
        {
            PlayerPrefs.SetInt("sfxToggleValue", 1);
            SfxSource.mute = true;
        }
        AudioManager.Instance.ToggleSFX(PlayerPrefs.GetInt("sfxToggleValue"));
        LoadSfxToggleValue();
    }

    void LoadSfxToggleValue()
    {
        if (PlayerPrefs.HasKey("sfxToggleValue"))
        {
            if (PlayerPrefs.GetInt("sfxToggleValue") == 0) SfxToggle.isOn = true;
            else if (PlayerPrefs.GetInt("sfxToggleValue") == 1) SfxToggle.isOn = false;
        }
        else PlayerPrefs.SetInt("sfxToggleValue", 0);
    }

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(MusicSlider.value);
        float musicVolumeValue = MusicSlider.value;
        PlayerPrefs.SetFloat("MusicVolumeValue", musicVolumeValue);
        LoadMusicValue();
    }

    void LoadMusicValue()
    {
        float musicVolumeValue = PlayerPrefs.GetFloat("MusicVolumeValue");
        MusicSlider.value = musicVolumeValue;
    }

    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(SfxSlider.value);
        float sfxVolumeValue = SfxSlider.value;
        PlayerPrefs.SetFloat("SfxVolumeValue", sfxVolumeValue);
        LoadMusicValue();
    }

    void LoadSfxValue()
    {
        float sfxVolumeValue = PlayerPrefs.GetFloat("SfxVolumeValue");
        SfxSlider.value = sfxVolumeValue;
    }
}