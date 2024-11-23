using Main_Folders.Scripts.Minimapa;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestObjects : MonoBehaviour
{
    [Header("Dependencies")]
    [Tooltip("Insira as quests prévias obrigatórias.")]
    public QuestObjects[] requiredQuests;

    [Header("Quest Outputs")]
    [Tooltip("Objetos manipulados ao concluir a quest.")]
    public List<GameObject> questOutputs = new();

    [Header("Quest Status")]
    [Tooltip("Indica se a quest está disponível.")]
    public bool isAvailable;
    [Tooltip("Indica se a quest foi concluída.")]
    public bool isCompleted;

    private void Start()
    {
        AddMinimapIconPosition();
        CheckDependencies();
    }

    /// <summary>
    /// Adiciona o ícone no minimapa, mas adaptável, pois pode ser um inimigo ou objetivo.
    /// </summary>
    protected abstract void AddMinimapIconPosition();

    /// <summary>
    /// Verifica se todas as quests dependentes foram concluídas.
    /// </summary>
    public void CheckDependencies()
    {
        if (requiredQuests == null || requiredQuests.Length == 0)
        {
            MarkQuestAsAvailable();
            return;
        }

        foreach (var quest in requiredQuests)
        {
            if (!quest.isCompleted)
            {
                isAvailable = false;
                return;
            }
        }

        MarkQuestAsAvailable();
    }

    /// <summary>
    /// Marca a quest como disponível.
    /// </summary>
    public void MarkQuestAsAvailable()
    {
        isAvailable = true;
    }

    /// <summary>
    /// Finaliza a quest.
    /// </summary>
    public void CompleteQuest()
    {
        if (!isAvailable || isCompleted)
        {
            Debug.LogWarning("Quest já concluída ou indisponível.");
            return;
        }

        isCompleted = true;
        Debug.Log("Chegou aqui 1");
        ProcessQuestCompletion();
        Debug.Log("Chegou aqui 4");
    }

    /// <summary>
    /// Lógica personalizada de conclusão da quest, implementada nas subclasses.
    /// </summary>
    protected virtual void ProcessQuestCompletion()
    {
        if (questOutputs != null)
        {
            // Lógica padrão: Ativar ou manipular objetos de saída
            foreach (var output in questOutputs)
            {
                if (output != null)
                {
                    output.SetActive(false);
                }
            }

            questOutputs.Clear();
        }

        // Verifica e chama o método de GameJuiceMentor
        GameJuiceCall();

        Debug.Log("Quest concluída com sucesso.");
    }

    protected abstract void GameJuiceCall();

    private void Update()
    {
        CheckDependencies();
    }
}
