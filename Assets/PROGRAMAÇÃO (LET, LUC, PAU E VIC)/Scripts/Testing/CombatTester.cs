using System.Collections;
using System.Collections.Generic;
using Main_Folders.Scripts.Visuals;
using UnityEngine;

public class CombatTester : MonoBehaviour
{
    public static CombatTester Instance;
    public BattleVisuals Attacker;
    public BattleVisuals Defender;
    void Awake(){
        Instance = this;
    }
    [ContextMenu("Switch Units")]
    void SwitchUnits(){
        BattleVisuals temp = Attacker;
        Attacker = Defender;
        Defender = temp;
    }
}
