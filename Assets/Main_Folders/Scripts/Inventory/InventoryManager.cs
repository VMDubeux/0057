using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("Lista de Itens")]
    public List<ItemPickUp> Items = new();

    [Header("Referencias na HUD")]
    public Transform ItemContent;
    public GameObject InventoryItem;
    public Toggle EnableRemove;

    public InventoryItemController[] InventoryItems;

    private void Awake()
    {
        Instance = this;
    }

    public void Add(ItemPickUp itemPickUp)
    {
        Items.Add(itemPickUp);
    }

    public void Remove(ItemPickUp itemPickUp)
    {
        Items.Remove(itemPickUp);
    }

    public void ListItems()
    {
        // Limpar itens do inventario
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        // Adicionar lista de itens ao inventario
        //Items = new List<ItemPickUp>();
        foreach (var o in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();

            itemName.text = ItemSO.GetName(o.ItemType);
            itemIcon.sprite = ItemSO.GetSprite(o.ItemType);
        }

        SetInventoryItems();
    }

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

    public void SetInventoryItems()
    {
        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < Items.Count; i++)
        {
            InventoryItems[i].AddItem(Items[i]);
        }
    }
}
