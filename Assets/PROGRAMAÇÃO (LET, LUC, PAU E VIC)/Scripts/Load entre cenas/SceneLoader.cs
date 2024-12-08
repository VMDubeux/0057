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

    public static AsyncOperation UnloadBattleScene(int sceneIndex)
    {
        Scene sceneToUnload = SceneManager.GetSceneByBuildIndex(sceneIndex);
        if (sceneToUnload.isLoaded)
        {
            Debug.Log($"Descarregando cena de índice {sceneIndex}...");
            return SceneManager.UnloadSceneAsync(sceneIndex);
        }
        else
        {
            Debug.LogWarning($"A cena de índice {sceneIndex} não está carregada.");
            return null;
        }
    }

    private void UpdateLoadingBar(float progress)
    {
        if (loadingBar != null)
        {
            loadingBar.fillAmount = Mathf.Clamp01(progress / 0.9f);
        }
    }

    public static IEnumerator ReloadScene(int sceneIndex)
    {
        // Verifica se a cena está carregada e a descarrega
        Scene sceneToUnload = SceneManager.GetSceneByBuildIndex(sceneIndex);
        if (sceneToUnload.isLoaded)
        {
            AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(sceneIndex);
            while (!unloadOperation.isDone)
            {
                yield return null; // Aguarda o descarregamento
            }
            Debug.Log($"Cena {sceneIndex} descarregada.");
        }

        // Recarrega a cena
        LoadScene(sceneIndex, LoadType.Additive);
    }
}
