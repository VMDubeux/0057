using System.Collections;
using System.Collections.Generic;
using Main_Folders.Scripts;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public GameObject [] CardsToDrop;
    public void CardDrop()
    {
        foreach (GameObject card in CardsToDrop)
        {
            Instantiate(card, References.Instance.CurrentEnemyBattle.transform.position, Quaternion.identity);
        }
    }
}
