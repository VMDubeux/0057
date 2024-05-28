using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{
    public static LevelsManager Instance;

    [SerializeField] private GameObject PauseCanvasMenu;

    [SerializeField] private GameObject CanvasInventario;

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
            CameraPivot = null;
            EventSystem = null;
            Light = null;
        }
        if (currentGameSceneIndex > 0 && SceneManager.sceneCount == 1)
        {
            CameraPivot.SetActive(true);
            CanvasInventario.SetActive(true);
            EventSystem.SetActive(true);
            Light.SetActive(true);

        }
        else if (currentGameSceneIndex > 0 && SceneManager.sceneCount == 2)
        {
            CameraPivot.SetActive(false);
            CanvasInventario.SetActive(false);
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