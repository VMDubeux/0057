using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterSystem : MonoBehaviour
{
    [SerializeField] private Encouter[] enemiesInScene;
    [SerializeField] private int maxNumEnemies;

    private EnemyManager enemyManager;

    void Start()
    {
        enemyManager = GameObject.FindFirstObjectByType<EnemyManager>();
        enemyManager.GenerateEnemiesByEncouter(enemiesInScene, maxNumEnemies);
    }
}

[System.Serializable]
public class Encouter
{
    public EnemyInfo Enemy;
    public int LevelMin;
    public int LevelMax;
}

