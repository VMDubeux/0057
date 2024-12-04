using Main_Folders.Scripts.Minimapa;
using UnityEngine;

public class TrunkPosition : MonoBehaviour
{
    void Start()
    {
        FindFirstObjectByType<CanvasMinimapa>()
            .transform
            .GetChild(0)
            .GetComponent<MarkerHolder>()?
            .AddTrunkMarker(this);
    }
}
