using Main_Folders.Scripts.Managers;
using Main_Folders.Scripts.Units;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.PROGRAMAÇÃO__LET__LUC__PAU_E_VIC_.Scripts.Inventory
{
    public class InventoryItemController : MonoBehaviour
    {
        [SerializeField] private ItemPickUp itemPickUp; // Item atual associado a este controlador
        private InventoryItem itemInventory; // Item de inventário associado a este controlador

        private void Start()
        {
            // Configura o botão para chamar UseItem no clique
            GetComponent<Button>().onClick.AddListener(() => UseItem(itemPickUp));

            DontDestroyOnLoad(gameObject);
        }

        public void RemoveItem()
        {
            Debug.Log($"Removing item: {itemPickUp}");
            InventoryManager.Instance.Remove(itemPickUp);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public void AddItem(InventoryItem newItem, ItemPickUp item)
        {
            itemInventory = newItem; // Atualiza o item de inventário
            itemPickUp = item; // Atualiza o item pick up associado
            DontDestroyOnLoad(gameObject);
        }

        public void UseItem(ItemPickUp item)
        {
            Debug.Log($"Using item: {item}");
            GameObject battleVisual = GameObject.FindAnyObjectByType<PartyManager>().allMember[0].gameObject;
            PartyManager partyManager = GameObject.FindAnyObjectByType<PartyManager>();

            // Cria uma cópia do item para evitar perder a referência
            ItemPickUp itemCopy = Instantiate(item);

            switch (itemCopy.ItemType)
            {
                case ItemType.CartaComum:
                case ItemType.CartaEsp:
                case ItemType.CartaMed:
                    Debug.Log($"{itemCopy.ItemType} enviada ao Deck");
                    AddToDeckCanvas(item);
                    InventoryManager.Instance.Remove(itemCopy);
                    break;

                case ItemType.PerfumePeq:
                    partyManager.SetStatsValues(0, 20);
                    Debug.Log($"Aumentou HP Máximo: {battleVisual.GetComponent<Unit>()._stats[0].Value}");
                    InventoryManager.Instance.Remove(item);
                    break;

                case ItemType.PerfumeMed:
                    if (battleVisual.GetComponent<PlayerUnit>().DrawAmount == 7) return;
                    ChangeValues(ref battleVisual.GetComponent<PlayerUnit>().DrawAmount, 1);
                    partyManager.SetStatsValues(1, 1);
                    Debug.Log($"Aumentou Draw Amount: {battleVisual.GetComponent<PlayerUnit>().DrawAmount}");
                    InventoryManager.Instance.Remove(item);
                    break;

                case ItemType.PerfumeGrd:
                    if (battleVisual.GetComponent<PlayerUnit>().MaxEnergy == 6) return;
                    ChangeValues(ref battleVisual.GetComponent<PlayerUnit>().MaxEnergy, 1);
                    partyManager.SetStatsValues(2, 1);
                    Debug.Log($"Aumentou Max Energy: {battleVisual.GetComponent<PlayerUnit>().MaxEnergy}");
                    InventoryManager.Instance.Remove(item);
                    break;
            }
        }

        private void AddToDeckCanvas(ItemPickUp item)
        {
            // Encontrar o Canvas do Deck, mesmo que esteja inativo
            GameObject deckCanvasObject = GameObject.FindAnyObjectByType<CanvasDeck>(FindObjectsInactive.Include)?.gameObject;

            if (deckCanvasObject == null)
            {
                Debug.LogError("Deck Canvas não encontrado!");
                return;
            }

            // Encontrar os botões do Deck
            DeckItemController[] deckButtons = deckCanvasObject.GetComponentsInChildren<DeckItemController>(true);

            // Adicionar o item ao primeiro botão disponível
            foreach (DeckItemController deckButton in deckButtons)
            {
                if (deckButton.IsEmpty())
                {
                    // Adiciona a referência ao item pick-up
                    deckButton.Setup(item, ItemSO.GetSprite(item.ItemType));
                    break;
                }
            }
        }

        private void ChangeValues(ref int valueRef, int valueChange)
        {
            valueRef += valueChange;
        }
    }
}
