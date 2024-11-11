using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneLoadButton : MonoBehaviour
{
    public void Play()
    {
        SceneLoader.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
