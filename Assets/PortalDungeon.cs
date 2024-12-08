using Main_Folders.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PortalDungeon : MonoBehaviour
{
    [SerializeField] private EncounterDefinition encounterDefinition;
    [SerializeField] private GameObject newPlayerPos;

    private void OnTriggerEnter(Collider other)
    {
        encounterDefinition = gameObject.GetComponent<EncounterDefinition>();

        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.position = newPlayerPos.transform.position;
            other.GetComponent<NavMeshAgent>().Warp(newPlayerPos.transform.position);
            encounterDefinition.ChamarBatalha();
        }
    }
}