using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [SerializeField] private ItemPickUp[] Items;

    [Header("Referencias na HUD")]
    public GameObject Inventory;
    public Transform ItemContent;
    public GameObject InventoryItemBackground;
    public Toggle EnableRemove;

    [SerializeField] private InventoryItem[] InventoryList;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        Items = new ItemPickUp[5];
        InventoryList = new InventoryItem[5];
    }

    public void Add(ItemPickUp itemPickUp)
    {
        if (Items[itemPickUp.Id] == null)
        {
            Debug.Log("Criou objeto!");
            Items[itemPickUp.Id] = itemPickUp;
            CreateInventoryItem(itemPickUp);
        }
        else
        {
            Debug.Log("Aumentou quantidade!");
            Items[itemPickUp.Id].InventoryItem.itemQuantity++;
            Items[itemPickUp.Id].InventoryItem.pathNumber.text = Items[itemPickUp.Id].InventoryItem.itemQuantity.ToString();
        }
    }

    public void Remove(ItemPickUp itemPickUp)
    {
        if (Items[itemPickUp.Id].InventoryItem.itemQuantity > 1)
        {
            Debug.Log("Removeu quantidade!");
            Items[itemPickUp.Id].InventoryItem.itemQuantity--;
            Items[itemPickUp.Id].InventoryItem.pathNumber.text = Items[itemPickUp.Id].InventoryItem.itemQuantity.ToString();
        }
        else
        {
            Debug.Log("Removeu objeto!");
            ItemPickUp[] search = FindObjectsByType<ItemPickUp>(FindObjectsInactive.Include,FindObjectsSortMode.InstanceID);
            Debug.Log(search.Length);
            for (int i = 0; i < search.Length; i++)
            {
                if (search[i].ItemType == itemPickUp.ItemType && !search[i].isActiveAndEnabled)
                {
                    Debug.Log("ENTROU!");
                    search[i].DestroyIt();
                }
            }
            Items[itemPickUp.Id] = null;
            InventoryList[itemPickUp.InventoryItem.id].inventoryItem.Destroy();
            InventoryList[itemPickUp.InventoryItem.id] = null;
            //itemPickUp.DestroyIt();
        }
    }

    public void CreateInventoryItem(ItemPickUp itemPickUp)
    {
        GameObject obj = Instantiate(InventoryItemBackground, ItemContent);

        InventoryItem Item = new()
        {
            type = itemPickUp.ItemType,
            pathName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>(),
            pathNumber = obj.transform.Find("NumberText").GetComponent<TextMeshProUGUI>(),
            pathIcon = obj.transform.Find("ItemIcon").GetComponent<Image>(),
            pathRemoveButton = obj.transform.Find("RemoveButton").GetComponent<Button>()
        };

        Item.pathName.text = ItemSO.GetName(itemPickUp.ItemType);
        Item.pathIcon.sprite = ItemSO.GetSprite(itemPickUp.ItemType);
        Item.id = ItemSO.GetId(itemPickUp.ItemType);
        Item.itemQuantity++;
        Item.pathNumber.text = Item.itemQuantity.ToString();

        itemPickUp.InventoryItem = Item;

        InventoryList[Item.id] = Item;

        Item.inventoryItem = obj.GetComponent<InventoryItemController>();

        Item.inventoryItem.AddItem(Item, itemPickUp);
    }

    /*
    public void ListItems()
    {

        // Limpar itens do inventario
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (ItemPickUp o in Items) // Adicionar lista de itens ao inventario
        {
            Debug.Log(Items.Count);

            GameObject obj = Instantiate(InventoryItemBackground, ItemContent);
            InventoryItem Item = new();

            Item.type = o.ItemType;
            Item.pathName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            Item.pathNumber = obj.transform.Find("NumberText").GetComponent<TextMeshProUGUI>();
            Item.pathIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            Item.pathRemoveButton = obj.transform.Find("RemoveButton").GetComponent<Button>();

            Item.pathName.text = ItemSO.GetName(o.ItemType);
            Item.pathIcon.sprite = ItemSO.GetSprite(o.ItemType);
            Item.gameObjects = ItemSO.GetGameObject(o.ItemType);
            Item.itemQuantity++;
            Item.pathNumber.text = Item.itemQuantity.ToString();
            InventoryList.Add(Item);
        }
        SetInventoryItems();
    }
    */

    public void EnableItemRemove()
    {
        if (EnableRemove.isOn)
        {
            foreach (Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(false);
            }
        }
    }

    void InventoryActive()
    {
        if (!Inventory.activeSelf)
        {
            EnableRemove.isOn = false;
        }
    }

    void OnGUI()
    {
        InventoryActive();
    }
}

public class InventoryItem : MonoBehaviour
{
    public ItemType type;
    public int id;
    public TextMeshProUGUI pathName;
    public TextMeshProUGUI pathNumber;
    public Image pathIcon;
    public Button pathRemoveButton;
    public int itemQuantity = 0;
    public InventoryItemController inventoryItem;
}