using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Menu : MonoBehaviour
{
    public GameObject PauseCanvasMenu;
    public GameObject CanvasInventario;
    public GameObject CameraPivot;
    public GameObject EventSystem;
    public static Pause_Menu Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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

        if (SceneManager.loadedSceneCount > 1)
        {
            CameraPivot.SetActive(false);
            CanvasInventario.SetActive(false);
            EventSystem.SetActive(false);
        }
        else
        {
            CameraPivot.SetActive(true);
            CanvasInventario.SetActive(true);
            EventSystem.SetActive(true);
        }
    }
}
