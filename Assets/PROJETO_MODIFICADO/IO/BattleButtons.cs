using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleButtons : MonoBehaviour
{
    public void EndTurn(){
        StateMachine.Instance.ChangeState<EndTurnState>();
    }
}
