using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShop : MonoBehaviour, IShopCostumer
{
    public event EventHandler OnDripAmountChanged;
    //public event EventHandler OnHealthPotionAmountChanged;

    [SerializeField] int dripAmount = 50;

    public int GetDripAmount()
    {
        return dripAmount;
    }

    public void BoughtItem(ItemType itemType)
    {
        GameObject obj = new();
        obj.AddComponent<ItemPickUp>();
        obj.GetComponent<ItemPickUp>().ItemType = itemType;
        obj.GetComponent<ItemPickUp>().Id = ItemSO.GetId(itemType);
        obj.GetComponent<ItemPickUp>().Name = ItemSO.GetName(itemType);
        obj.GetComponent<ItemPickUp>().Cost = ItemSO.GetCost(itemType);
        obj.GetComponent<ItemPickUp>().Sprite = ItemSO.GetSprite(itemType);
        
        Debug.Log("Bought: " + obj.GetComponent<ItemPickUp>().Name);
        InventoryManager.Instance.Add(obj.GetComponent<ItemPickUp>());
        obj.SetActive(false);
        //Destroy(obj);
        //InventoryManager.Instance.ListItems();
    }

    public bool TrySpendDripAmount(int spendDripAmount)
    {
        if (GetDripAmount() >= spendDripAmount)
        {
            dripAmount -= spendDripAmount;
            print(dripAmount);
            OnDripAmountChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }
        else
        {
            print("RECURSOS INSUFICIENTES!");
            return false;
        }
    }
}
