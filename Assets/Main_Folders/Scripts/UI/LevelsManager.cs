using System.Collections;
using System.Collections.Generic;
using Main_Folders.Scripts.Audio;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{
    public static LevelsManager Instance;

    internal bool isTalking = false;

    [SerializeField] private GameObject PauseCanvasMenu;

    [SerializeField] internal GameObject LevelCanvas;

    [SerializeField] internal GameObject CanvasInventario;

    [SerializeField] private GameObject CameraPivot;

    [SerializeField] private GameObject EventSystem;

    [SerializeField] private GameObject Light;

    [Header("Escreva o nome da cena atual:")]
    [SerializeField] internal int currentGameSceneIndex;

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
        PauseCanvasMenu = FindAnyObjectByType<AudioControllerLevels>(FindObjectsInactive.Include).gameObject;

        PauseCanvasMenu.SetActive(false);

        Time.timeScale = 1.0f; // Verificar necessidade;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && currentGameSceneIndex != 0)
        {
            if (PauseCanvasMenu.activeSelf)
            {
                PauseCanvasMenu.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                PauseCanvasMenu.SetActive(true);
                Time.timeScale = 0;
            }
        }

        if (currentGameSceneIndex > 0)
        {
            CanvasInventario = FindAnyObjectByType<CanvasInventario>(FindObjectsInactive.Include).gameObject;
            LevelCanvas = FindAnyObjectByType<CanvasHUD>(FindObjectsInactive.Include).gameObject;
            CameraPivot = FindAnyObjectByType<CameraPivot>(FindObjectsInactive.Include).gameObject;
            EventSystem = FindFirstObjectByType<EventSystem>(FindObjectsInactive.Include).gameObject;
            Light = FindFirstObjectByType<Light>(FindObjectsInactive.Include).gameObject;
        }
    }

    private void OnGUI()
    {
        currentGameSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentGameSceneIndex == 0)
        {
            PauseCanvasMenu.SetActive(false);
            CanvasInventario = null;
            LevelCanvas = null;
            CameraPivot = null;
            EventSystem = null;
            Light = null;
        }
        if (currentGameSceneIndex > 0 && SceneManager.sceneCount == 1)
        {
            CameraPivot.SetActive(true);
            EventSystem.SetActive(true);
            Light.SetActive(true);

            if (isTalking == false)
            {
                CanvasInventario.SetActive(true);
                LevelCanvas.SetActive(true);
            }
            else
            {
                CanvasInventario.SetActive(false);
                LevelCanvas.SetActive(false);
            }
        }
        else if (currentGameSceneIndex > 0 && SceneManager.sceneCount == 2)
        {
            CameraPivot.SetActive(false);
            CanvasInventario.SetActive(false);
            LevelCanvas.SetActive(false);
            EventSystem.SetActive(false);
            Light.SetActive(false);
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(sceneBuildIndex: 0);
        Time.timeScale = 1.0f;
    }

    public void ResumeGame()
    {
        PauseCanvasMenu.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }
}