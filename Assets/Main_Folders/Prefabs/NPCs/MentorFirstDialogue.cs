using System.Collections;
using System.Collections.Generic;
using Main_Folders.Scripts.Player;
using Main_Folders.Scripts.UI;
using UnityEngine;

public class MentorFirstDialogue : MonoBehaviour
{
    [SerializeField] private DialogManager dialogTriggerPrefab;
    internal DialogManager dialogTrigger;
    [SerializeField] private DialogStep mentorDialogue;
    [SerializeField] private PlayerMovement playerMovement;

    private void OnTriggerEnter(Collider other)
    {
        dialogTrigger = Instantiate(dialogTriggerPrefab);
        dialogTrigger.step = mentorDialogue;
        dialogTrigger.dialogueDelegate += playerMovement.GiveDripToPlayer;
        dialogTriggerPrefab.gameObject.SetActive(true);
        LevelsManager.Instance.isTalking = true;
    }

    private void OnTriggerExit(Collider other)
    {
        Destroy(dialogTrigger.gameObject);
        LevelsManager.Instance.isTalking = false;
    }
}