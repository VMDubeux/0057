using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    ItemSO Item;
    public Button RemoveButton;
    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(Item);
        Destroy(gameObject);
    }
    
    public void AddItem(ItemSO newItem)
    {
        Item = newItem;
    }

    public void UseItem()
    {
        // Metodo para usar o item específico. 
    }
}
