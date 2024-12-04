using Main_Folders.Scripts.Minimapa;
using UnityEngine;

public class PortalPosition : MonoBehaviour
{
    void Start()
    {
        FindFirstObjectByType<CanvasMinimapa>()
            .transform
            .GetChild(0)
            .GetComponent<MarkerHolder>()?
            .AddPortalMarker(this);
    }
}
