using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    public static QuestSystem Instance = new();

    public event Action QuestCompleted;

    public void InvokeEvent()
    {
        QuestCompleted?.Invoke();
    }
}
