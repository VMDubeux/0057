using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MentorFirstDialogue : MonoBehaviour
{
    [SerializeField] private DialogManager dialogTriggerPrefab;
    [SerializeField] private DialogStep mentorDialogue;
    private void OnTriggerEnter(Collider other)
    {
        DialogManager dialogTrigger = Instantiate(dialogTriggerPrefab);
        dialogTrigger.step  = mentorDialogue;
    }
}
