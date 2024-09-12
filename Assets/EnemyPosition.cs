using Main_Folders.Scripts.Minimapa;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPosition : MonoBehaviour
{
    void Start()
    {
        FindAnyObjectByType<MarkerHolder>().AddEnemyMarker(this);
    }

    void OnTriggerEnter()
    {
        FindAnyObjectByType<MarkerHolder>().AddEnemyMarker(this);
    }
}
