using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform shopItemTemplate;
    private IShopCostumer shopCostumer;

    private void Awake()
    {
        container = transform.Find("Container");
        shopItemTemplate = container.Find("ShopItemTemplate");
        shopItemTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        CreateItemButton(ItemType.PerfumePeq, ItemSO.GetSprite(ItemType.PerfumePeq), "Perfume Pequeno", ItemSO.GetCost(ItemType.PerfumePeq), 0);
        CreateItemButton(ItemType.PerfumeMed, ItemSO.GetSprite(ItemType.PerfumeMed), "Perfume Médio", ItemSO.GetCost(ItemType.PerfumeMed), 6);
        CreateItemButton(ItemType.PerfumeGrd, ItemSO.GetSprite(ItemType.PerfumeGrd), "Perfume Grande", ItemSO.GetCost(ItemType.PerfumeGrd), 12);
        CreateItemButton(ItemType.CartaComum, ItemSO.GetSprite(ItemType.CartaComum), "Carta", ItemSO.GetCost(ItemType.CartaComum), 18);

        Hide();
    }

    private void CreateItemButton(ItemType itemType, Sprite itemSprite, string itemName, int itemCost, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        shopItemTransform.gameObject.SetActive(true);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();
        float shopItemHeight = 30f;
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);
        shopItemTransform.Find("NameText").GetComponent<TextMeshProUGUI>().SetText(itemName.ToString());
        shopItemTransform.Find("PriceText").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());
        shopItemTransform.Find("Icon").GetComponent<Image>().sprite = itemSprite;

        shopItemTransform.GetComponent<Button>().onClick.AddListener(delegate { TryBuyItem(itemType); });
    }

    private void TryBuyItem(ItemType itemType)
    {
        if (shopCostumer.TrySpendDripAmount(ItemSO.GetCost(itemType)))
            shopCostumer.BoughtItem(itemType);
    }

    public void Show(IShopCostumer shopCostumer)
    {
        this.shopCostumer = shopCostumer;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
