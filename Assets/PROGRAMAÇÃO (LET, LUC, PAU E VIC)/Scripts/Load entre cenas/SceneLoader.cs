using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Image loadingBar;

    private static int nextSceneIndex;

    public static void LoadScene(int sceneIndex)
    {
        nextSceneIndex = sceneIndex;
        SceneManager.LoadScene("LOAD_SCENE");
    }

    private void Start()
    {
        if (nextSceneIndex >= 0)
        {
            StartCoroutine(LoadNextLevel());
        }
    }

    IEnumerator LoadNextLevel()
    {
        AsyncOperation loadLevel = SceneManager.LoadSceneAsync(nextSceneIndex);

        while (!loadLevel.isDone)
        {
            loadingBar.fillAmount = Mathf.Clamp01(loadLevel.progress / 0.9f);
            yield return null;
        }
    }
}