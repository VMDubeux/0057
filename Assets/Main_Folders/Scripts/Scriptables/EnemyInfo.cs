using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Enemy")]
public class EnemyInfo : ScriptableObject
{
    public string EnemyName;
    public int BaseHealth;
    public int BaseBlock;
    public int BaseInitiative;
    public GameObject EnemyVisualPrefab; //used in battle scene
}
