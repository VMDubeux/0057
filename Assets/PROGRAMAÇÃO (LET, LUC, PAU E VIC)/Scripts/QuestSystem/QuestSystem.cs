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
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Notifica que uma quest foi concluída.
    /// </summary>
    public void NotifyQuestCompletion()
    {
        QuestCompleted?.Invoke();
    }
}
