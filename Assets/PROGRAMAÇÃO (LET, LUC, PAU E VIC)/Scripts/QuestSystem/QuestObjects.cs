using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestObjects : MonoBehaviour
{
    [Header("Inputs:")]
    [Tooltip("Insira as quests pr�vias obrigat�rias")]
    public QuestObjects[] requiredQuests;

    [Header("Outputs:")]
    [Tooltip("Insira (se houver) os objetos que ser�o manipulados pelo m�todo CompletedQuest")]
    public GameObject[] possibleOutputs;

    [Header("Availability:")]
    [Tooltip("Dir� se est� dispon�vel para ser realizada a quest")]
    public bool isAvailable;

    [Header("Status:")]
    [Tooltip("Dir� se a quest foi realizada")]
    public bool isFinished;

    public void CheckQuestCompletion()
    {
        if (requiredQuests != null)
        {
            foreach (var quest in requiredQuests)
            {
                if (!quest.isFinished)
                {
                    return; // Sai da fun��o se encontrar uma quest n�o cumprida
                }
            }
        }

        isAvailable = true;
    }

    protected abstract void CompletedQuest(); // m�todo de conclus�o de quest personaliz�vel

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
