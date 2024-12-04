using Main_Folders.Scripts.Minimapa;
using UnityEngine;

public class VendorPosition : MonoBehaviour
{
    void Start()
    {
        FindFirstObjectByType<CanvasMinimapa>().transform.GetChild(0).GetComponent<MarkerHolder>()?.AddVendorMarker(this.gameObject.GetComponent<VendorPosition>());
    }
}
