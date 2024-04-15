using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public ItemSO Item;

    void PickUp()
    {
        InventoryManager.Instance.Add(Item);
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        PickUp();
        InventoryManager.Instance.ListItems();
    }
}
