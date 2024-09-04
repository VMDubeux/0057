using System.Collections;
using System.Collections.Generic;
using Main_Folders.Scripts.Visuals;
using UnityEngine;

public class TargetUnit : MonoBehaviour, ITarget
{
    //Unit _clickedUnit;
    BattleVisuals _clickedVisuals;

    public IEnumerator GetTargets(List<object> targets){
        _clickedVisuals = null;

        foreach(BattleVisuals battleVisuals in StateMachine.Instance.Units){
            battleVisuals.onBattleVisualsClicked += OnBattleVisualsClicked;
        }

        while(_clickedVisuals == null){
            yield return null;
        }
        targets.Add(_clickedVisuals);

        foreach(BattleVisuals battleVisuals in StateMachine.Instance.Units){
            battleVisuals.onBattleVisualsClicked -= OnBattleVisualsClicked;
        }
        Debug.Log(_clickedVisuals);
    }

    void OnBattleVisualsClicked(BattleVisuals battleVisuals) 
    {
        _clickedVisuals = battleVisuals;
    }

    /*void OnUnitClicked(Unit unit){
        _clickedUnit = unit;
    }*/
}