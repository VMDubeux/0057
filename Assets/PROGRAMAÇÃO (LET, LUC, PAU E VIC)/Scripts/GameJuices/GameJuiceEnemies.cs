using Main_Folders.Scripts.Managers;
using Main_Folders.Scripts.Minimapa;
using Main_Folders.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
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

        StartCoroutine(InitializeAfterDelay());
    }

    private IEnumerator InitializeAfterDelay()
    {
        yield return new WaitForEndOfFrame(); // Garante que tudo seja inicializado antes

        if (PlayerPrefs.GetInt(_assetKey, 0) == 1)
        {
            HandleQuestAlreadyCompleted();
        }
        else
        {
            InitializeDroppableCards();
            CanvasCardDroppedMessage = FindFirstObjectByType<CanvasMessageCard>(FindObjectsInactive.Include).transform.GetChild(0).gameObject;
            if (gameObject.name != "PartyManager")
                FindFirstObjectByType<CanvasMinimapa>(FindObjectsInactive.Include).transform.GetChild(0).GetComponent<MarkerHolder>()?.AddEnemyMarker(this.gameObject);
        }
    }

    private void HandleQuestAlreadyCompleted()
    {
        wasOpen = true;

        lacaioQuests.isAvailable = true;
        lacaioQuests.isCompleted = true;
        gameObject.GetComponent<Unit>().hasFought = true;

        var holder = Resources.FindObjectsOfTypeAll<MarkerHolder>();
        if (holder == null) return;

        foreach (var h in holder)
        {
            h.RemoveEnemyMarker(this.gameObject);
        }

        if (lacaioQuests.questOutputs != null)
        {
            // Lógica padrão: Ativar ou manipular objetos de saída
            foreach (var output in lacaioQuests.questOutputs)
            {
                if (output != null)
                {
                    output.SetActive(false);
                }
            }

            lacaioQuests.questOutputs.Clear();
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

            PlayerPrefs.SetInt(_assetKey, 1);

            wasOpen = true;

            StartCoroutine(CanvasCardDropped());
        }
        else
        {
            Debug.LogWarning("A lista '_CardsToDrop' está vazia ou não foi inicializada.");
        }
    }

    protected override void HandleTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !gameObject.GetComponent<Unit>().hasFought && !wasOpen)
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
