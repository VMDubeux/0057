using System.Collections;
using System.Collections.Generic;
using Main_Folders.Scripts.Units;
using Main_Folders.Scripts.Visuals;
using UnityEngine;

public class ChangeEnergyEffect : CardEffect
{
    public int Value;
    public bool enermyEqualsEnemies;
    public override IEnumerator Apply(List<object> targets)
    {
        foreach (object o in targets)
        {
            BattleVisuals player = o as BattleVisuals;
            if (player.CompareTag("Player"))
            {
                PlayerUnit unit = player.GetComponent<PlayerUnit>();
                if(enermyEqualsEnemies)
                {
                    unit.CurrentEnergy += Value + TargetAllEnemies.enemiesInBattle;
                }
                else
                    unit.CurrentEnergy += Value;
            }
            yield return null;
        }
    }
}
