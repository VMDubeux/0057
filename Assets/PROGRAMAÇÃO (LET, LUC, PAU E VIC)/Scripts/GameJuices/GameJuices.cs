using Main_Folders.Scripts.Shop;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameJuices : MonoBehaviour
{
    public delegate void PressedButton();
    public static event PressedButton OnPressedButton;

    public GameObject CanvasGameJuices;
    public GameObject LightGameJuice;
    public GameObject ParticleGameJuice;
    [SerializeField] private bool isInside = false;
    [SerializeField] private bool wasOpen = false;
    public bool GobackToOrigin;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && wasOpen == false)
        {
            CanvasGameJuices.SetActive(true);
            isInside = true;
        }
    }

    private IEnumerator IsInside()
    {
        if (isInside == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GetComponent<Animator>().SetBool("Trigger", true);
                wasOpen = true;
                CanvasGameJuices.SetActive(false);

                if (LightGameJuice != null)
                    LightGameJuice.SetActive(true);

                if (ParticleGameJuice != null)
                    ParticleGameJuice.SetActive(true);

                if (GobackToOrigin == true)
                {
                    if (gameObject.GetComponent<ShopTriggerCollider>() != null)
                        ShopTriggerCollider.OnPlayerEntered += Verification;

                    yield return new WaitForSeconds(2.5f);
                    OnPressedButton();
                }
            }
        }
    }

    private void Verification()
    {

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CanvasGameJuices.SetActive(false);
            isInside = false;

            if (GobackToOrigin == true)
            {
                GetComponent<Animator>().SetBool("Trigger", false);
                wasOpen = false;
            }

            OnPressedButton = null;
        }
    }

    private void Update()
    {
        StartCoroutine(IsInside());
    }
}