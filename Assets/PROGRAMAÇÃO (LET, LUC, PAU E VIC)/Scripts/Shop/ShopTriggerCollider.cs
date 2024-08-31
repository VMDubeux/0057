using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Main_Folders.Scripts.Shop
{
    public class ShopTriggerCollider : MonoBehaviour
    {
        public delegate void PlayerEntered();
        public static event PlayerEntered OnPlayerEntered;

        [SerializeField] private ShopUI shopUI;
        private IShopCostumer shopCostumer;
        private GameObject colliderObject;

        private void OnTriggerEnter(Collider collider)
        {
            shopCostumer = collider.GetComponent<IShopCostumer>();
            colliderObject = collider.gameObject;
            GameJuices.OnPressedButton += OpenStore;
        }

        private void OnTriggerExit(Collider collider)
        {
            shopCostumer = collider.GetComponent<IShopCostumer>();

            if (shopCostumer != null)
            {
                shopUI.Hide();

                ShopItemController[] items = FindObjectsByType<ShopItemController>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);
                foreach (ShopItemController item in items)
                {
                    item.transform.localScale = new Vector3(1, 1, 1);
                }
            }
            GetComponent<BoxCollider>().enabled = true;
            OnPlayerEntered = null;
        }

        private void OpenStore()
        {
            if (shopCostumer != null && OnPlayerEntered != null)
            {
                shopUI.Show(shopCostumer);
                colliderObject.GetComponent<PlayerMovement>().enabled = false;
            }
        }

        private void ExitStore()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                shopUI.Hide();

                ShopItemController[] items = FindObjectsByType<ShopItemController>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);
                foreach (ShopItemController item in items)
                {
                    item.transform.localScale = new Vector3(1, 1, 1);
                }

                OnPlayerEntered = null;
                GetComponent<BoxCollider>().enabled = false;
                colliderObject.GetComponent<PlayerMovement>().enabled = true;
            }
        }

        private void Update()
        {
            ExitStore();
        }
    }
}
