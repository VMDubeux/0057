using System.Collections;
using UnityEngine;
using Main_Folders.Scripts.Minimapa;
using Main_Folders.Scripts.Managers;
using Main_Folders.Scripts.UI;

public class GameJuiceMentor : GameJuices
{
    [SerializeField] private QuestMentor mentorQuest; // Substitui MentorFirstDialogue
    [SerializeField] private CardToPickUp[] cardsToDrop = new CardToPickUp[2];

    protected override void Start()
    {
        // Valida que a QuestMentor foi atribuída
        if (mentorQuest == null)
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
    }

    private void HandleQuestAlreadyCompleted()
    {
        wasOpen = true;

        mentorQuest.isAvailable = true;
        mentorQuest.isCompleted = true;

        var holder = Resources.FindObjectsOfTypeAll<MarkerHolder>();
        if (holder == null) return;

        foreach (var h in holder)
        {
            h.RemoveObjectiveMarker(this.gameObject);
        }

        if (mentorQuest.questOutputs != null)
        {
            // Lógica padrão: Ativar ou manipular objetos de saída
            foreach (var output in mentorQuest.questOutputs)
            {
                if (output != null)
                {
                    output.SetActive(false);
                }
            }

            mentorQuest.questOutputs.Clear();
        }
    }

    protected override void HandleTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !wasOpen)
        {
            if (mentorQuest.isDialogueStarted || mentorQuest.isDialogueFinished)
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
            if (mentorQuest != null)
            {
                GameObject activeDialog = mentorQuest.GetActiveDialog();
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
        if (mentorQuest != null)
        {
            mentorQuest.StartDialogue();
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
        foreach (CardToPickUp card in cardsToDrop)
        {
            CardInventoryManager.Instance.CardPickedUp(card);
        }

        PlayerPrefs.SetInt(_assetKey, 1);

        wasOpen = true;

        StartCoroutine(CanvasCardDropped());
    }
    private void Verification()
    {
        Debug.Log("Verificando!");
    }
}
