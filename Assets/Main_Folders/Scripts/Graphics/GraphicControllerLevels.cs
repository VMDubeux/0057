using System;
using System.Collections.Generic;
using Main_Folders.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main_Folders.Scripts.Graphics
{
    public class GraphicControllerLevels : MonoBehaviour
    {
        private TMP_Dropdown _resolutionDropdown, _qualityDropdown;
        private Resolution[] _resolutions;
        private Toggle _fullScreenToggle;
        List<string> options = new();

        private void Awake()
        {
            _resolutionDropdown = GameObject.Find("CanvasPause/Container/FullScreen/Dropdown")
                .GetComponent<TMP_Dropdown>();
            GraphicManager.Instance.resolutionDropdown = _resolutionDropdown;
            _qualityDropdown = GameObject.Find("CanvasPause/Container/Graphics/GraphicsDropdown")
                .GetComponent<TMP_Dropdown>();
            GraphicManager.Instance.qualityDropdown = _qualityDropdown;
            _fullScreenToggle = GameObject.Find("CanvasPause/Container/FullScreen/Toggle").GetComponent<Toggle>();
            GraphicManager.Instance.fullScreenToggle = _fullScreenToggle;
        }

        private void Start()
        {
            Resolution();
            LoadFullScreenToggleValue();
            LoadResolutionValue();
            LoadQualityValue();
        }

        private void Resolution()
        {
            _resolutions = Screen.resolutions;

            foreach (var t in _resolutions)
            {
                // Only for school lessons (Split Method)
                string[] parameters = { "x", "@", "." };
                string resolutionString = t.ToString();
                string[] splitArray = resolutionString.Split(parameters, 3, StringSplitOptions.RemoveEmptyEntries);

                string width = splitArray[0];
                string height = splitArray[1];

                string[] refreshRateSplitArray =
                    splitArray[2].Split(parameters, 2, StringSplitOptions.RemoveEmptyEntries);
                string refreshRate = refreshRateSplitArray[0];

                string option = $"{width} x {height} {refreshRate}Hz";

                //if (100 < int.Parse(refreshRate))
                options.Add(option);
            }

            _resolutionDropdown.ClearOptions();
            _resolutionDropdown.AddOptions(options);
            _resolutionDropdown.RefreshShownValue();
        }

        public void SetToggleFullScreen()
        {
            switch (_fullScreenToggle.isOn)
            {
                case true:
                    PlayerPrefs.SetInt("fullScreenToggleValue", 1);
                    Screen.fullScreen = true;
                    break;
                case false:
                    PlayerPrefs.SetInt("fullScreenToggleValue", 0);
                    Screen.fullScreen = false;
                    break;
            }

            LoadFullScreenToggleValue();
        }

        private static void LoadFullScreenToggleValue()
        {
            if (PlayerPrefs.HasKey("fullScreenToggleValue"))
            {
                if (PlayerPrefs.GetInt("fullScreenToggleValue") == 0) Screen.fullScreen = false;
                else if (PlayerPrefs.GetInt("fullScreenToggleValue") == 1) Screen.fullScreen = true;
            }

            else PlayerPrefs.SetInt("fullScreenToggleValue", 1);
        }

        public void SetResolutions()
        {
            Resolution resolution = _resolutions[_resolutionDropdown.value];

            switch (resolution.width)
            {
                case >= 1920:
                    Screen.SetResolution(resolution.width, resolution.height, FullScreenMode.FullScreenWindow);
                    _fullScreenToggle.isOn = true;
                    SetToggleFullScreen();
                    break;

                default:
                    Screen.SetResolution(resolution.width, resolution.height, FullScreenMode.Windowed);
                    _fullScreenToggle.isOn = false;
                    SetToggleFullScreen();
                    break;
            }

            PlayerPrefs.SetInt("ResolutionValue", _resolutionDropdown.value);
            LoadResolutionValue();
        }

        void LoadResolutionValue()
        {
            if (PlayerPrefs.HasKey("ResolutionValue"))
            {
                int resolutionValue = PlayerPrefs.GetInt("ResolutionValue");
                _resolutionDropdown.value = resolutionValue;
            }
            else
            {
                int listLastIndex = options.Count;

                string[] parameters = { "x", "@", "." };
                string resolutionString = options[listLastIndex].ToString();
                string[] splitArray = resolutionString.Split(parameters, 3, StringSplitOptions.RemoveEmptyEntries);

                string width = splitArray[0];
                string height = splitArray[1];
                Screen.SetResolution(int.Parse(width), int.Parse(height), FullScreenMode.FullScreenWindow);
                _resolutionDropdown.value = listLastIndex - 1;
            }
        }

        public void SetQuality()
        {
            QualitySettings.SetQualityLevel(_qualityDropdown.value);
            PlayerPrefs.SetInt("QualityValue", _qualityDropdown.value);
            LoadQualityValue();
        }

        private void LoadQualityValue()
        {
            if (PlayerPrefs.HasKey("QualityValue"))
            {
                int qualityValue = PlayerPrefs.GetInt("QualityValue");
                _qualityDropdown.value = qualityValue;
            }
            else
            {
                QualitySettings.SetQualityLevel(2);
            }
        }
    }
}