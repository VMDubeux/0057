using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : CardEffect
{
    public int Amount;
    public override IEnumerator Apply(List<object> targets){
        foreach(Object o in targets){
            Unit unit = o as Unit;

            ModifiedValues modifiedValues = new ModifiedValues(Amount);
            ApplyModifier(modifiedValues, ModifierTags.DoAttackDamage, StateMachine.Instance.CurrentUnit);
            ApplyModifier(modifiedValues, ModifierTags.TakeAttackDamage, unit);

            int block = unit.GetStatValue(StatType.Block);
            int leftoverBlock = Mathf.Max(0, block - modifiedValues.FinalValue);
            unit.SetStatValue(StatType.Block, leftoverBlock);

            int currentHP = unit.GetStatValue(StatType.HP);
            int leftoverDamage = Mathf.Max(0, modifiedValues.FinalValue - block);
            unit.SetStatValue(StatType.HP, Mathf.Max(0, currentHP-leftoverDamage));

            Debug.LogFormat("Unit {0} HP went from {1} to {2}; block went from {3} to {4} ", 
                unit.name, currentHP, unit.GetStatValue(StatType.HP), block, leftoverBlock);
            yield return null;
            if(unit.GetStatValue(StatType.HP)<=0){
                unit.Modify[(int)ModifierTags.WhenUnitDies](null);
            }
        }
    }
    void ApplyModifier(ModifiedValues modifiedValues, ModifierTags tag, Unit unit){
        TagModifier modifier = unit.Modify[(int)tag];
        if(modifier!=null){
            modifier(modifiedValues);
        }
    }
}
