using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlimeKingTrigger : MonoBehaviour
{
    DialogueManager dialogueManager;
    public GameObject canvas;

    private void Start()
    {
        dialogueManager = GetComponent<DialogueManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        canvas.SetActive(true);
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            canvas.SetActive(false);
    }
    public void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(DialogueManager.isChatting == false)
            {
                dialogueManager.StartDialogue();
            }
            else 
                dialogueManager.DisplayNextSentence();
        }
    }
}
