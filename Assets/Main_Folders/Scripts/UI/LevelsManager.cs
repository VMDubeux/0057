using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{
    public static LevelsManager Instance;

    private GameObject PauseCanvasMenu;

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

        PauseCanvasMenu = GameObject.Find("Canvas_Pause_Menu");
    }

    private void Start()
    {
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
    }

    private void OnGUI()
    {
        currentGameSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentGameSceneIndex == 0)
            PauseCanvasMenu.SetActive(false);
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