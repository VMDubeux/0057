using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopCostumer
{
    void BoughtItem(ItemType itemType);
    bool TrySpendDripAmount(int dripAmount);
}
