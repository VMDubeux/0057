using UnityEngine;

public class PlayerShop : MonoBehaviour, IShopCostumer
{
    //public event EventHandler OnDripAmountChanged;
    //public event EventHandler OnHealthPotionAmountChanged;

    [SerializeField] int dripAmount = 50;
    public bool canBuy;

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
            //OnDripAmountChanged?.Invoke(this, EventArgs.Empty);
            canBuy = true;
            return true;
        }
        else
        {
            print("RECURSOS INSUFICIENTES!");
            canBuy = false;
            return false;
        }
    }
}
