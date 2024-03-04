using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header("Todos os itens do jogo")]
    public GameObject[] gameItem;
    public Sprite[] itemImage;

    [Header("Canvas")]
    public GameObject[] inventorySlot;  
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("inventory");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    
    public void OnPickUp (int index)
    {
        // add sprite na UI do inventario
        for (int i = 0; i < inventorySlot.Length; i++)
        {
            if (inventorySlot[i].GetComponent<Image>().sprite == null)
            {
                inventorySlot[i].GetComponent<Image>().sprite = itemImage[index];
            }
        }
        // add contagem de determinado item
        // 
    }

}
