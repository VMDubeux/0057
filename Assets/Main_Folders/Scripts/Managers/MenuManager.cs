using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private GameObject mainMenu, optionsMenu, creditsMenu;

    private void Awake()
    {
        mainMenu = GameObject.Find("Canvas/MainMenu");
        optionsMenu = GameObject.Find("Canvas/OptionsMenu");
        creditsMenu = GameObject.Find("Canvas/CreditsMenu");
    }

    private void Start()
    {
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene(sceneBuildIndex:1);
    }

    public void OpenOptions()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void OpenCredits()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void ReturnFromOptions()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void ExitGame()
    {
        print("Exit game!");
        Application.Quit();
    }
}
