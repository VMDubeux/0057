using Main_Folders.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PortalDungeon : MonoBehaviour
{
    [SerializeField] private EncounterDefinition encounterDefinition;

    private void OnTriggerEnter(Collider other)
    {
        encounterDefinition = gameObject.GetComponent<EncounterDefinition>();

        if (other.gameObject.CompareTag("Player"))
        {
            if (encounterDefinition.isBattleStarted == true) return;
            encounterDefinition.ChamarBatalha();
        }
    }
}