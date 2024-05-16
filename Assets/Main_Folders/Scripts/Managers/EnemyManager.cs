using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyManager : MonoBehaviour
{
    public static GameObject instance;

    public GameObject[] allEnemies;
    [Header("Enemy Prefab")]
    public GameObject enemy;
    [Header("")]
    [SerializeField] private List<Enemy> currentEnemies;

    private const float LEVEL_MOD = 0.5f;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this.gameObject;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void GenerateEnemyByEncouter(int numEnemy, int lvlMin, int lvlMax)
    {
        currentEnemies.Clear();
        int NumEnemy = numEnemy;

        for (int i = 0; i < NumEnemy; i++)
        {
            //Encouter tempEncouter = encounter;
            int level = Random.Range(lvlMin, lvlMax + 1);
            Debug.Log(level);
            GenerateEnemyByName(level);
        }
    }

    public void GenerateVariableEnemiesByEncouter(int minNumEnemies, int maxNumEnemies, int lvlMin, int lvlMax)
    {
        currentEnemies.Clear();
        int numEnemies = Random.Range(minNumEnemies, maxNumEnemies + 1);

        for (int i = 0; i < numEnemies; i++)
        {
            int level = Random.Range(lvlMin, lvlMax + 1);
            GenerateEnemiesByName(level);
        }
    }

    public void GenerateEnemyByName(int level)
    {
        Enemy newEnemy = new Enemy();
        newEnemy.EnemyName = enemy.name;
        newEnemy.Level = level;
        float levelModifier = (LEVEL_MOD * newEnemy.Level);

        newEnemy.HP = Mathf.RoundToInt(enemy.GetComponent<Unit>()._stats[1].Value + (enemy.GetComponent<Unit>()._stats[1].Value * levelModifier));
        newEnemy.MaxHP = newEnemy.HP;
        newEnemy.Block = Mathf.RoundToInt(enemy.GetComponent<Unit>()._stats[2].Value + (enemy.GetComponent<Unit>()._stats[2].Value * levelModifier));
        newEnemy.Strength = Mathf.RoundToInt(enemy.GetComponent<Unit>()._stats[3].Value + (enemy.GetComponent<Unit>()._stats[3].Value * levelModifier));
        newEnemy.EnemyVisualPrefab = enemy.gameObject;

        currentEnemies.Add(newEnemy);
    }

    public void GenerateEnemiesByName(int level)
    {
        for (int i = 0; i < allEnemies.Length; i++)
        {
            Enemy newEnemy = new Enemy();
            newEnemy.EnemyName = allEnemies[i].name;
            newEnemy.Level = level;
            float levelModifier = (LEVEL_MOD * newEnemy.Level);

            newEnemy.HP = Mathf.RoundToInt(allEnemies[i].GetComponent<Unit>()._stats[1].Value + (allEnemies[i].GetComponent<Unit>()._stats[1].Value * levelModifier));
            newEnemy.MaxHP = newEnemy.HP;
            newEnemy.Block = Mathf.RoundToInt(allEnemies[i].GetComponent<Unit>()._stats[2].Value + (allEnemies[i].GetComponent<Unit>()._stats[2].Value * levelModifier));
            newEnemy.Strength = Mathf.RoundToInt(allEnemies[i].GetComponent<Unit>()._stats[3].Value + (allEnemies[i].GetComponent<Unit>()._stats[3].Value * levelModifier));
            newEnemy.EnemyVisualPrefab = allEnemies[i].gameObject;

            currentEnemies.Add(newEnemy);
        }
    }

    public List<Enemy> GetCurrentEnemies()
    {
        return currentEnemies;
    }
}

[System.Serializable]
public class Enemy
{
    public string EnemyName;
    public int Level;
    public int HP;
    public int MaxHP;
    public int Block;
    public int Strength;
    public GameObject EnemyVisualPrefab;
}
