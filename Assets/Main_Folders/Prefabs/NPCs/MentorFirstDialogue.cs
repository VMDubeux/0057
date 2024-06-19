using System.Collections;
using System.Collections.Generic;
using Main_Folders.Scripts.Player;
using Main_Folders.Scripts.UI;
using UnityEngine;

[RequireComponent(typeof(QuestManager))]
public class MentorFirstDialogue : MonoBehaviour
{
    [SerializeField] private DialogManager dialogTriggerPrefab;
    internal DialogManager dialogTrigger;
    [SerializeField] private DialogStep mentorDialogue;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameObject tronco;
    private QuestManager questManager;

    private void Start()
    {
        questManager = GetComponent<QuestManager>();
        questManager.completedQuest += EndedQuest;
    }

    private void OnTriggerEnter(Collider other)
    {
        dialogTrigger = Instantiate(dialogTriggerPrefab);
        dialogTrigger.step = mentorDialogue;
        dialogTrigger.dialogueDelegate += EndedDialog;
        dialogTriggerPrefab.gameObject.SetActive(true);
        LevelsManager.Instance.isTalking = true;
    }

    private void OnTriggerExit(Collider other)
    {
        Destroy(dialogTrigger.gameObject);
        LevelsManager.Instance.isTalking = false;
    }

    private void EndedDialog()
    {
        questManager.IntegralizarQuest(questManager.Quests[0]);
    }

    private void EndedQuest()
    {
        Destroy(tronco);
        playerMovement.GiveDripToPlayer();
    }
}