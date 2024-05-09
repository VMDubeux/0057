using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Enemy")]
public class EnemyInfo : ScriptableObject
{
    public string EnemyName;
    public int BaseHP;
    public int BaseBlock;
    public int BaseStrength;
    public GameObject EnemyVisualPrefab; //used in battle scene
}
