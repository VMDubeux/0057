using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    [Header("Lista de Itens")]
    public List<ItemSO> Items = new List<ItemSO>();

    [Header("Referencias na HUD")]
    public Transform ItemContent;
    public GameObject InventoryItem;
    public Toggle EnableRemove;

    public InventoryItemController[] InventoryItems;

    private void Awake()
    {
        Instance = this;
    }

    public void Add(ItemSO item)
    {
        Items.Add(item);
    }

    public void Remove(ItemSO item)
    {
        Items.Remove(item);
    }

    public void ListItems()
    {
        // Limpar itens do inventario
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
        // Adicionar lista de itens ao inventario
        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
        }

        SetInventoryItems();
    }

    public void EnableItemRemove()
    {
        if(EnableRemove.isOn)
        {
            foreach(Transform item in ItemContent)
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

        for (int i = 0; i< Items.Count; i++)
        {
            InventoryItems[i].AddItem(Items[i]);
        }
    }
}
