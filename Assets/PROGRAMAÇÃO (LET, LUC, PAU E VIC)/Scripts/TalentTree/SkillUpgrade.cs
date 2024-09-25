using Main_Folders.Scripts.UI;
using Main_Folders.Scripts.Units;
using UnityEngine;
using UnityEngine.UI;

public class SkillUpgrade : MonoBehaviour
{
    // Vari�veis de Inst�ncia

    public GameObject prevButton;
    public GameObject nextButton;
    private SkillPoints sP_script;
    [SerializeField] private GameObject playerBattleVisual;
    public int value = 1;
    public static int stInflictBonus = 0, stLight = 0, resAmount = 0;

    public void CheckUpgrades()
    {
        //verifica se existe upgrade inativo e o botão atual já foi usado
        if (GetComponent<Button>() != null)
        {
            Button buttonComponent = GetComponent<Button>();
            SkillUpgrade currentSU = GetComponent<SkillUpgrade>();
            if (buttonComponent != null)
            {
                if(prevButton == null && buttonComponent.interactable == true)
                {
                    buttonComponent.gameObject.SetActive(true);
                }
                else if (prevButton != null && prevButton.GetComponent<Button>().interactable == false && buttonComponent.interactable == true)
                {
                    buttonComponent.gameObject.SetActive(true);
                }
            }
        }
    }

    public void UpgradeAttack1 ()
    {
        playerBattleVisual.GetComponent<Unit>()._stats[3].Value += value;
    }         
    public void UpgradeAttack2 ()
    {
        stInflictBonus = value;
    }
    public void UpgradeAttack3 ()
    {
        stLight = value;
    }

    public void UpgradeDef1 ()
    {
        playerBattleVisual.GetComponent<Unit>()._stats[2].Value += value;
    }
    public void UpgradeDef2 ()
    {
        // Inicia o jogo com VALUE carga extra da poção 
    }

    public void UpgradeDef3 ()
    {
        resAmount = value;
        playerBattleVisual.GetComponent<Unit>()._stats[4].Value = resAmount;
    }

    public void UpgradeHP1 ()
    {
        playerBattleVisual.GetComponent<Unit>()._stats[1].Value += value;
    }
    public void UpgradeHP2 ()
    {
        // Recebe VALUE pots de vida
    }
    public void UpgradeHP3 ()
    {
        playerBattleVisual.GetComponent<PlayerUnit>().MaxCards += value;
    }
}

