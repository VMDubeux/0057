using Main_Folders.Scripts.Minimapa;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorPosition : MonoBehaviour
{
    void Start()
    {
        FindFirstObjectByType<CanvasMinimapa>().transform.GetChild(0).GetComponent<MarkerHolder>()?.AddVendorMarker(this.gameObject.GetComponent<VendorPosition>());
    }
}
