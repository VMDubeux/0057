using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAllEnemies : MonoBehaviour, ITarget
{
    public IEnumerator GetTargets(List<object> targets)
    {
        foreach (BattleVisuals battleVisuals in StateMachine.Instance.Units)
        {
            if (battleVisuals.CompareTag("Enemies"))
            {
                if (battleVisuals.HP > 0)
                {
                    targets.Add(battleVisuals);
                }
            }
        }
        yield return null;
    }
}
