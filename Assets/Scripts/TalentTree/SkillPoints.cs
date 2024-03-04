using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkillPoints : MonoBehaviour
{
    public int skillPoints;
    public TextMeshProUGUI sP_text;
    public GameObject[] upgradeButtons; 
    bool disableUpgradeButton = false;

    private void Start()
    {
        sP_text.text = "Skill Points: " + skillPoints;
    }
    private void Update()
    {
        if(disableUpgradeButton == true)
        {
            disableUpgradeButton = false;
            foreach(GameObject g in upgradeButtons)
            {
                if (g.GetComponent<Button>().interactable == true)
                {
                    g.SetActive(false);
                }
            }
        }
    }
    public void UseSkillPoint()
    {
        skillPoints--;
        sP_text.text = "Skill Points: " + skillPoints;
        if(skillPoints == 0)
        {
            disableUpgradeButton = true;
        }
    }
    public void LevelUp()
    {
        skillPoints++;
        sP_text.text = "Skill Points: " + skillPoints;
        foreach(GameObject g in upgradeButtons)
        {
            g.GetComponent<SkillUpgrade>().CheckUpgrades();
        }
    }

}
