using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleVisualsStats : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI levelText;

    private int currHealth;
    private int maxHealth;
    private int level;

    private Animator anim;

    private const string LEVEL_ABB = "Lvl: ";

    public void SetStartingValues(int currHealth, int maxHealth, int level)
    {
        this.currHealth = currHealth;
        this.maxHealth = maxHealth;
        this.level = level;
        levelText.text = LEVEL_ABB + this.level.ToString();
        UpdateHealthBar();
    }

    public void ChangeHealth(int currHealth)
    {
        this.currHealth = currHealth;
        if (currHealth <= 0)
        {
            Destroy(gameObject, 1);
        }
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = currHealth;
    }
}