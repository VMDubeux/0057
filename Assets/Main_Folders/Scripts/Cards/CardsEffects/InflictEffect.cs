using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InflictEffect : CardEffect
{
    public StatusEffect StatusEffectPrefab;
    public int AppliesXTimes;
    public override IEnumerator Apply(List<object> targets){
        foreach(Object o in targets){
            Unit unit = o as Unit;
            for(int i=0; i<AppliesXTimes; i++){
                TryToApply(unit);
            }
            
            yield return null;
        }
    }
    void TryToApply(Unit unit){
        StatusEffect status = GetEffect(unit);
        if(status == null){
            status = CreateEffect(unit);
        }else{
            if(StatusEffectPrefab.StacksDuration){
                status.Duration += StatusEffectPrefab.Duration;
            }
            if(StatusEffectPrefab.StacksIntensity){
                status.Amount += StatusEffectPrefab.Amount;
            }
        }
    }
    StatusEffect GetEffect(Unit unit){
        foreach(StatusEffect status in unit.GetComponentsInChildren<StatusEffect>()){
            if(status.name == StatusEffectPrefab.name){
                return status;
            }
        }
        return null;
    }
    StatusEffect CreateEffect(Unit unit){
        StatusEffect instantiated = Instantiate(StatusEffectPrefab, Vector3.zero, Quaternion.identity, unit.transform);
        instantiated.name = StatusEffectPrefab.name;
        return instantiated;
    }
}