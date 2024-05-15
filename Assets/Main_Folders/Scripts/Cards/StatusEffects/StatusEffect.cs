using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : MonoBehaviour
{
    public int Duration;
    public int Amount;
    public bool StacksIntensity;
    public bool StacksDuration;
    protected BattleVisuals _host;
    int _currentDuration;
    void OnEnable(){
        _host = GetComponentInParent<BattleVisuals>();
        if(Duration>0){
            _currentDuration = Duration;
            _host.onBattleVisualsTakeTurn+=DurationCountdown;
        }
        Invoke("OnInflicted", 1);
    }
    void OnDisable(){
        OnRemoved();
    }
    public abstract void OnInflicted();
    public abstract void OnRemoved();
    public virtual void OnDurationEnded(){
        _host.onBattleVisualsTakeTurn -= DurationCountdown;
        Destroy(this.gameObject);
    }
    void DurationCountdown(BattleVisuals unit){
        _currentDuration--;
        if(_currentDuration<=0){
            OnDurationEnded();
        }
    }
}
