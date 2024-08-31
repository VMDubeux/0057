using Main_Folders.Scripts.Managers;
using Main_Folders.Scripts.Units;
using UnityEngine;
using UnityEngine.UI;

namespace Main_Folders.Scripts.Inventory
{
    public class InventoryCardController : MonoBehaviour
    {
        [SerializeField] private InventoryItem ItemInventory;
        [SerializeField] private ItemPickUp ItemPickUp;

        private void Start()
        {
            gameObject.GetComponent<Button>().onClick
                .AddListener(delegate { UseItem(ItemPickUp); }); // Chama m�todo de uso dos itens

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
                    Debug.Log("Adicionar carta comum ao invent�rio de cartas");
                    InventoryManager.Instance.Remove(item);
                    break;
                case ItemType.CartaMed:
                    Debug.Log("Adicionar carta m�dia ao invent�rio de cartas");
                    InventoryManager.Instance.Remove(item);
                    break;
                case ItemType.CartaEsp:
                    Debug.Log("Adicionar carta especial ao invent�rio de cartas");
                    InventoryManager.Instance.Remove(item);
                    break;
            }
        }
    }
}