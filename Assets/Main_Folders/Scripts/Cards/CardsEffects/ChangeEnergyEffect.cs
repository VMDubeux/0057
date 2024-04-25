using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEnergyEffect : CardEffect
{
    public int Value;
    public override IEnumerator Apply(List<object> targets){
        foreach(object o in targets){
            PlayerUnit player = o as PlayerUnit;
            player.CurrentEnergy+= Value;
            yield return null;
        }
    }
}
