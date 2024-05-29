using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MentorFirstDialogue : MonoBehaviour
{
    [SerializeField] private DialogManager dialogTriggerPrefab;
    [SerializeField] private DialogStep mentorDialogue;
    [SerializeField] private PlayerMovement playerMovement;

    private void OnTriggerEnter(Collider other)
    {
        DialogManager dialogTrigger = Instantiate(dialogTriggerPrefab);
        dialogTrigger.step = mentorDialogue;
        dialogTrigger.dialogueDelegate += playerMovement.GiveDripToPlayer;
    }
}