using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameJuices : MonoBehaviour
{
    public GameObject CanvasGameJuices;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CanvasGameJuices.SetActive(true);

            GetComponent<Animator>().SetBool("Trigger", true);
        }
    }
}
