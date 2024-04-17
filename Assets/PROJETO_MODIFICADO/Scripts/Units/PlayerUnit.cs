using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUnit : Unit
{
    public int MaxEnergy;
    public int CurrentEnergy {
        get{
            return _currentEnergy;
        }
        set{
            //modify/add/control 
            _currentEnergy = value;
            UpdateEnergyMeter();
        }
    }
    [SerializeField]
    int _currentEnergy;
    public int DrawAmount;
    public int MaxCards;
    Text _energyMeter;
    void Awake(){
        _energyMeter = GameObject.Find("Canvas/EnergyMeter").GetComponent<Text>();
    }
    public override IEnumerator Recover(){
        yield return StartCoroutine(base.Recover());
        CurrentEnergy = MaxEnergy;
        int cardsToDraw = Mathf.Min(MaxCards - CardsController.Instance.Hand.Cards.Count, DrawAmount);
        yield return StartCoroutine(CardsController.Instance.Draw(cardsToDraw));
    }
    void UpdateEnergyMeter(){
        _energyMeter.text = string.Format("{0}/{1}", CurrentEnergy, MaxEnergy);
    }
}