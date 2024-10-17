using Main_Folders.Scripts.Shop;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCollider : MonoBehaviour
{
    public delegate void PlayerEntered();
    public static event PlayerEntered OnPlayerEntered;

    [SerializeField] private ShopUserInterface shopUI;
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
        GetComponent<BoxCollider>().enabled = false;
        colliderObject.GetComponent<PlayerMovement>().enabled = true;
    }
}
