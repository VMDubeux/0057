using System.Collections;
using Main_Folders.Scripts.Player;
using UnityEngine;
using Main_Folders.Scripts.Player;
using Main_Folders.Scripts.UI;
using Assets.PROGRAMAÇÃO__LET__LUC__PAU_E_VIC_.Scripts.GameJuices;
using System.Collections.Generic;

public class GameJuiceMentor : GameJuices
{
    private MentorFirstDialogue mentorDialog;

    [SerializeField] private CardToPickUp[] _CardsToDrop = new CardToPickUp[3];

    protected override void Start()
    {
        mentorDialog = GetComponent<MentorFirstDialogue>();
    }

    protected override void HandleTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !wasOpen)
        {
            CanvasGameJuices.SetActive(true);
            isInside = true;
        }
    }

    protected override void HandleTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CanvasGameJuices.SetActive(false);
            isInside = false;
            wasOpen = false;
            // Remove the subscription to the event
            ShopTriggerCollider.OnPlayerEntered -= Verification;
            LevelsManager.Instance.isTalking = false;
            Destroy(mentorDialog.dialogTrigger.gameObject);
        }
    }

    protected override IEnumerator IsInside()
    {
        if (isInside && Input.GetKeyDown(KeyCode.E))
        {
            HandleButtonPress();
            yield return new WaitForSeconds(2.5f);
            PerformDelegate();
        }
    }

    protected override void HandleButtonPress()
    {
        wasOpen = true;
        CanvasGameJuices.SetActive(false);
        mentorDialog.StartDialogue();
    }

    protected override void SetupReturnToOrigin()
    {
        throw new System.NotImplementedException();
    }

    protected override void AddRandomItemToInventory() // Não será randomico aqui
    {
        foreach(CardToPickUp card in _CardsToDrop)
            CardInventoryManager.Instance.CardPickedUp(card);
    }

    private void Verification()
    {
        Debug.Log("Verificando!");
    }
}