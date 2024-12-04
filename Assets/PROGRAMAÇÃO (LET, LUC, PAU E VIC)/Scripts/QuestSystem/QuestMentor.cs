using UnityEngine;
using Main_Folders.Scripts.Minimapa;
using Main_Folders.Scripts.Managers;
using Main_Folders.Scripts.UI;

public class QuestMentor : QuestObjects
{
    [Header("Dialogue Settings")]
    [SerializeField] private DialogManager dialogTriggerPrefab;
    private DialogManager dialogTrigger;

    [SerializeField] private DialogStep mentorDialogueStep;

    internal bool isDialogueStarted = false;
    internal bool isDialogueFinished = false;

    /// <summary>
    /// Retorna o gameObject para inserir o �cone no minimapa.
    /// </summary>
    protected override void AddMinimapIconPosition()
    {
        FindFirstObjectByType<CanvasMinimapa>().transform.GetChild(0).GetComponent<MarkerHolder>()?.AddObjectiveMarker(this.gameObject);
        //FindFirstObjectByType<MarkerHolder>()?.AddObjectiveMarker(this.gameObject);
    }

    /// <summary>
    /// Retorna o objeto do di�logo ativo, caso exista.
    /// </summary>
    public GameObject GetActiveDialog()
    {
        return dialogTrigger != null ? dialogTrigger.gameObject : null;
    }

    /// <summary>
    /// Inicia o di�logo associado ao mentor.
    /// </summary>
    public void StartDialogue()
    {
        if (dialogTriggerPrefab == null || mentorDialogueStep == null)
        {
            Debug.LogError("DialogTriggerPrefab ou MentorDialogueStep n�o configurados no QuestMentor.");
            return;
        }

        dialogTrigger = Instantiate(dialogTriggerPrefab);
        dialogTrigger.step = mentorDialogueStep;
        dialogTrigger.dialogueDelegate += OnDialogueEnded;
        dialogTrigger.gameObject.SetActive(true);

        LevelsManager.Instance.isTalking = true;
        isDialogueStarted = true;
        PlayerMovement.isMovementBlocked = true;

        GetComponent<GameJuiceMentor>().enabled = false;

        // Marca a quest como dispon�vel
        MarkQuestAsAvailable();
    }

    /// <summary>
    /// Executado ao t�rmino do di�logo.
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
    /// L�gica executada ao completar a quest.
    /// </summary>
    protected override void ProcessQuestCompletion()
    {
        base.ProcessQuestCompletion();

        // Quando a quest for completada, o marcador pode ser removido
        FindFirstObjectByType<CanvasMinimapa>().transform.GetChild(0).GetComponent<MarkerHolder>()?.RemoveObjectiveMarker(this.gameObject);
    }

    protected override void GameJuiceCall()
    {
        var gameJuiceMentor = gameObject.GetComponent<GameJuiceMentor>();
        gameJuiceMentor.enabled = true;
        if (gameJuiceMentor != null)
        {
            gameJuiceMentor.AddRandomItemToInventory();
        }
        else
        {
            Debug.LogWarning("GameJuiceMentor n�o encontrado no objeto. Certifique-se de que ele est� anexado.");
        }

        var player = FindFirstObjectByType<PlayerMovement>();
        if (player != null)
        {
            player.GiveDripToPlayer();
        }
        else
        {
            Debug.LogWarning("Player n�o encontrado.");
        }

        Destroy(gameJuiceMentor);
    }
}
