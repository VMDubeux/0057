using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public ChatScriptableObject chatScript;
    public GameObject chatPanel;
    public TextMeshProUGUI textUI;
    public TextMeshProUGUI nameUI;
    public Image speakerImageUI;
    public Queue<string> sentences;

    public static bool isChatting = false;

    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue()
    {
        isChatting = true;
        chatPanel.SetActive(true);
        sentences.Clear();
        foreach(string s in chatScript.dialogueSentences) 
        {
            sentences.Enqueue(s);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        textUI.text = sentence;
        nameUI.text = chatScript.speakerName[0];
        speakerImageUI.sprite = chatScript.speakerImage[0];
    }
    void EndDialogue()
    {
        chatPanel.SetActive(false);
        isChatting = false;
    }
}
