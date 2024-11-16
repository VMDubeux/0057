using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogManager : MonoBehaviour
{
    public DialogStep step; 
    [SerializeField] private TextMeshProUGUI dialogBox;
    [SerializeField] private DialogButtonMenu dialogButtonMenuPrefab;

    private DialogButtonMenu currentDialogButtonMenu;
    private int currentDialog = -1;
    public delegate void DialogueDelegate();
    public DialogueDelegate dialogueDelegate;

    void Start() {
        if (step == null || step.dialogues.Length == 0) return;
        currentDialogButtonMenu = Instantiate(dialogButtonMenuPrefab, transform);
        currentDialog = -1;
        NextStep();
    }

    public void NextStep() {
        currentDialog++;

        if (currentDialog == step.dialogues.Length) {

            if (step.options.Length == 0) {
                dialogueDelegate();
                Destroy(gameObject);
            } else {
                currentDialogButtonMenu.ConfigureWith(step.options, this);
            }
            
            return;
        }

        if (currentDialog == step.dialogues.Length + 1) {
            dialogueDelegate();
            Destroy(gameObject);
            return;
        }

        dialogBox.text = step.dialogues[currentDialog];
        currentDialogButtonMenu.ConfigureWith(null, this);
        
    }

    public void SelectOption(DialogStepOption option) {
        currentDialog = -1;
        step = option.nextSteps;
        NextStep();
    } 
}
