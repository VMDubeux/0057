using System.Collections;
using System.Collections.Generic;
using Main_Folders.Scripts.Cards.CardsEffects;
using Main_Folders.Scripts.Units;
using Main_Folders.Scripts.Visuals;
using UnityEngine;

public class FearEffect : CardEffect
{
    int enemiesFeared = 0;
    public bool isRecoveringEnergy = false;
    public override IEnumerator Apply(List<object> targets)
    {
        enemiesFeared = 0;
        EnemyTurnState.enemyCanPlay = false;
        foreach (BattleVisuals battleVisuals in StateMachine.Instance.Units)
        {
            enemiesFeared++;
        }
        if(isRecoveringEnergy)
        {
            EnergyByEnemyFeared();
        }
        yield return null;
    }
    public void EnergyByEnemyFeared ()
    {
        gameObject.GetComponent<ChangeEnergyEffect>().Value = enemiesFeared - 3;
    }
}