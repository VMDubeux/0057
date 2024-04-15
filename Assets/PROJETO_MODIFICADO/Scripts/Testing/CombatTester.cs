using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTester : MonoBehaviour
{
    public static CombatTester Instance;
    public Unit Attacker;
    public Unit Defender;
    void Awake(){
        Instance = this;
    }
    [ContextMenu("Switch Units")]
    void SwitchUnits(){
        Unit temp = Attacker;
        Attacker = Defender;
        Defender = temp;
    }
}
