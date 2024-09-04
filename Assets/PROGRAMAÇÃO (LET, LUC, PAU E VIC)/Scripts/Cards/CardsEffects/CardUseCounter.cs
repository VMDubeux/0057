using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardUseCounter : MonoBehaviour
{
    public static int timesUsed = 0;

    void Start()
    {
        timesUsed = 0;
    }
    
    public void Increase()
    {
        timesUsed++;
    }

}
