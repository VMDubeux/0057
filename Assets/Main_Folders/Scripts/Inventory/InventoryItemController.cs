using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    [SerializeField] private InventoryItem ItemInventory;
    [SerializeField] private ItemPickUp ItemPickUp;

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(delegate { UseItem(ItemPickUp); });
    }

    public void RemoveItem()
    {
        Button RemoveButton = transform.GetComponentInChildren<Button>();
        InventoryManager.Instance.Remove(ItemPickUp);
        //Item.Destroy();
        //Destroy(gameObject);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void AddItem(InventoryItem newItem, ItemPickUp Item)
    {
        ItemInventory = newItem;
        ItemPickUp = Item;
    }


    public void UseItem(ItemPickUp item)
    {
        InventoryManager.Instance.Remove(item);
        //GameObject partyManager = GameObject.Find("PartyManager"); // Centralizar os atributos do Player no PartyManager, pois ele n�o � destru�do.

        switch (item.ItemType)
        {
            default:
            case ItemType.PerfumePeq:
                Debug.Log("Aumentar vida");
                break;
            case ItemType.PerfumeMed:
                Debug.Log("Aumentar número de cartas na m�o");
                break;
            case ItemType.PerfumeGrd:
                Debug.Log("Aumentar quantidade de mana por turno");
                break;
            case ItemType.CartaComum:
                Debug.Log("Ganhar carta comum");
                break;
        }
    }
}
