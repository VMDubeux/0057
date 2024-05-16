using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterSystem : MonoBehaviour
{
    //[SerializeField] private GameObject[] enemiesInScene;
    //Encouter encouter;
    //[SerializeField] private Encouter[] enemiesInBattleScene;
    //[SerializeField] private Encouter enemyInBattleScene;
    //[SerializeField] private int minNumEnemies; // Se quiser, pode ser um número fixo, basta modificar no GenerateEnemiesByEncounter.
    //[SerializeField] private int maxNumEnemies; // Se quiser, pode ser um número fixo, basta modificar no GenerateEnemiesByEncounter.

    private EnemyManager enemyManager;

    void Start()
    {
        //enemiesInScene = GameObject.FindGameObjectsWithTag("Enemies");
        enemyManager = GameObject.FindFirstObjectByType<EnemyManager>(); 
        
    }

    public IEnumerator StartGenerateEnemiesByEncouter(int min, int max, int fixo, bool variavel, int lvlMin, int lvlMax)
    {
        if (variavel == false)
            enemyManager.GenerateEnemyByEncouter(/*enemyInBattleScene,*/ fixo, lvlMin, lvlMax);
        else
            enemyManager.GenerateVariableEnemiesByEncouter(/*enemiesInBattleScene,*/ min, max, lvlMin, lvlMax);

        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("LEVEL_BATTLE", LoadSceneMode.Single);
    }

    
}

/*[System.Serializable]
public class Encouter
{
    public GameObject Enemy;
    public int LevelMin;
    public int LevelMax;
}

*/