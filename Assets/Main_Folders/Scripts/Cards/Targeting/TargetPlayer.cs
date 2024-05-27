using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlayer : MonoBehaviour, ITarget
{
    public IEnumerator GetTargets(List<object> targets){
        foreach (BattleVisuals battleVisuals in StateMachine.Instance.Units)
        {
            if (battleVisuals.HP > 0)
            {
                targets.Add(battleVisuals);
            }
        }
        yield return null;
    }
}
