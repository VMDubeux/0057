using Main_Folders.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameJuiceEnemies : GameJuices
{
    [SerializeField] private QuestLacaio lacaioQuests; // Array com 3 posições
    [SerializeField] private CardToPickUp[] allAvailableCards;
    [SerializeField] private CardToPickUp.CardRarity[] _DroppableCardsRarity;
    [SerializeField] private List<CardToPickUp> _CardsToDrop;
    [Tooltip("Apenas deixe selecionado se o NPC, quando derrotado, tiver que fornecer Carta")]
    [SerializeField] private bool isCardDroppable = false;

    private bool itemDelivered = false; // Controla se o item já foi entregue

    protected override void Start()
    {
        InitializeDroppableCards();

        if (lacaioQuests == null)
        {
            Debug.LogError("A QuestLacaio não foi atribuída ao GameJuiceEnemies. Verifique no editor.");
        }
    }

    private void InitializeDroppableCards()
    {
        _CardsToDrop = new List<CardToPickUp>();

        if (CardInventoryManager.Instance == null || CardInventoryManager.Instance.cardsToPick == null)
        {
            Debug.LogError("CardInventoryManager.Instance ou cardsToPick não está inicializado!");
            return;
        }

        allAvailableCards = CardInventoryManager.Instance.cardsToPick;

        foreach (var card in allAvailableCards)
        {
            if (System.Array.Exists(_DroppableCardsRarity, rarity => rarity == card.cardRarity))
            {
                Debug.Log($"Carta adicionada: {card.Name}, Raridade: {card.cardRarity}");
                _CardsToDrop.Add(card);
            }
        }

        Debug.Log($"Total de cartas dropáveis configuradas: {_CardsToDrop.Count}");
    }

    internal override void AddRandomItemToInventory()
    {
        if (isCardDroppable == false) return;

        if (_CardsToDrop != null && _CardsToDrop.Count > 0)
        {
            int randomIndex = Random.Range(0, _CardsToDrop.Count);
            CardToPickUp randomSelectedCardToPick = _CardsToDrop[randomIndex];

            Debug.Log($"Carta selecionada: {randomSelectedCardToPick.Name} (Posição no inventário: {randomSelectedCardToPick.InventoryPos}).");

            CardInventoryManager.Instance.CardPickedUp(randomSelectedCardToPick);

            StartCoroutine(CanvasCardDropped());
        }
        else
        {
            Debug.LogWarning("A lista '_CardsToDrop' está vazia ou não foi inicializada.");
        }
    }

    protected override void HandleTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !gameObject.GetComponent<Unit>().hasFought)
        {
            isInside = true;
        }
    }

    protected override void HandleTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInside = false;

            // Finaliza interações
            LevelsManager.Instance.isTalking = false;

            // Garante que o diálogo ativo seja destruído
            if (lacaioQuests != null)
            {
                GameObject activeDialog = lacaioQuests.GetActiveDialog();
                if (activeDialog != null)
                {
                    Destroy(activeDialog);
                }
            }
        }
    }

    protected override IEnumerator IsInside()
    {
        if (isInside == true)
        {
            HandleButtonPress();
            yield return new WaitForSeconds(2.5f);
            PerformDelegate();
        }
    }

    protected override void HandleButtonPress()
    {
        // Inicia o diálogo associado à QuestLacaio
        if (lacaioQuests != null)
        {
            lacaioQuests.StartDialogue();
        }
        else
        {
            Debug.LogWarning("A QuestLacaio não está configurada. Certifique-se de que ela foi atribuída.");
        }
    }

    protected override void SetupReturnToOrigin()
    {
        Debug.LogWarning("SetupReturnToOrigin não foi implementado.");
    }
}
