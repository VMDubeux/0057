using Main_Folders.Scripts.Minimapa;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    void Start()
    {
        FindFirstObjectByType<CanvasMinimapa>()
            .transform
            .GetChild(0)
            .GetComponent<MarkerHolder>()?
            .AddPlayerMarker(this);
    }
}
