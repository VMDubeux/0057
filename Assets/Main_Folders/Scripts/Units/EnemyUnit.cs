using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUnit : Unit
{
    public int damage;

    public int block;
    private void Start() {
        InvokeRepeating("UpdateHealthBar", 2, 2);
    }
    public override IEnumerator EnemyDamage()
    {
        yield return null;
        Unit stats = GameObject.FindAnyObjectByType<PlayerUnit>().GetComponent<PlayerUnit>();
        
        Debug.Log("Vida atual: "+ stats.stats[1].Value);
        int difDamage = Mathf.Abs(stats.stats[2].Value - damage);
        if(stats.stats[2].Value <= 0)
        // se nao tem escudo, da o dano
            stats.stats[1].Value -= damage;
        else // se tiver escudo
        {
            if(damage > stats.stats[2].Value) // e o dano for maior que escudo
            {
                stats.stats[2].Value = 0;
                stats.stats[1].Value -= difDamage;
            }
            else // se o dano for menor ou igual
            {
                stats.stats[2].Value -= damage; // dano apenas no escudo
            }
        }
        Debug.Log("Vida atual: "+ stats.stats[1].Value);
    }
        void UpdateHealthBar()
    {
        gameObject.GetComponentInChildren<Slider>().maxValue = stats[0].Value;
        gameObject.GetComponentInChildren<Slider>().value = stats[1].Value;
    }
}