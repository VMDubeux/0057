using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    [SerializeField] private InventoryItem ItemInventory;
    [SerializeField] private ItemPickUp ItemPickUp;

    public void RemoveItem()
    {
        Button RemoveButton = transform.GetComponentInChildren<Button>();
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
    }

    public void UseItem()
    {
        // Metodo para usar o item específico. 
    }
}
