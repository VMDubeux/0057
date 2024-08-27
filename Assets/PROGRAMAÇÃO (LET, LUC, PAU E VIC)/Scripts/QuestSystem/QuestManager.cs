using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public string[] Quests;
    public string[] DependenciaQuests;
    private bool[] IntegralizacaoQuests;

    public delegate void CompletedQuest();
    public CompletedQuest completedQuest;

    // Start is called before the first frame update
    void Start()
    {
        IntegralizacaoQuests = new bool[Quests.Length];

        for (int i = 0; i < IntegralizacaoQuests.Length; i++)
        {
            IntegralizacaoQuests[i] = false;
        }
    }

    public void IntegralizarQuest(string nome)
    {
        int i;
        bool found = false;

        //Encontre o indice da quest
        for (i = 0; i < Quests.Length; i++)
        {
            if (Quests[i] == nome) { 
                found = true; 
                break; 
            }
        }

        if (!found)
        {
            Debug.Log("Quest não encontrada");
            return;
        }

        //Hora de checar se tenho dependencias
        if (DependenciaQuests[i] == "" || DependenciaQuests[i] == null)
        {
            IntegralizacaoQuests[i] = true;
            Debug.Log("A quest " + nome + " foi finalizada.");

            if (LevelCompleto())
            {
                completedQuest();
            }

            return;
        }

        if (DependenciaQuests[i].Contains(","))
        {
            string[] dependencias = splitString(",", DependenciaQuests[i]);

            bool completo = true;
            for (int j = 0; j < dependencias.Length; j++)
            {

                if (IntegralizacaoQuests[int.Parse(dependencias[j])] == false)
                {
                    completo = false;
                    Debug.Log("A quest " + nome + " NÃO foi finalizada. Ela possui dependencias: " + Quests[int.Parse(dependencias[j])]);
                }

            }

            if (completo)
            {
                Debug.Log("A quest " + nome + " foi finalizada.");
                IntegralizacaoQuests[i] = true;
            }
            return;

        }

        if (IntegralizacaoQuests[int.Parse(DependenciaQuests[i])])
        {
            Debug.Log("A quest " + nome + " foi finalizada.");
            IntegralizacaoQuests[i] = true;
            return;
        }

        Debug.Log("A quest " + nome + " NÃO foi finalizada. Ela possui dependencias: " + Quests[int.Parse(DependenciaQuests[i])]);

    }

    public void VerIntegralizacaoQuest()
    {

        for (int i = 0; i < Quests.Length; i++)
        {
            Debug.Log("Nome da Quest: " + Quests[i] + " - Integralizada: " + IntegralizacaoQuests[i]);
        }

    }

    public bool LevelCompleto()
    {

        bool completo = true;

        for (int i = 0; i < IntegralizacaoQuests.Length; i++)
        {
            if (IntegralizacaoQuests[i] == false)
            {
                completo = false;
            }
        }

        return completo;

    }

    public string[] splitString(string needle, string haystack)
    {

        return haystack.Split(new string[] { needle }, System.StringSplitOptions.None);

    }
}
