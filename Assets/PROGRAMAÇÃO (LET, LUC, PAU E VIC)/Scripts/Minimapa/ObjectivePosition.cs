using Main_Folders.Scripts.Minimapa;
using UnityEngine;

public class ObjectivePosition : MonoBehaviour
{
    void Start()
    {
        FindAnyObjectByType<MarkerHolder>().AddObjectiveMarker(this);
    }

    void OnTriggerEnter()
    {
        FindAnyObjectByType<MarkerHolder>().RemoveObjectiveMarker(this);
    }
}
