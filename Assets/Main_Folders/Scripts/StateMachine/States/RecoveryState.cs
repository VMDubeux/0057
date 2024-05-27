using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryState : State
{
    public override IEnumerator Enter()
    {
        yield return StartCoroutine(machine.CurrentUnit.Recover());
        if (StateMachine.Instance.CurrentUnit.CompareTag("Player"))
            StartCoroutine(WaitThenChangeState<PlayCardsState>());
        else
            StartCoroutine(WaitThenChangeState<EnemyTurnState>());
    }
}
