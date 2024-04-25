using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : MonoBehaviour
{
    public int Duration;
    public int Amount;
    public bool StacksIntensity;
    public bool StacksDuration;
    protected Unit _host;
    int _currentDuration;
    void OnEnable(){
        _host = GetComponentInParent<Unit>();
        if(Duration>0){
            _currentDuration = Duration;
            _host.onUnitTakeTurn+=DurationCountdown;
        }
        Invoke("OnInflicted", 1);
    }
    void OnDisable(){
        OnRemoved();
    }
    public abstract void OnInflicted();
    public abstract void OnRemoved();
    public virtual void OnDurationEnded(){
        _host.onUnitTakeTurn-=DurationCountdown;
        Destroy(this.gameObject);
    }
    void DurationCountdown(Unit unit){
        _currentDuration--;
        if(_currentDuration<=0){
            OnDurationEnded();
        }
    }
}
