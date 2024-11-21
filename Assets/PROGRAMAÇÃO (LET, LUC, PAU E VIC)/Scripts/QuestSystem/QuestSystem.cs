using System;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    public static QuestSystem Instance;

    public event Action QuestCompleted;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Define a instância se ainda não foi atribuída
        }
        else
        {
            Destroy(gameObject); // Garante que apenas uma instância exista
            return;
        }

        DontDestroyOnLoad(gameObject); // Opcional: Garante que o QuestSystem persista entre cenas
    }

    /// <summary>
    /// Método para notificar que uma quest foi concluída.
    /// </summary>
    public void NotifyQuestCompletion()
    {
        QuestCompleted?.Invoke();
    }
}
