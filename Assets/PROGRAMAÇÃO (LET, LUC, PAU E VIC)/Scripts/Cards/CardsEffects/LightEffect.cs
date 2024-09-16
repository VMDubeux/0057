using System.Collections;
using System.Collections.Generic;
using Main_Folders.Scripts.Cards.CardsEffects;
using Main_Folders.Scripts.Units;
using Main_Folders.Scripts.Visuals;
using UnityEngine;

public class LightEffect : CardEffect
{
    public int luz = 0;
    public override IEnumerator Apply(List<object> targets)
    {
        DamageEffect.lightQuantity = luz;
        yield return null;
    }
}