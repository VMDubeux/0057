using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main_Folders.Scripts.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance { get; private set; }

        [Header("Itens do Inventário")]
        [SerializeField] private ItemPickUp[] items = new ItemPickUp[10];
        [SerializeField] private InventoryItem[] inventoryList = new InventoryItem[10];

        [Header("Referências na HUD")]
        [SerializeField] private GameObject inventory;
        [SerializeField] private Transform[] contentTransforms = new Transform[2]; // 0: Itens, 1: Cartas
        [SerializeField] private GameObject inventoryItemBackground;
        [SerializeField] private Toggle enableRemove;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void Add(ItemPickUp itemPickUp)
        {
            if (itemPickUp == null) return;

            if (items[itemPickUp.Id] == null)
            {
                Debug.Log("Criou objeto!");
                items[itemPickUp.Id] = itemPickUp;
                CreateInventoryItem(itemPickUp);
            }
            else
            {
                Debug.Log("Aumentou quantidade!");
                UpdateItemQuantity(itemPickUp, items[itemPickUp.Id].InventoryItem.itemQuantity + 1);
            }
        }

        public void Remove(ItemPickUp itemPickUp)
        {
            if (itemPickUp == null || items[itemPickUp.Id] == null) return;

            if (items[itemPickUp.Id].InventoryItem.itemQuantity > 1)
            {
                Debug.Log("Removeu quantidade!");
                UpdateItemQuantity(itemPickUp, items[itemPickUp.Id].InventoryItem.itemQuantity - 1);
            }
            else
            {
                Debug.Log("Removeu objeto!");
                DestroyInactiveItem(itemPickUp);
                ClearItemData(itemPickUp);
            }
        }

        private void UpdateItemQuantity(ItemPickUp itemPickUp, int newQuantity)
        {
            items[itemPickUp.Id].InventoryItem.itemQuantity = newQuantity;
            items[itemPickUp.Id].InventoryItem.pathNumber.text = newQuantity.ToString();
        }

        private void DestroyInactiveItem(ItemPickUp itemPickUp)
        {
            ItemPickUp[] search = FindObjectsOfType<ItemPickUp>(true);
            foreach (var item in search)
            {
                if (item.ItemType == itemPickUp.ItemType && !item.isActiveAndEnabled)
                {
                    item.DestroyIt();
                    break; // Assuming only one item needs to be destroyed.
                }
            }
        }

        private void ClearItemData(ItemPickUp itemPickUp)
        {
            items[itemPickUp.Id] = null;
            if (inventoryList[itemPickUp.InventoryItem.id] != null)
            {
                inventoryList[itemPickUp.InventoryItem.id].inventoryItemController.Destroy();
                inventoryList[itemPickUp.InventoryItem.id] = null;
            }
        }

        private void CreateInventoryItem(ItemPickUp itemPickUp)
        {
            Transform content = GetContentForItem(itemPickUp.ItemType);
            GameObject obj = Instantiate(inventoryItemBackground, content);

            InventoryItem item = new InventoryItem
            {
                type = itemPickUp.ItemType,
                pathName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>(),
                pathNumber = obj.transform.Find("NumberText").GetComponent<TextMeshProUGUI>(),
                pathIcon = obj.transform.Find("ItemIcon").GetComponent<Image>(),
                pathRemoveButton = obj.transform.Find("RemoveButton").GetComponent<Button>()
            };

            item.pathName.text = ItemSO.GetName(itemPickUp.ItemType);
            item.pathIcon.sprite = ItemSO.GetSprite(itemPickUp.ItemType);
            item.id = ItemSO.GetId(itemPickUp.ItemType);
            item.itemQuantity = 1;
            item.pathNumber.text = item.itemQuantity.ToString();

            itemPickUp.InventoryItem = item;
            inventoryList[item.id] = item;

            item.inventoryItemController = obj.GetComponent<InventoryItemController>();
            item.inventoryItemController.AddItem(item, itemPickUp);
        }

        private Transform GetContentForItem(ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.CartaComum:
                case ItemType.CartaMed:
                case ItemType.CartaEsp:
                    return contentTransforms[1]; // Cartas
                default:
                    return contentTransforms[0]; // Itens
            }
        }

        public void EnableItemRemove()
        {
            bool isActive = enableRemove.isOn;
            foreach (Transform item in contentTransforms[0])
            {
                item.Find("RemoveButton").gameObject.SetActive(isActive);
            }
            foreach (Transform item in contentTransforms[1])
            {
                item.Find("RemoveButton").gameObject.SetActive(isActive);
            }
        }

        private void Update()
        {
            if (!inventory.activeSelf)
            {
                enableRemove.isOn = false;
            }
        }
    }

    [System.Serializable]
    public class InventoryItem
    {
        public ItemType type;
        public int id;
        public TextMeshProUGUI pathName;
        public TextMeshProUGUI pathNumber;
        public Image pathIcon;
        public Button pathRemoveButton;
        public int itemQuantity;
        public InventoryItemController inventoryItemController;
    }
}
