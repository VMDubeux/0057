using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnState  : State
{
    public override IEnumerator Enter(){
        yield return null;
        StartCoroutine(WaitThenChangeState<TurnBeginState>());
    }
}