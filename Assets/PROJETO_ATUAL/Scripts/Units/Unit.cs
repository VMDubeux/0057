using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public List<Stat> Stats;
    [ContextMenu("Generate Stats")]
    void GenerateStats()
    {
        Stats = new List<Stat>();
        for (int i = 0; i < (int)StatType.Dexterity + 1; i++)
        {
            Stat stat = new Stat();
            stat.Type = (StatType)i;
            stat.value = Random.Range(0, 100);
            Stats.Add(stat);
        }
    }
}

