using Main_Folders.Scripts.Managers;
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
            InventoryManager.Instance.Remove(item);
            GameObject battleVisual = GameObject.FindAnyObjectByType<PartyManager>().allMember[0].gameObject;

            switch (item.ItemType)
            {
                default:
                case ItemType.PerfumePeq:
                    ChangeValues(ref battleVisual.GetComponent<Unit>()._stats[0].Value, 20);
                    Debug.Log($"Aumentou HP Máximo: {battleVisual.GetComponent<Unit>()._stats[0].Value}");
                    break;
                case ItemType.PerfumeMed:
                    if (battleVisual.GetComponent<PlayerUnit>().DrawAmount == 7) return;
                    ChangeValues(ref battleVisual.GetComponent<PlayerUnit>().DrawAmount, 1);
                    Debug.Log($"Aumentou Draw Amount: {battleVisual.GetComponent<PlayerUnit>().DrawAmount}");
                    break;
                case ItemType.PerfumeGrd:
                    ChangeValues(ref battleVisual.GetComponent<PlayerUnit>().MaxEnergy, 1);
                    Debug.Log($"Aumentou Max Energy: {battleVisual.GetComponent<PlayerUnit>().MaxEnergy}");
                    break;
                case ItemType.CartaComum:
                    Debug.Log("Adicionar carta ao inventário de cartas");
                    break;
            }
        }

        void ChangeValues(ref int valueRef, int valueChange)
        {
            valueRef += valueChange;
        }
    }
}