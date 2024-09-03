using Main_Folders.Scripts.Shop;
using UnityEngine;

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

        // Assinar o evento
        GameJuices.OnPressedButton += OpenStore;
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (shopCostumer != null)
            {
                shopUI.Hide();
                // Reset items and collider
                ResetShopItems();
                GetComponent<BoxCollider>().enabled = true;
                colliderObject.GetComponent<PlayerMovement>().enabled = true;
            }
            // Desassinar o evento
            GameJuices.OnPressedButton -= OpenStore;
        }
    }

    private void OpenStore()
    {
        if (shopCostumer != null)
        {
            shopUI.Show(shopCostumer);
            colliderObject.GetComponent<PlayerMovement>().enabled = false;
        }
    }

    private void ResetShopItems()
    {
        ShopItemController[] items = FindObjectsByType<ShopItemController>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);
        foreach (ShopItemController item in items)
        {
            item.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ExitStore();
        }
    }

    private void ExitStore()
    {
        shopUI.Hide();
        ResetShopItems();
        GetComponent<BoxCollider>().enabled = false;
        colliderObject.GetComponent<PlayerMovement>().enabled = true;
    }
}
