using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMentor : QuestObjects
{
    protected override void CompletedQuest()
    {
        // Exemplo de ação ao concluir a Quest
        if (possibleOutputs.Length > 0 && possibleOutputs[0] != null)
        {
            Destroy(possibleOutputs[0]); // Remove o objeto especificado
        }

        if (possibleOutputs.Length > 1 && possibleOutputs[1] != null)
        {
            QuestObjects nextQuest = possibleOutputs[1].GetComponent<QuestObjects>();
            if (nextQuest != null)
            {
                nextQuest.isAvailable = true; // Torna a próxima quest disponível
            }
        }
    }
}
