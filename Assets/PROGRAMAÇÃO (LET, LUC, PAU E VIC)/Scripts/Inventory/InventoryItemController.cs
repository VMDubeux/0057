using Main_Folders.Scripts.Managers;
using Main_Folders.Scripts.Units;
using UnityEngine;
using UnityEngine.UI;

namespace Main_Folders.Scripts.Inventory
{
    public class InventoryItemController : MonoBehaviour
    {
        [SerializeField] private InventoryItem ItemInventory;
        [SerializeField] private ItemPickUp ItemPickUp;

        private void Start()
        {
            gameObject.GetComponent<Button>().onClick
                .AddListener(delegate { UseItem(ItemPickUp); }); // Chama método de uso dos itens
            
            DontDestroyOnLoad(this);
        }

        public void RemoveItem()
        {
            //Button RemoveButton = transform.GetComponentInChildren<Button>();
            InventoryManager.Instance.Remove(ItemPickUp);
            //Item.Destroy();
            //Destroy(gameObject);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public void AddItem(InventoryItem newItem, ItemPickUp Item)
        {
            ItemInventory = newItem;
            ItemPickUp = Item;
            DontDestroyOnLoad(gameObject);
        }

        public void UseItem(ItemPickUp item)
        {
            GameObject battleVisual = GameObject.FindAnyObjectByType<PartyManager>().allMember[0].gameObject;
            PartyManager partyManager = GameObject.FindAnyObjectByType<PartyManager>();

            switch (item.ItemType)
            {
                default:
                case ItemType.CartaComum:
                    Debug.Log($"Carta Comum enviada ao Deck");
                    InventoryManager.Instance.Remove(item);
                    break;
                case ItemType.CartaEsp:
                    Debug.Log($"Carta Especial enviada ao Deck");
                    InventoryManager.Instance.Remove(item);
                    break;
                case ItemType.CartaMed:
                    Debug.Log($"Carta Média enviada ao Deck");
                    InventoryManager.Instance.Remove(item);
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

        void ChangeValues(ref int valueRef, int valueChange)
        {
            valueRef += valueChange;
        }
    }
}