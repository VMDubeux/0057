using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUpgrade : MonoBehaviour
{
    public GameObject prevButton, currentButton;
    public GameObject[] nextButton;
    SkillPoints sP_script;

    private void Start()
    {
        sP_script = FindFirstObjectByType<SkillPoints>();
        Debug.Log(sP_script.gameObject.name);
    }
    public void CheckUpgrades()
    {
        if(nextButton.Length > 0)
        {
            if(sP_script.skillPoints >= 0 && prevButton.GetComponent<Button>().interactable == false)
            {
                foreach (GameObject i in nextButton)
                {
                    if(i.GetComponent<Button>().interactable == true)
                    {
                        i.SetActive(true);
                    }
                }
            }
            else
            {
                foreach (GameObject i in nextButton)
                {
                    i.SetActive(false);
                }
            }
        }

    }
}
