using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GraphicControllerLevels : MonoBehaviour
{
    private TMP_Dropdown resolutionDropdown, qualityDropdown;
    private Resolution[] resolutions;
    private Toggle fullScreenToggle;

    private void Awake()
    {
        resolutionDropdown = GameObject.Find("CanvasPause/OptionsMenu/FullScreen/Dropdown").GetComponent<TMP_Dropdown>();
        qualityDropdown = GameObject.Find("CanvasPause/OptionsMenu/Graphics/GraphicsDropdown").GetComponent<TMP_Dropdown>();
        fullScreenToggle = GameObject.Find("CanvasPause/OptionsMenu/FullScreen/Toggle").GetComponent<Toggle>();
    }

    private void Start()
    {
        Resolution();
    }

    private void Update()
    {
        LoadFullScreenToggleValue();
    }

    private void Resolution()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetToggleFullScreen()
    {
        GraphicManager.Instance.ToggleFullScreen();
        if (fullScreenToggle.isOn == true)
        {
            PlayerPrefs.SetInt("fullScreenToggleValue", 0);
            Screen.fullScreen = false;
        }
        else if (fullScreenToggle.isOn == false)
        {
            PlayerPrefs.SetInt("fullScreenToggleValue", 1);
            Screen.fullScreen = true;
        }
    }

    void LoadFullScreenToggleValue()
    {
        if (PlayerPrefs.HasKey("fullScreenToggleValue"))
        {
            if (PlayerPrefs.GetInt("fullScreenToggleValue") == 0) Screen.fullScreen = false;
            else if (PlayerPrefs.GetInt("fullScreenToggleValue") == 1) Screen.fullScreen = true;
        }
        else PlayerPrefs.SetInt("fullScreenToggleValue", 0);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        int resolutionValue = resolutionDropdown.value;
        PlayerPrefs.SetInt("ResolutionValue", resolutionValue);
        GraphicManager.Instance.ResolutionValue(resolutionValue);
        LoadResolutionValue();
    }

    void LoadResolutionValue()
    {
        int resolutionValue = PlayerPrefs.GetInt("ResolutionValue");
        resolutionDropdown.value = resolutionValue;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        int qualityValue = qualityDropdown.value;
        PlayerPrefs.SetInt("QualityValue", qualityValue);
        GraphicManager.Instance.QualityValue(qualityIndex);
        LoadQualityValue();
    }

    public void LoadQualityValue()
    {
        int qualityValue = PlayerPrefs.GetInt("QualityValue");
        qualityDropdown.value = qualityValue;
    }
}
