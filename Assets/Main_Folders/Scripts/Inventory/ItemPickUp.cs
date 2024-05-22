using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public ItemType ItemType;
    [SerializeField] internal int Id;
    [SerializeField] internal string Name;
    [SerializeField] internal int Cost;
    [SerializeField] internal Sprite Sprite;

    private void Start()
    {
        Id = ItemSO.GetId(ItemType);
        Name = ItemSO.GetName(ItemType);
        Cost = ItemSO.GetCost(ItemType);
        Sprite = ItemSO.GetSprite(ItemType);
    }

    void PickUp()
    {
        InventoryManager.Instance.Add(this.gameObject.GetComponent<ItemPickUp>());
        gameObject.SetActive(false);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        PickUp();
        InventoryManager.Instance.ListItems();    
    }
}
