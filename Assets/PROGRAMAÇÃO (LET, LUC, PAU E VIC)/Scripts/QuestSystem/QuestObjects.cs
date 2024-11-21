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

    /// <summary>
    /// Verifica se a quest pode ser disponibilizada com base nas requiredQuests.
    /// </summary>
    public void CheckQuestAvailability()
    {
        if (requiredQuests != null)
        {
            foreach (var quest in requiredQuests)
            {
                if (!quest.isFinished) // Se qualquer requiredQuest não foi concluída
                {
                    isAvailable = false;
                    return;
                }
            }
        }

        isAvailable = true; // Disponibiliza a quest se todas as requiredQuests estiverem concluídas
    }

    /// <summary>
    /// Finaliza a quest atual.
    /// </summary>
    public void FinishQuest()
    {
        if (!isFinished && isAvailable)
        {
            isFinished = true;
            CompletedQuest();
            NotifyQuestCompletion();
        }
    }

    protected abstract void CompletedQuest(); // Método abstrato para personalizar a conclusão da quest

    /// <summary>
    /// Notifica o QuestSystem sobre a conclusão da quest.
    /// </summary>
    protected void NotifyQuestCompletion()
    {
        if (QuestSystem.Instance != null)
        {
            QuestSystem.Instance.NotifyQuestCompletion();
        }
        else
        {
            Debug.LogWarning("QuestSystem.Instance não está disponível. Verifique se o objeto QuestSystem está na cena.");
        }
    }

    protected void OnEnable()
    {
        QuestSystem.Instance.QuestCompleted += CheckQuestAvailability;
        QuestSystem.Instance.NotifyQuestCompletion();
    }

    protected void OnDisable()
    {
        QuestSystem.Instance.QuestCompleted -= CheckQuestAvailability;
    }
}
