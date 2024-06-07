using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main_Folders.Scripts.Managers
{
    public class GraphicManager : MonoBehaviour
    {
        public static GraphicManager Instance;

        [SerializeField] internal TMP_Dropdown resolutionDropdown, qualityDropdown;

        [SerializeField] internal Toggle fullScreenToggle;

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

            SearchGameDropdownsAndToggles();
        }

        private void Update()
        {
            ToggleFullScreen(PlayerPrefs.GetInt("fullScreenToggleValue"));
            ResolutionValue(PlayerPrefs.GetInt("ResolutionValue"));
            QualityValue(PlayerPrefs.GetInt("QualityValue"));
        }

        public void ToggleFullScreen(int value)
        {
            if (value == 0) fullScreenToggle.isOn = false;
            else if (value == 1) fullScreenToggle.isOn = true;
        }

        public void ResolutionValue(int resolution)
        {
            resolutionDropdown.value = resolution;
        }

        public void QualityValue(int quality)
        {
            qualityDropdown.value = quality;
        }

        private void SearchGameDropdownsAndToggles()
        {
            qualityDropdown = GameObject.FindWithTag("QualityDropdown").GetComponent<TMP_Dropdown>();
            resolutionDropdown = GameObject.FindWithTag("ResolutionDropdown").GetComponent<TMP_Dropdown>();
            fullScreenToggle = GameObject.FindWithTag("FullScreenToggle").GetComponent<Toggle>();
        }
    }
}