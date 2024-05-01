using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GraphicManager : MonoBehaviour
{
    public static GraphicManager Instance;

    [SerializeField] private TMP_Dropdown resolutionDropdown, qualityDropdown;

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

        //resolutionDropdown = GameObject.Find("Canvas/OptionsMenu/FullScreen/Dropdown").GetComponent<TMP_Dropdown>();
        //qualityDropdown = GameObject.Find("Canvas/OptionsMenu/Graphics/GraphicsDropdown").GetComponent<TMP_Dropdown>();
        qualityDropdown = GameObject.FindWithTag("QualityDropdown").GetComponent<TMP_Dropdown>();
        resolutionDropdown = GameObject.FindWithTag("ResolutionDropdown").GetComponent<TMP_Dropdown>();
    }

    private void Start()
    {
        ResolutionValue(PlayerPrefs.GetInt("ResolutionValue"));
        QualityValue(PlayerPrefs.GetInt("QualityValue"));
        ToggleFullScreen();
    }

    public void ToggleFullScreen()
    {
        PlayerPrefs.GetInt("fullScreenToggleValue");
    }

    public void ResolutionValue(int resolution)
    {
        resolutionDropdown.value = resolution;    
    }

    public void QualityValue(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
        qualityDropdown.value = quality;
    }
}
