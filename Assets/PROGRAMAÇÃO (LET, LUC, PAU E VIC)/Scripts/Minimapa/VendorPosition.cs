using Main_Folders.Scripts.Minimapa;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorPosition : MonoBehaviour
{
    void Start()
    {
        FindAnyObjectByType<MarkerHolder>().AddVendorMarker(this);
    }
}
