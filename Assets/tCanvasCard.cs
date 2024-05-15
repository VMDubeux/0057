using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class tCanvasCard : MonoBehaviour
{
    public tCardSO currentCard;
    int damage, shield;
    bool damageAll;
    string tName = "";
    void Start()
    {
        tName = currentCard.cardName;
        damage = currentCard.damage;
        shield = currentCard.shield;
        damageAll = currentCard.damageAll;
    }

    public void UseCard()
    {
        if(shield > 0)
        {
            GameObject.Find("Player").GetComponent<tPlayerStats>().baseShield += shield;
        }
        if (damage > 0)
        {
            if(damageAll)
            {
                GameObject.Find("Enemy").GetComponent<tEnemyStats>().enemyLife -= damage;
                Debug.Log("UsingCard");
            }
        }
    }
}
