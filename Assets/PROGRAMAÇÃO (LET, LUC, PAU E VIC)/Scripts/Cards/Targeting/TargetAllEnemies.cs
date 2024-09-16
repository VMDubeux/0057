using System.Collections;
using System.Collections.Generic;
using Main_Folders.Scripts.Visuals;
using UnityEngine;

public class TargetAllEnemies : MonoBehaviour, ITarget
{
    public static int enemiesInBattle;
    public IEnumerator GetTargets(List<object> targets)
    {
        enemiesInBattle = 0;
        foreach (BattleVisuals battleVisuals in StateMachine.Instance.Units)
        {
            enemiesInBattle++;
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
