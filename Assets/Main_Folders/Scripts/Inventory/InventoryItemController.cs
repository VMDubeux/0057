using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    ItemPickUp Item;
    public Button RemoveButton;

    public void RemoveItem()
    {
        RemoveButton = transform.GetComponentInChildren<Button>();
        InventoryManager.Instance.Remove(Item);
        Item.Destroy();
        Destroy(gameObject);
    }
    
    public void AddItem(ItemPickUp newItem)
    {
        Item = newItem;
    }

    public void UseItem()
    {
        // Metodo para usar o item específico. 
    }
}
