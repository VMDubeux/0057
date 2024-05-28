using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading;

public class GraphicControllerLevels : MonoBehaviour
{
    private TMP_Dropdown resolutionDropdown, qualityDropdown;
    private Resolution[] resolutions;
    private Toggle fullScreenToggle;

    private void Awake()
    {
        resolutionDropdown = GameObject.Find("CanvasPause/Container/FullScreen/Dropdown").GetComponent<TMP_Dropdown>();
        GraphicManager.Instance.resolutionDropdown = resolutionDropdown;
        qualityDropdown = GameObject.Find("CanvasPause/Container/Graphics/GraphicsDropdown").GetComponent<TMP_Dropdown>();
        GraphicManager.Instance.qualityDropdown = qualityDropdown;
        fullScreenToggle = GameObject.Find("CanvasPause/Container/FullScreen/Toggle").GetComponent<Toggle>();
        GraphicManager.Instance.fullScreenToggle = fullScreenToggle;
    }

    private void Start()
    {
        Resolution();
    }

    private void OnGUI()
    {
        if (gameObject.activeSelf)
        {
            resolutionDropdown = GameObject.Find("CanvasPause/Container/FullScreen/Dropdown").GetComponent<TMP_Dropdown>();
            GraphicManager.Instance.resolutionDropdown = resolutionDropdown;
            qualityDropdown = GameObject.Find("CanvasPause/Container/Graphics/GraphicsDropdown").GetComponent<TMP_Dropdown>();
            GraphicManager.Instance.qualityDropdown = qualityDropdown;
            fullScreenToggle = GameObject.Find("CanvasPause/Container/FullScreen/Toggle").GetComponent<Toggle>();
            GraphicManager.Instance.fullScreenToggle = fullScreenToggle;
        }
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
        resolutionDropdown.RefreshShownValue();
    }

    public void SetToggleFullScreen()
    {
        if (fullScreenToggle.isOn == true)
        {
            PlayerPrefs.SetInt("fullScreenToggleValue", 1);
            Screen.fullScreen = true;
        }
        else if (fullScreenToggle.isOn == false)
        {
            PlayerPrefs.SetInt("fullScreenToggleValue", 0);
            Screen.fullScreen = false;
        }
        LoadFullScreenToggleValue();
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
        PlayerPrefs.SetInt("ResolutionValue", resolutionDropdown.value);
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
        LoadQualityValue();
    }

    public void LoadQualityValue()
    {
        int qualityValue = PlayerPrefs.GetInt("QualityValue");
        qualityDropdown.value = qualityValue;
    }
}
