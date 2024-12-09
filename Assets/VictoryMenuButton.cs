using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryMenuButton : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        PlayerPrefs.DeleteAll();
        SceneLoader.LoadScene(1);
        Time.timeScale = 1.0f;
    }
}
