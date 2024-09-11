using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkillPoints : MonoBehaviour
{
    public int skillPoints;
    public TextMeshProUGUI sP_text;
    public GameObject[] upgradeButtons;

    private void Start()
    {
        // Atualiza a UI no início
        UpdateSkillPointsUI();
    }

    private void UpdateSkillPointsUI()
    {
        sP_text.text = "Skill Points: " + skillPoints;
    }

    public void UseSkillPoint()
    {
        if (skillPoints > 0)
        {
            skillPoints--;
            UpdateSkillPointsUI();

            if (skillPoints == 0)
            {
                DisableUpgradeButtons();
            }

            // Atualiza os botões de upgrade
            UpdateUpgradeButtons();
        }
    }

    public void LevelUp()
    {
        skillPoints++;
        UpdateSkillPointsUI();

        // Atualiza os botões de upgrade
        UpdateUpgradeButtons();
    }

    private void UpdateUpgradeButtons()
    {
        foreach (GameObject buttonObject in upgradeButtons)
        {
            SkillUpgrade skillUpgrade = buttonObject.GetComponent<SkillUpgrade>();
            if (skillUpgrade != null)
            {
                skillUpgrade.CheckUpgrades();
            }
        }
    }

    private void DisableUpgradeButtons()
    {
        foreach (GameObject buttonObject in upgradeButtons)
        {
            Button buttonComponent = buttonObject.GetComponent<Button>();
            if (buttonComponent != null)
            {
                buttonComponent.interactable = false;
            }
        }
    }
}