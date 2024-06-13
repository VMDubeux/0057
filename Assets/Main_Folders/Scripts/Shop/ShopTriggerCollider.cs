using UnityEngine;

namespace Main_Folders.Scripts.Shop
{
    public class ShopTriggerCollider : MonoBehaviour
    {
        [SerializeField] private ShopUI shopUI;

        private void OnTriggerEnter(Collider collider)
        {
            IShopCostumer shopCostumer = collider.GetComponent<IShopCostumer>();
            if (shopCostumer != null)
            {
                shopUI.Show(shopCostumer);
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            IShopCostumer shopCostumer = collider.GetComponent<IShopCostumer>();
            if (shopCostumer != null)
            {
                shopUI.Hide();

                ShopItemController[] items = FindObjectsByType<ShopItemController>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);
                foreach (ShopItemController item in items)
                {
                    item.transform.localScale = new Vector3(1, 1, 1);
                }
            }
        }
    }
}
