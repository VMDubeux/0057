using System.Collections;
using System.Collections.Generic;
using Main_Folders.Scripts.Units;
using Main_Folders.Scripts.Visuals;
using UnityEngine;

public class TargetPlayer : MonoBehaviour, ITarget
{
    public IEnumerator GetTargets(List<object> targets){
        foreach (BattleVisuals battleVisuals in StateMachine.Instance.Units)
        {
            if (battleVisuals.HP > 0 && battleVisuals is PlayerUnit)
            {
                targets.Add(battleVisuals);
                Debug.Log(battleVisuals + " é o Target");
            }
        }
        yield return null;
    }
}
