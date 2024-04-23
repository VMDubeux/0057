using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public ItemSO Item;

    void PickUp()
    {
        InventoryManager.Instance.Add(Item);
        Destroy(gameObject);
    }

        private void OnTriggerEnter(Collider other)
    {
        PickUp();
        InventoryManager.Instance.ListItems();    
    }
}
