using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class tPlayerStats : MonoBehaviour
{
    public int currentHealth = 100;
    public int maxHealth = 100;
    public int baseDamage = 10;
    public int baseShield = 0;
    public int maxEnergy = 3;
    public int currentEnergy = 3;
    public Slider lifebar;
    
    void Start()
    {
        lifebar.maxValue = maxHealth;
        lifebar.value = currentHealth;
    }

    public void UpdateLifeBar()
    {
        lifebar.value = currentHealth;
    }
}
