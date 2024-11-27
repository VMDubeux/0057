using Main_Folders.Scripts.Managers;
using Main_Folders.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameJuiceDripless : GameJuices
{
    [SerializeField] private QuestDripless driplessQuests;

    protected override void Start()
    {
        driplessQuests = GetComponent<QuestDripless>();
    }

    internal override void AddRandomItemToInventory()
    {
        throw new System.NotImplementedException();
    }

    protected override void HandleTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInside = true;
        }
    }

    protected override void HandleTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInside = false;

            // Finaliza interações
            LevelsManager.Instance.isTalking = false;

            // Garante que o diálogo ativo seja destruído
            if (driplessQuests != null)
            {
                GameObject activeDialog = driplessQuests.GetActiveDialog();
                if (activeDialog != null)
                {
                    Destroy(activeDialog);
                }
            }
        }
    }

    protected override IEnumerator IsInside()
    {
        if (isInside == true)
        {
            HandleButtonPress();
            yield return new WaitForSeconds(2.5f);
            PerformDelegate();
        }
    }

    protected override void HandleButtonPress()
    {
        // Inicia o diálogo associado à QuestLacaio
        if (driplessQuests != null)
        {
            driplessQuests.StartDialogue();
        }
        else
        {
            Debug.LogWarning("A QuestLacaio não está configurada. Certifique-se de que ela foi atribuída.");
        }
    }

    protected override void SetupReturnToOrigin()
    {
        Debug.LogWarning("SetupReturnToOrigin não foi implementado.");
    }
}
