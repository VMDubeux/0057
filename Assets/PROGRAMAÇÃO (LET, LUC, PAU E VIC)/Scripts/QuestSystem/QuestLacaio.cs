using Main_Folders.Scripts.Minimapa;
using Main_Folders.Scripts.UI;
using UnityEngine;

public class QuestLacaio : QuestObjects
{
    [Header("Dialogue Settings")]
    [SerializeField] private DialogManager dialogTriggerPrefab;
    private DialogManager dialogTrigger;

    [SerializeField] private DialogStep[] lacaioDialogueSteps = new DialogStep[3];
    internal bool isDialogueStarted = false;
    internal bool isDialogueFinished = false;

    private Unit unitComponent;
    private EnemyMovementStates enemyMovement;

    private void Awake()
    {
        unitComponent = gameObject.GetComponent<Unit>();
        if (unitComponent == null)
            Debug.LogError("Unit component is missing on this object.");

        enemyMovement = gameObject.GetComponent<EnemyMovementStates>();
        if (enemyMovement == null)
            Debug.LogError("EnemyMovementStates component is missing on this object.");
    }

    protected override void AddMinimapIconPosition()
    {
        FindFirstObjectByType<MarkerHolder>().AddEnemyMarker(this.gameObject);
    }

    public GameObject GetActiveDialog()
    {
        return dialogTrigger != null ? dialogTrigger.gameObject : null;
    }

    public void StartDialogue()
    {
        if (isDialogueStarted || isDialogueFinished)
            return;

        if (dialogTriggerPrefab == null || lacaioDialogueSteps.Length == 0)
        {
            Debug.LogError("DialogTriggerPrefab ou LacaioDialogueSteps não configurados no QuestLacaio.");
            return;
        }

        var selectedDialogueStep = lacaioDialogueSteps[Random.Range(0, lacaioDialogueSteps.Length)];
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

        // Impede o movimento do inimigo enquanto o diálogo estiver em andamento
        if (enemyMovement != null)
        {
            enemyMovement.StopMovement();
        }

        MarkQuestAsAvailable();
    }

    private void OnDialogueEnded()
    {
        isDialogueFinished = true;

        if (dialogTrigger != null)
            Destroy(dialogTrigger.gameObject);

        StartCombat();
    }

    private void StartCombat()
    {
        PlayerMovement.isMovementBlocked = false;

        if (enemyMovement != null)
        {
            enemyMovement.StartCombatLogic();
        }
    }

    protected override void ProcessQuestCompletion()
    {
        if (unitComponent != null && unitComponent.hasFought == true)
        {
            base.ProcessQuestCompletion();

            var holder = Resources.FindObjectsOfTypeAll<MarkerHolder>();
            if (holder == null) return;

            foreach (var h in holder)
            {
                h.RemoveEnemyMarker(this.gameObject);
            }
            Debug.Log("REMOVEU O ICONE?");
        }
        else
        {
            Debug.LogWarning("A batalha ainda não foi concluída. Quest não pode ser marcada como completa.");
        }
    }

    protected override void GameJuiceCall()
    {
        var gameJuiceLacaio = gameObject.GetComponent<GameJuiceEnemies>();
        if (gameJuiceLacaio != null)
        {
            gameJuiceLacaio.AddRandomItemToInventory();
        }
        else
        {
            Debug.LogWarning("GameJuiceLacaio não encontrado no objeto. Certifique-se de que ele está anexado.");
        }
    }
}