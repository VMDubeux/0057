using Main_Folders.Scripts.Managers;
using Main_Folders.Scripts.Minimapa;
using Main_Folders.Scripts.UI;
using UnityEngine;

public class QuestMentorDungeon : QuestObjects
{
    [Header("Dialogue Settings")]
    [SerializeField] private DialogManager dialogTriggerPrefab;
    private DialogManager dialogTrigger;

    [SerializeField] private DialogStep mentorDungeonDialogueStep;

    internal bool isDialogueStarted = false;
    internal bool isDialogueFinished = false;

    /// <summary>
    /// Retorna o gameObject para inserir o ícone no minimapa.
    /// </summary>
    protected override void AddMinimapIconPosition()
    {
        FindFirstObjectByType<CanvasMinimapa>().transform.GetChild(0).GetComponent<MarkerHolder>()?.AddObjectiveMarker(this.gameObject);
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
        if (dialogTriggerPrefab == null || mentorDungeonDialogueStep == null)
        {
            Debug.LogError("DialogTriggerPrefab ou MentorDialogueStep não configurados no QuestMentor.");
            return;
        }

        dialogTrigger = Instantiate(dialogTriggerPrefab);
        dialogTrigger.step = mentorDungeonDialogueStep;
        dialogTrigger.dialogueDelegate += OnDialogueEnded;
        dialogTrigger.gameObject.SetActive(true);

        LevelsManager.Instance.isTalking = true;
        isDialogueStarted = true;
        PlayerMovement.isMovementBlocked = true;

        // Marca a quest como disponível
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
        FindFirstObjectByType<CanvasMinimapa>().transform.GetChild(0).GetComponent<MarkerHolder>()?.RemoveObjectiveMarker(this.gameObject);
    }

    protected override void GameJuiceCall()
    {
        GameJuiceMentorDungeon mentorDungeon = GetComponent<GameJuiceMentorDungeon>();

        PlayerPrefs.SetInt(mentorDungeon._assetKey, 1);

        mentorDungeon.wasOpen = true;
    }
}
