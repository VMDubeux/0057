using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyManager : MonoBehaviour
{
    public static GameObject instance;
    
    [SerializeField] private GameObject[] allEnemies;
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

    public void GenerateEnemiesByEncouter(Encouter[] encounters, int maxNumEnemies)
    {
        currentEnemies.Clear();
        int numEnemies = Random.Range(1, maxNumEnemies + 1);

        for (int i = 0; i < numEnemies; i++)
        {
            Encouter tempEncouter = encounters[Random.Range(0, encounters.Length)];
            int level = Random.Range(tempEncouter.LevelMin, tempEncouter.LevelMax + 1);
            GenerateEnemyByName(tempEncouter.Enemy.name, level);
        }
    }

    public void GenerateEnemyByName(string enemyName, int level)
    {
        for (int i = 0; i < allEnemies.Length; i++)
        {
            if (enemyName == allEnemies[i].name)
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
