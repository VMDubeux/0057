using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEffect : CardEffect
{
    public int Amount;
    public override IEnumerator Apply(List<object> targets)
    {
        foreach (Object o in targets)
        {
            BattleVisuals unit = o as BattleVisuals;

            ModifiedValues modifiedValues = new ModifiedValues(Amount);
            ApplyModifier(modifiedValues, ModifierTags.GainBlock, StateMachine.Instance.CurrentUnit);


            Debug.LogFormat("Unit {0} gained {1} block", unit.name, modifiedValues.FinalValue);
            int currentBlock = unit.GetStatValue(3);
            unit.SetStatValue(3, currentBlock + modifiedValues.FinalValue);

            yield return null;
        }
    }
    void ApplyModifier(ModifiedValues modifiedValues, ModifierTags tag, BattleVisuals unit)
    {
        TagModifier modifier = unit.Modify[(int)tag];
        if (modifier != null)
        {
            modifier(modifiedValues);
        }
    }
}
