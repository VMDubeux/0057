using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBattleState  : State
{
    public override IEnumerator Enter(){
        yield return null;
        Debug.Log("Battle ended");
    }
}