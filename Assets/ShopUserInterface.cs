using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUserInterface : MonoBehaviour
{
    [SerializeField] private Transform canvasStore;
    private IShopCostumer shopCostumer;

    private void Awake()
    {
        canvasStore = transform.parent;
        canvasStore.gameObject.SetActive(false);
    }

    public void Show(IShopCostumer shopCostumer)
    {
        this.shopCostumer = shopCostumer;
        canvasStore.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        canvasStore.gameObject.SetActive(false);
        //gameObject.SetActive(false);
    }
}