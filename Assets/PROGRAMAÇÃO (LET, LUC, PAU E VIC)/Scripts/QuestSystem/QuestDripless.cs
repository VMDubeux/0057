using Main_Folders.Scripts.Minimapa;
using Main_Folders.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDripless : QuestObjects
{
    [Header("Dialogue Settings")]
    [SerializeField] private DialogManager dialogTriggerPrefab;
    private DialogManager dialogTrigger;

    [SerializeField] private DialogStep[] driplessDialogueSteps = new DialogStep[3];

    internal bool isDialogueStarted = false;
    internal bool isDialogueFinished = false;

    /// <summary>
    /// Retorna o gameObject para inserir o ícone no minimapa.
    /// </summary>
    protected override void AddMinimapIconPosition()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Retorna o objeto do diálogo ativo, caso exista.
    /// </summary>
    public GameObject GetActiveDialog()
    {
        return dialogTrigger != null ? dialogTrigger.gameObject : null;
    }

    /// <summary>
    /// Inicia o diálogo associado ao mentor.
    /// </summary>
    public void StartDialogue()
    {
        if (isDialogueStarted || isDialogueFinished)
            return;

        if (dialogTriggerPrefab == null || driplessDialogueSteps.Length == 0)
        {
            Debug.LogError("DialogTriggerPrefab ou LacaioDialogueSteps não configurados no QuestLacaio.");
            return;
        }

        var selectedDialogueStep = driplessDialogueSteps[Random.Range(0, driplessDialogueSteps.Length)];
        if (selectedDialogueStep == null)
        {
            Debug.LogError("O diálogo selecionado é nulo. Verifique os elementos no array LacaioDialogueSteps.");
            return;
        }

        dialogTrigger = Instantiate(dialogTriggerPrefab);
        dialogTrigger.step = selectedDialogueStep;
        dialogTrigger.dialogueDelegate += OnDialogueEnded;

        dialogTrigger.gameObject.SetActive(true);

        LevelsManager.Instance.isTalking = true;
        isDialogueStarted = true;
        PlayerMovement.isMovementBlocked = true;

        MarkQuestAsAvailable();
    }

    /// <summary>
    /// Executado ao término do diálogo.
    /// </summary>
    private void OnDialogueEnded()
    {
        isDialogueFinished = true;

        CompleteQuest();
        if (dialogTrigger != null)
        {
            Destroy(dialogTrigger.gameObject);
        }

        PlayerMovement.isMovementBlocked = false;
    }

    /// <summary>
    /// Lógica executada ao completar a quest.
    /// </summary>
    protected override void ProcessQuestCompletion()
    {
        base.ProcessQuestCompletion();

        // Quando a quest for completada, o marcador pode ser removido
    }

    protected override void GameJuiceCall()
    {
        Debug.LogWarning("GameJuiceLacaio não encontrado no objeto. Certifique-se de que ele está anexado.");
    }
}
