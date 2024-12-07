using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using Main_Folders.Scripts.Minimapa;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Image loadingBar;

    private static int nextSceneIndex;
    private static LoadType loadType;

    private MarkerHolder markerHolder;

    public enum LoadType
    {
        Normal,
        Additive
    }

    public static void LoadScene(int sceneIndex, LoadType type = LoadType.Normal)
    {
        nextSceneIndex = sceneIndex;
        loadType = type;

        // Carregar a cena de loading como Additive para evitar descarregar a atual.
        SceneManager.LoadScene("LOAD_SCENE", LoadSceneMode.Additive);
    }

    private void Start()
    {
        markerHolder = FindAnyObjectByType<MarkerHolder>(FindObjectsInactive.Include);

        if (nextSceneIndex >= 0)
        {
            if (loadType == LoadType.Additive)
            {
                StartCoroutine(LoadBattleLevel());
            }
            else
            {
                StartCoroutine(LoadNextLevel());
            }
        }
    }

    private IEnumerator LoadNextLevel()
    {
        AsyncOperation loadLevel = SceneManager.LoadSceneAsync(nextSceneIndex);

        while (!loadLevel.isDone)
        {
            UpdateLoadingBar(loadLevel.progress);
            yield return null;
            markerHolder.ChangeScene();
        }

        // Remover a cena de loading após carregamento.
        SceneManager.UnloadSceneAsync("LOAD_SCENE");
    }

    private IEnumerator LoadBattleLevel()
    {
        AsyncOperation loadLevel = SceneManager.LoadSceneAsync(nextSceneIndex, LoadSceneMode.Additive);
        while (!loadLevel.isDone)
        {
            UpdateLoadingBar(loadLevel.progress);
            yield return null;
        }

        // Fechar cena de loading sem descarregar a cena base
        SceneManager.UnloadSceneAsync("LOAD_SCENE");
    }

    public static void UnloadBattleScene(int sceneIndex)
    {
        Scene sceneToUnload = SceneManager.GetSceneByBuildIndex(sceneIndex);
        if (sceneToUnload.isLoaded)
        {
            SceneManager.UnloadSceneAsync(sceneIndex);
        }
        else
        {
            Debug.LogWarning($"A cena de índice {sceneIndex} não está carregada.");
        }
    }

    private void UpdateLoadingBar(float progress)
    {
        if (loadingBar != null)
        {
            loadingBar.fillAmount = Mathf.Clamp01(progress / 0.9f);
        }
    }
}
