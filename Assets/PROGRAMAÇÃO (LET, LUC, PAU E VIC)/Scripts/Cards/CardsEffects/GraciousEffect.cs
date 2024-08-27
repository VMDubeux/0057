using System.Collections;
using System.Collections.Generic;
using Main_Folders.Scripts.Units;
using Main_Folders.Scripts.Visuals;
using UnityEngine;

public class GraciousEffect : CardEffect
{
    [SerializeField] int graciousAmount = 5;
    public const int graciousHeal = 2;
    public override IEnumerator Apply(List<object> targets)
    {
        foreach (BattleVisuals battleVisuals in StateMachine.Instance.Units)
        {
            if (battleVisuals.HP > 0 && battleVisuals.CompareTag("Player"))
            {
                targets.Add(battleVisuals);
                battleVisuals.GetComponent<PlayerUnit>().Graciosidade += graciousAmount;
            }
        }
        yield return null;
    }
}