using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestObjects : MonoBehaviour
{
    [Header("Inputs:")]
    [Tooltip("Insira as quests prévias obrigatórias")]
    public QuestObjects[] requiredQuests;

    [Header("Outputs:")]
    [Tooltip("Insira (se houver) os objetos que serão manipulados pelo método CompletedQuest")]
    public GameObject[] possibleOutputs;

    [Header("Availability:")]
    [Tooltip("Dirá se está disponível para ser realizada a quest")]
    public bool isAvailable;

    [Header("Status:")]
    [Tooltip("Dirá se a quest foi realizada")]
    public bool isFinished;

    public void CheckQuestCompletion()
    {
        if (requiredQuests != null)
        {
            foreach (var quest in requiredQuests)
            {
                if (!quest.isFinished)
                {
                    return; // Sai da função se encontrar uma quest não cumprida
                }
            }
        }

        isAvailable = true;
    }

    protected abstract void CompletedQuest(); // método de conclusão de quest personalizável

    protected void OnEnable()
    {
        QuestSystem.Instance.QuestCompleted += CheckQuestCompletion;
        QuestSystem.Instance.InvokeEvent();
    }

    protected void OnDisable()
    {
        QuestSystem.Instance.QuestCompleted -= CheckQuestCompletion;
    }
}
