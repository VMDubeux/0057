using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTester : MonoBehaviour
{
    public Card Card;
    [ContextMenu("Draw")]
    void DrawCard(){
        StartCoroutine(CardsController.Instance.Draw());
    }
    [ContextMenu("Discard")]
    void RemoveCard(){
        CardsController.Instance.Discard(Card);
    }
    [ContextMenu("Shuffle")]
    void Shuffle(){
        StartCoroutine(CardsController.Instance.ShuffleDiscardIntoDeck());
    }
}
