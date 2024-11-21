using System.Collections;
using System.Collections.Generic;
using Main_Folders.Scripts.Player;
using Main_Folders.Scripts.UI;
using UnityEngine;

[RequireComponent(typeof(QuestManager))]
public class MentorFirstDialogue : MonoBehaviour
{
    [SerializeField] private DialogManager dialogTriggerPrefab;
    public DialogManager dialogTrigger;
    [SerializeField] private DialogStep mentorDialogue;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameObject tronco;
    private QuestManager questManager;

    private void Start()
    {
        playerMovement = FindFirstObjectByType<PlayerMovement>();
        questManager = GetComponent<QuestManager>();
        questManager.completedQuest += EndedQuest;
    }

    public void StartDialogue()
    {
        dialogTrigger = Instantiate(dialogTriggerPrefab);
        dialogTrigger.step = mentorDialogue;
        dialogTrigger.dialogueDelegate += EndedDialog;
        dialogTriggerPrefab.gameObject.SetActive(true);
        LevelsManager.Instance.isTalking = true;

        // Notifica o QuestSystem sobre a interação
        if (QuestSystem.Instance != null)
        {
            QuestSystem.Instance.NotifyQuestCompletion();
        }
        else
        {
            Debug.LogWarning("QuestSystem.Instance não está disponível. Verifique se o QuestSystem está configurado corretamente na cena.");
        }
    }

    private void EndedDialog()
    {
        questManager.IntegralizarQuest(questManager.Quests[0]);
        Destroy(GetComponent<ParticleSystem>());
    }

    private void EndedQuest()
    {
        Destroy(tronco);
        playerMovement.GiveDripToPlayer();
    }
}
