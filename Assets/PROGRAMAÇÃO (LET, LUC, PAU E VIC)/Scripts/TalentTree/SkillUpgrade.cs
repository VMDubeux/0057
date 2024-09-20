using Main_Folders.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

public class SkillUpgrade : MonoBehaviour
{
    // Vari�veis de Inst�ncia
    public GameObject currentButton;
    public GameObject[] prevButton;
    public GameObject[] nextButton;
    private SkillPoints sP_script;

    private string skillIdentifier; // Para armazenar o identificador �nico da habilidade

    // M�todos de Inicializa��o
    private void Start()
    {
        currentButton = gameObject;
        sP_script = LevelsManager.Instance.GetComponent<SkillPoints>();

        if (sP_script == null)
        {
            Debug.LogError("SkillPoints script not found in the GameManager.");
        }

        // Cria ou obt�m o identificador �nico da habilidade
        skillIdentifier = PersistentIdentifierManager.GetOrCreateIdentifier(gameObject);

        // Verifica se o jogador j� comprou a habilidade
        CheckSkillPurchase();
    }

    // M�todos P�blicos
    public void CheckUpgrades()
    {
        bool anyPrevButtonInteractable = CheckPrevButtons();
        if (anyPrevButtonInteractable || gameObject.name == "First")
        {
            bool isAnyNextButtonInteractable = CheckNextButtons();
            SetNextButtonsInteractable(isAnyNextButtonInteractable);
        }
        else
        {
            SetNextButtonsInteractable(false);
        }
    }

    // M�todos Privados
    private bool CheckPrevButtons()
    {
        foreach (var button in prevButton)
        {
            var buttonComponent = button.GetComponent<Button>();
            if (buttonComponent != null && !buttonComponent.interactable)
            {
                return true; // Encontrou um bot�o n�o interativo
            }
        }
        return false;
    }

    private bool CheckNextButtons()
    {
        foreach (var button in nextButton)
        {
            var buttonComponent = button.GetComponent<Button>();
            if (buttonComponent != null && buttonComponent.interactable)
            {
                return true; // Encontrou um bot�o interativo
            }
        }
        return false;
    }

    private void SetNextButtonsInteractable(bool isInteractable)
    {
        foreach (var button in nextButton)
        {
            var buttonComponent = button.GetComponent<Button>();
            if (buttonComponent != null)
            {
                buttonComponent.interactable = isInteractable;
            }
        }
    }

    private void CheckSkillPurchase()
    {
        // Verifica se a habilidade foi comprada
        if (PlayerPrefs.GetInt(skillIdentifier, 0) == 1)
        {
            // Se a habilidade foi comprada, ativar a habilidade
            EnableSkill();
        }
        else
        {
            // Se n�o foi comprada, desativar a habilidade
            DisableSkill();
        }
    }

    private void EnableSkill()
    {
        // Ativa o bot�o atual e os pr�ximos bot�es
        var buttonComponent = currentButton.GetComponent<Button>();
        if (buttonComponent != null)
        {
            buttonComponent.interactable = true;
        }
        CheckUpgrades();
    }

    private void DisableSkill()
    {
        // Desativa o bot�o atual e os pr�ximos bot�es
        var buttonComponent = currentButton.GetComponent<Button>();
        if (buttonComponent != null)
        {
            buttonComponent.interactable = false;
        }
        SetNextButtonsInteractable(false);
    }

    // M�todo para ser chamado quando o jogador compra a habilidade
    public void OnSkillPurchased()
    {
        PlayerPrefs.SetInt(skillIdentifier, 1);
        PlayerPrefs.Save(); // Salva as prefer�ncias imediatamente
        EnableSkill();
    }
}
