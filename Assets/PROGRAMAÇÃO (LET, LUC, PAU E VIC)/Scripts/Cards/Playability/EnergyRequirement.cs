using System.Collections;
using System.Collections.Generic;
using Main_Folders.Scripts.Units;
using UnityEngine;

public class EnergyRequirement : MonoBehaviour, IPlayability
{
    public int Energy;
    public bool CanPlay(){
        PlayerUnit player = StateMachine.Instance.CurrentUnit as PlayerUnit;
        if(player){
            return player.CurrentEnergy >= Energy;
        }
        return false;
    }
}
