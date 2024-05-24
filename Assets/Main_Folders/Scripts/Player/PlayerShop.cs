using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        ItemPickUp itemPickUp = new()
        {
            ItemType = itemType,
            Id = ItemSO.GetId(itemType),
            Name = ItemSO.GetName(itemType),
            Cost = ItemSO.GetCost(itemType),
            Sprite = ItemSO.GetSprite(itemType),
        };

        Debug.Log("Bought: " + itemPickUp.Name);
        InventoryManager.Instance.Add(itemPickUp);
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
