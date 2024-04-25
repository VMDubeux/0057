using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagModifierStatusEffect : StatusEffect
{
    public ModifierTags Tag;
    public bool isPercentage;
    public override void OnInflicted(){
        if(isPercentage)
            _host.Modify[(int)Tag] += PercentageEffect;
        else
            _host.Modify[(int)Tag] += ValueChangeEffect;
    }
    public override void OnRemoved(){
        if(isPercentage)
            _host.Modify[(int)Tag] -= PercentageEffect;
        else
            _host.Modify[(int)Tag] -= ValueChangeEffect;
    }
    void PercentageEffect(ModifiedValues modifiedValues){
        float amount = Amount;
        float baseValue = modifiedValues.BaseValue;
        modifiedValues.FinalValue += Mathf.FloorToInt(baseValue*(amount/100));
    }
    void ValueChangeEffect(ModifiedValues modifiedValues){
        modifiedValues.FinalValue += Amount;
    }
}
