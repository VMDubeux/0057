using Main_Folders.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

public class SkillUpgrade : MonoBehaviour
{
    // Variáveis de Instância
    public GameObject currentButton;
    public GameObject[] prevButton;
    public GameObject[] nextButton;
    private SkillPoints sP_script;

    private string skillIdentifier; // Para armazenar o identificador único da habilidade

    // Métodos de Inicialização
    private void Start()
    {
        currentButton = gameObject;
        sP_script = LevelsManager.Instance.GetComponent<SkillPoints>();

        if (sP_script == null)
        {
            Debug.LogError("SkillPoints script not found in the GameManager.");
        }

        // Cria ou obtém o identificador único da habilidade
        skillIdentifier = PersistentIdentifierManager.GetOrCreateIdentifier(gameObject);

        // Verifica se o jogador já comprou a habilidade
        CheckSkillPurchase();
    }

    // Métodos Públicos
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

    // Métodos Privados
    private bool CheckPrevButtons()
    {
        foreach (var button in prevButton)
        {
            var buttonComponent = button.GetComponent<Button>();
            if (buttonComponent != null && !buttonComponent.interactable)
            {
                return true; // Encontrou um botão não interativo
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
                return true; // Encontrou um botão interativo
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
            // Se não foi comprada, desativar a habilidade
            DisableSkill();
        }
    }

    private void EnableSkill()
    {
        // Ativa o botão atual e os próximos botões
        var buttonComponent = currentButton.GetComponent<Button>();
        if (buttonComponent != null)
        {
            buttonComponent.interactable = true;
        }
        CheckUpgrades();
    }

    private void DisableSkill()
    {
        // Desativa o botão atual e os próximos botões
        var buttonComponent = currentButton.GetComponent<Button>();
        if (buttonComponent != null)
        {
            buttonComponent.interactable = false;
        }
        SetNextButtonsInteractable(false);
    }

    // Método para ser chamado quando o jogador compra a habilidade
    public void OnSkillPurchased()
    {
        PlayerPrefs.SetInt(skillIdentifier, 1);
        PlayerPrefs.Save(); // Salva as preferências imediatamente
        EnableSkill();
    }
}
