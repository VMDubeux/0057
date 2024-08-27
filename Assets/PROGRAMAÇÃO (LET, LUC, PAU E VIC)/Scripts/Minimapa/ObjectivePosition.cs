using Unity.VisualScripting;
using UnityEngine;

namespace Main_Folders.Scripts.Minimapa
{
    public class ObjectivePosition : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            FindObjectOfType<MarkerHolder>().AddObjectiveMarker(this);
        }

        void OnTriggerEnter()
        {
            FindObjectOfType<MarkerHolder>().RemoveObjectiveMarker(this);
        }
    }
}
