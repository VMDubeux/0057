using System.Collections;
using System.Collections.Generic;
using Main_Folders.Scripts.Cards.CardsEffects;
using UnityEngine;

public class EndTurnState  : State
{
    public override IEnumerator Enter(){
        yield return null;
        DamageEffect.lightQuantity = 0;
        StartCoroutine(WaitThenChangeState<EnemyTurnState>());
    }
}