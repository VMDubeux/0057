using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsList : MonoBehaviour
{
    public List<Card> Cards;
    private bool isInitialized = false;
    private const int TotalDeckSize = 15;

    private void Start()
    {
        if (!isInitialized)
        {
            InitializeDeck();
            isInitialized = true;
        }
    }

    private void InitializeDeck()
    {
        if (CardInventoryManager.Instance == null || CardInventoryManager.Instance.chosenCards == null)
        {
            return;
        }

        Cards = new List<Card>();

        List<Card> uniqueCards = new List<Card>();
        foreach (CardToPickUp card in CardInventoryManager.Instance.chosenCards)
        {
            if (card == null || card.CardConvertClass == null)
            {
                continue;
            }

            uniqueCards.Add(card.CardConvertClass);
        }

        if (uniqueCards.Count == 0)
        {
            return;
        }

        int cardsPerType = TotalDeckSize / uniqueCards.Count;
        int remainingCards = TotalDeckSize % uniqueCards.Count;

        foreach (Card card in uniqueCards)
        {
            for (int i = 0; i < cardsPerType; i++)
            {
                Cards.Add(card);
            }
        }

        for (int i = 0; i < remainingCards; i++)
        {
            Cards.Add(uniqueCards[i]);
        }
    }
}