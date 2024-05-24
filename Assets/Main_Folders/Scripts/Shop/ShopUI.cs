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
    int increase;

    private void Awake()
    {
        container = transform.Find("Container");
        shopItemTemplate = container.Find("ShopItemTemplate");
        shopItemTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        ItemSO item = new();

        for (int i = 0; i < 4; i++)
        {
            int random = Random.Range(0, 4);
            item.itemType = ItemSO.GetItemType(random);
            CreateItemButton(item.itemType, ItemSO.GetSprite(item.itemType), ItemSO.GetName(item.itemType), ItemSO.GetCost(item.itemType), increase);

            increase += 6;
        }

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
