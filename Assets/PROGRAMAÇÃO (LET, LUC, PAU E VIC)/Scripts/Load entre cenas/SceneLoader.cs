using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    private IEnumerator LoadNextLevel()
    {
        AsyncOperation loadLevel = SceneManager.LoadSceneAsync(nextSceneIndex);

        while (!loadLevel.isDone)
        {
            if (loadingBar != null)
                loadingBar.fillAmount = Mathf.Clamp01(loadLevel.progress / 0.9f);

            yield return null;
        }
    }
}
