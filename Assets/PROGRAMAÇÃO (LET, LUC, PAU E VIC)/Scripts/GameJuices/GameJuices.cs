using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameJuices : MonoBehaviour
{
    public GameObject CanvasGameJuices;
    public GameObject LightGameJuice;
    public GameObject ParticleGameJuice;
    [SerializeField] private bool isInside = false;
    [SerializeField] private bool wasOpen = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && wasOpen == false)
        {
            CanvasGameJuices.SetActive(true);
            isInside = true;
        }
    }

    private void IsInside()
    {
        if (isInside == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GetComponent<Animator>().SetBool("Trigger", true);
                wasOpen = true;
                CanvasGameJuices.SetActive(false);
                LightGameJuice.SetActive(true);
                ParticleGameJuice.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CanvasGameJuices.SetActive(false);
            isInside = false;
        }
    }

    private void Update()
    {
        IsInside();
    }
}