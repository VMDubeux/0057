using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardHolder : MonoBehaviour
{
    public const float CardMoveDuration = 0.5f;
    public const float CardRotateDuration = 0.5f;
    public List<Card> Cards;
    public RectTransform Holder;
    public Text CardAmount;
    public int CardRotation;
    void Awake(){
        Cards = new List<Card>();
        CardAmount.text = string.Format("{0}", Cards.Count);
    }
    public void AddCard(Card card){
        RectTransform rect = card.transform as RectTransform;
        rect.anchorMax = Holder.anchorMax;
        rect.anchorMin = Holder.anchorMin;
        rect.pivot = Holder.pivot;
        CardHolder oldHolder = card.GetComponentInParent<CardHolder>();
        rect.SetParent(this.transform);
        
        card.Rotate(oldHolder.CardRotation - CardRotation, CardRotateDuration);
        card.Move(Holder.anchoredPosition3D, CardMoveDuration, ()=>{
            Cards.Add(card);
            CardAmount.text = ""+Cards.Count;
            card.transform.SetParent(Holder);
        });
    }
    public void RemoveCard(Card card){
        Cards.Remove(card);
        CardAmount.text = string.Format("{0}", Cards.Count);
    }
    public void SetInitialRotation(){
        foreach(Card card in Cards){
            RectTransform rect = card.transform as RectTransform;
            rect.rotation = Quaternion.Euler(0, CardRotation, 0);
        }
    }

}
