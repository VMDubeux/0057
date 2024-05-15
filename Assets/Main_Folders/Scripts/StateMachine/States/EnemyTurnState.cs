using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnState : State
{
    public override IEnumerator Enter(){
        yield return new WaitForSecondsRealtime (2f);
        if(machine.CurrentUnit.gameObject.GetComponent<EnemyUnit>() != null)
        {
            StartCoroutine(machine.CurrentUnit.EnemyDamage());
            StartCoroutine(WaitThenChangeState<EndTurnState>());
        }
        else StartCoroutine(WaitThenChangeState<PlayCardsState>());
    }
}