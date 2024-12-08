using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterSystem : MonoBehaviour
{
    private EnemyManager enemyManager;
    public bool battleActive;
    public GameObject prefab;

    void Awake()
    {
        enemyManager = GameObject.FindFirstObjectByType<EnemyManager>(FindObjectsInactive.Include);
    }

    /// <summary>
    /// Gera os inimigos de acordo com as configurações do encontro e inicia a cena de batalha.
    /// </summary>
    public IEnumerator StartGenerateEnemiesByEncouter(
        int min,
        int max,
        int fixedCount,
        bool isVariable,
        int levelMin,
        int levelMax,
        GameObject overworldPrefab,
        GameObject battlePrefab)
    {
        if (battleActive)
        {
            yield break; // Evita iniciar múltiplas batalhas
        }

        battleActive = true;
        prefab = overworldPrefab;

        if (!isVariable)
        {
            // Geração fixa de inimigos
            enemyManager.GenerateEnemyByEncouter(fixedCount, levelMin, levelMax, battlePrefab, overworldPrefab);
        }
        else
        {
            // Geração variável de inimigos
            enemyManager.GenerateVariableEnemiesByEncouter(min, max, levelMin, levelMax, battlePrefab, overworldPrefab);
        }

        yield return new WaitForSeconds(0.1f);

        // Carrega a cena de batalha como adicional
        //SceneManager.LoadScene("LEVEL_BATTLE", LoadSceneMode.Additive);
        SceneLoader.LoadScene(4, SceneLoader.LoadType.Additive);
    }

    public IEnumerator StartDungeonGenerateEnemiesByEncouter(
        int min,
        int max,
        int fixedCount,
        bool isVariable,
        int levelMin,
        int levelMax,
        GameObject overworldPrefab,
        GameObject battlePrefab)
    {
        if (battleActive)
        {
            yield break; // Evita iniciar múltiplas batalhas
        }

        battleActive = true;
        prefab = overworldPrefab;

        if (!isVariable)
        {
            // Geração fixa de inimigos
            enemyManager.GenerateEnemyByEncouter(fixedCount, levelMin, levelMax, battlePrefab, overworldPrefab);
        }
        else
        {
            // Geração variável de inimigos
            enemyManager.GenerateVariableEnemiesByEncouter(min, max, levelMin, levelMax, battlePrefab, overworldPrefab);
        }

        yield return new WaitForEndOfFrame();

        // Carrega a cena de batalha como adicional
        //SceneManager.LoadScene("LEVEL_BATTLE", LoadSceneMode.Additive);
        SceneLoader.LoadScene(7, SceneLoader.LoadType.Additive);
    }

    public IEnumerator StartDungeonBossGenerateEnemiesByEncouter(
    int min,
        int max,
        int fixedCount,
        bool isVariable,
        int levelMin,
        int levelMax,
        GameObject overworldPrefab,
        GameObject battlePrefab)
    {
        if (battleActive)
        {
            yield break; // Evita iniciar múltiplas batalhas
        }

        battleActive = true;
        prefab = overworldPrefab;

        if (!isVariable)
        {
            // Geração fixa de inimigos
            enemyManager.GenerateEnemyByEncouter(fixedCount, levelMin, levelMax, battlePrefab, overworldPrefab);
        }
        else
        {
            // Geração variável de inimigos
            enemyManager.GenerateVariableEnemiesByEncouter(min, max, levelMin, levelMax, battlePrefab, overworldPrefab);
        }

        yield return new WaitForEndOfFrame();

        // Garante que a cena 7 seja descarregada e recarregada
        yield return StartCoroutine(SceneLoader.ReloadScene(7));
    }
}