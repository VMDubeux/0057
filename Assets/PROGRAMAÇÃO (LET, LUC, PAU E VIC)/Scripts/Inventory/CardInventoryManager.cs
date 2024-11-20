using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInventoryManager : MonoBehaviour
{
    public static CardInventoryManager Instance;

    public CardToPickUp[] cardsToPick = new CardToPickUp[15];
    internal List<CardToPickUp> cardsPickedUp = new(), chosenCards = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (cardsToPick == null || cardsToPick.Length == 0)
        {
            Debug.LogWarning("cardsToPick está vazio! Verifique se as cartas foram configuradas no Inspector.");
        }
        else
        {
            Debug.Log($"cardsToPick inicializado com {cardsToPick.Length} cartas.");
        }

        foreach (var card in cardsToPick)
        {
            if (card != null)
            {
                card.transform.GetChild(0).GetComponent<Image>().sprite =
                    CardToPickUp.GetSprite(card.cardType);

                var toggle = card.gameObject.GetComponent<Toggle>();
                if (toggle != null)
                {
                    toggle.onValueChanged.AddListener(isOn => HandleToggleChange(card, isOn));
                }
            }
        }
    }

    public void CardPickedUp(CardToPickUp cardPickedUp)
    {
        if (cardPickedUp.InventoryPos < 0 || cardPickedUp.InventoryPos >= cardsToPick.Length)
        {
            Debug.LogError($"Índice inválido: {cardPickedUp.InventoryPos}. Verifique o campo InventoryPos no CardToPickUp.");
            return;
        }

        var cardImage = cardsToPick[cardPickedUp.InventoryPos].transform.GetChild(0).GetComponent<Image>();
        if (cardImage == null)
        {
            Debug.LogError($"Componente Image não encontrado no filho do card de índice {cardPickedUp.InventoryPos}.");
            return;
        }

        Color currentColor = cardImage.color;
        currentColor.a = 1f;
        cardImage.color = currentColor;

        Debug.Log($"Carta '{cardPickedUp.Name}' (Posição: {cardPickedUp.InventoryPos}) teve o alpha aumentado para {currentColor.a}.");

        cardsToPick[cardPickedUp.InventoryPos].gameObject.GetComponent<Toggle>().interactable = true;

        cardsPickedUp.Add(cardsToPick[cardPickedUp.InventoryPos]);
    }

    private void HandleToggleChange(CardToPickUp card, bool isOn)
    {
        if (isOn)
        {
            if (!chosenCards.Contains(card))
            {
                chosenCards.Add(card);
                Debug.Log($"Carta adicionada ao deck: {card.Name}");
            }
        }
        else
        {
            if (chosenCards.Contains(card))
            {
                chosenCards.Remove(card);
                Debug.Log($"Carta removida do deck: {card.Name}");
            }
        }
    }
}
