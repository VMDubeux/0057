using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableUnit : StatusEffect
{
    ModifierTags Tag = ModifierTags.WhenUnitDies;
    public override void OnInflicted(){
            _host.Modify[(int)Tag] += Change;
    }
    public override void OnRemoved(){
            _host.Modify[(int)Tag] -= Change;
    }
    void Change(ModifiedValues modifiedValues){
        SpriteRenderer sr = GetComponentInParent<SpriteRenderer>();
        if(sr!=null){
            sr.enabled = !sr.enabled;
        }
        Collider2D coll = GetComponentInParent<Collider2D>();
        if(coll!=null){
            coll.enabled = !coll.enabled;
        }
    }
}
