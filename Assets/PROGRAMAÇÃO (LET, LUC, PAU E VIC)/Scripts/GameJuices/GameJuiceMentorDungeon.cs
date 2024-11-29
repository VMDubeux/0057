using Main_Folders.Scripts.Managers;
using Main_Folders.Scripts.Minimapa;
using Main_Folders.Scripts.UI;
using System.Collections;
using UnityEngine;

public class GameJuiceMentorDungeon : GameJuices
{
    [SerializeField] private QuestMentorDungeon mentorDungeonQuest; // Substitui MentorFirstDialogue

    protected override void Start()
    {
        // Valida que a QuestMentor foi atribuída
        if (mentorDungeonQuest == null)
        {
            Debug.LogError("A QuestMentor não foi atribuída ao GameJuiceMentor. Verifique no editor.");
        }

        StartCoroutine(InitializeAfterDelay());
    }

    private IEnumerator InitializeAfterDelay()
    {
        yield return new WaitForEndOfFrame(); // Garante que tudo seja inicializado antes

        if (PlayerPrefs.GetInt(_assetKey, 0) == 1)
        {
            HandleQuestAlreadyCompleted();
        }

        CanvasGameJuices = FindFirstObjectByType<CanvasGameJuice>(FindObjectsInactive.Include).
            transform.GetChild(0).gameObject;
    }

    private void HandleQuestAlreadyCompleted()
    {
        wasOpen = true;

        mentorDungeonQuest.isAvailable = true;
        mentorDungeonQuest.isCompleted = true;

        var holder = Resources.FindObjectsOfTypeAll<MarkerHolder>();
        if (holder == null) return;

        foreach (var h in holder)
        {
            h.RemoveObjectiveMarker(this.gameObject);
        }

        if (mentorDungeonQuest.questOutputs != null)
        {
            // Lógica padrão: Ativar ou manipular objetos de saída
            foreach (var output in mentorDungeonQuest.questOutputs)
            {
                if (output != null)
                {
                    output.SetActive(false);
                }
            }

            mentorDungeonQuest.questOutputs.Clear();
        }
    }

    protected override void HandleTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !wasOpen)
        {
            if (mentorDungeonQuest.isDialogueStarted || mentorDungeonQuest.isDialogueFinished)
                return;

            CanvasGameJuices.SetActive(true);
            isInside = true;
        }
    }

    protected override void HandleTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CanvasGameJuices.SetActive(false);
            isInside = false;
            wasOpen = false;

            // Finaliza interações
            LevelsManager.Instance.isTalking = false;

            // Garante que o diálogo ativo seja destruído
            if (mentorDungeonQuest != null)
            {
                GameObject activeDialog = mentorDungeonQuest.GetActiveDialog();
                if (activeDialog != null)
                {
                    Destroy(activeDialog);
                }
            }
        }
    }

    protected override IEnumerator IsInside()
    {
        if (isInside && Input.GetKeyDown(KeyCode.E))
        {
            HandleButtonPress();
            yield return new WaitForSeconds(2.5f);
            PerformDelegate();
        }
    }

    protected override void HandleButtonPress()
    {
        wasOpen = true;
        CanvasGameJuices.SetActive(false);

        // Inicia o diálogo associado à QuestMentor
        if (mentorDungeonQuest != null)
        {
            mentorDungeonQuest.StartDialogue();
        }
        else
        {
            Debug.LogWarning("A QuestMentor não está configurada. Certifique-se de que ela foi atribuída.");
        }
    }

    protected override void SetupReturnToOrigin()
    {
        Debug.LogWarning("SetupReturnToOrigin não foi implementado.");
    }

    internal override void AddRandomItemToInventory() // Aqui os itens não serão aleatórios
    {
        Debug.LogWarning("SetupReturnToOrigin não foi implementado.");
    }
    private void Verification()
    {
        Debug.Log("Verificando!");
    }
}
