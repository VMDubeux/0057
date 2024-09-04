using System.Collections;
using System.Collections.Generic;
using Assets.PROGRAMAÇÃO__LET__LUC__PAU_E_VIC_.Scripts.Inventory;
using Unity.VisualScripting;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public int identity;
    public ItemType ItemType;
    [SerializeField] internal int Id;
    [SerializeField] internal string Name;
    [SerializeField] internal int Cost;
    [SerializeField] internal Sprite Sprite;
    [SerializeField] internal InventoryItem InventoryItem;

    private void Awake()
    {
        //DontDestroyOnLoad(this);
    }

    private void Start()
    {
        Id = ItemSO.GetId(ItemType);
        Name = ItemSO.GetName(ItemType);
        Cost = ItemSO.GetCost(ItemType);
        Sprite = ItemSO.GetSprite(ItemType);
        if (PlayerPrefs.HasKey(Id + Name + identity))
        {
            Destroy(this.gameObject);
        }
    }

    void PickUp()
    {
        InventoryManager.Instance.Add(this.gameObject.GetComponent<ItemPickUp>());
        DontDestroyOnLoad(gameObject);
        PlayerPrefs.SetString(Id + Name + identity, "Picked");
        gameObject.SetActive(false);
    }

    public void DestroyIt()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        PickUp();
    }
}