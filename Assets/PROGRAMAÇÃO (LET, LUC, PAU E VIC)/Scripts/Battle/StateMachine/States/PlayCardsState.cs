using System.Collections;
using System.Collections.Generic;
using Main_Folders.Scripts.Units;
using UnityEngine;
using UnityEngine.UI;

public class PlayCardsState : State
{
    public const string PlayedGameObject = "Effects/Played";
    public const string AfterPlayedGameObject = "Effects/AfterPlayed";
    Button _endTurnButton;
    Coroutine _cardSequencer;
    HorizontalLayoutGroup _handLayout;
    PlayerUnit _playerUnit;
    void Awake(){
        _handLayout = CardsController.Instance.Hand.Holder.GetComponent<HorizontalLayoutGroup>();        
    }
    private void Update() {
        if( _playerUnit == null && machine.CurrentUnit.CompareTag("Player"))
        {
            _playerUnit = machine.CurrentUnit as PlayerUnit;
        }
    }
    public override IEnumerator Enter(){
        yield return new WaitForSeconds(0.5f);
        Graciosidade();
        EndTurnButton(true);
        _handLayout.enabled = false;
        _cardSequencer = StartCoroutine(CardSequencer());
    }
    public override IEnumerator Exit(){
        yield return null;
        EndTurnButton(false);
        _handLayout.enabled = true;
        StopCoroutine(_cardSequencer);
    }
    IEnumerator CardSequencer(){
        while(true){
            if(machine.CardsToPlay.Count>0){
                Card card = machine.CardsToPlay.Dequeue();
                Debug.Log("Playing "+card);
                yield return StartCoroutine(PlayCardEffect(card, card.transform.Find(PlayedGameObject)));
                yield return new WaitForSeconds(0.5f);
                yield return StartCoroutine(PlayCardEffect(card, card.transform.Find(AfterPlayedGameObject)));
                yield return new WaitForSeconds(0.5f);
            }
            yield return null;
        }
    }
    IEnumerator PlayCardEffect(Card card, Transform playTransform){
        for(int i=0; i<playTransform.childCount; i++){
            ITarget targeter = playTransform.GetChild(i).GetComponent<ITarget>();
            List<object> targets = new List<object>();
            if(targeter==null){
                continue;
            }
            yield return StartCoroutine(targeter.GetTargets(targets));
            foreach(CardEffect effect in playTransform.GetChild(i).GetComponents<CardEffect>()){
                yield return StartCoroutine(effect.Apply(targets));
            }
        }
    }
    void EndTurnButton(bool interactability){
        if(_endTurnButton==null){
            _endTurnButton = GameObject.Find("Canvas/EndTurnButton").GetComponent<Button>();
        }
        _endTurnButton.interactable = interactability;
    }

        void Graciosidade()
    {
        if(_playerUnit.Graciosidade > 0 && _playerUnit.HP < _playerUnit.MaxHP)
        {
            for(int  i = 0; i < GraciousEffect.graciousHeal; i++)
            {
            _playerUnit.HP++;
            _playerUnit.Graciosidade--;
            Debug.Log("Gracioso !");
            }
        }
    }
}
