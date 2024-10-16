using System.Collections;
using UnityEngine;

namespace Assets.PROGRAMAÇÃO__LET__LUC__PAU_E_VIC_.Scripts.GameJuices
{
    public class GameJuiceTableStore : global::GameJuices
    {
        protected override void Start()
        {
            // Não implementar
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

                // Sempre volta à origem ao sair do trigger
                GetComponent<Animator>().SetBool("Trigger", false);
                wasOpen = false;

                // Remove the subscription to the event
                ShopTriggerCollider.OnPlayerEntered -= Verification;
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
            GetComponent<Animator>().SetBool("Trigger", true);
            wasOpen = true;
            CanvasGameJuices.SetActive(false);
            SetupReturnToOrigin();
        }

        protected override void AddRandomItemToInventory()
        {
            // Não é necessário implementar nada aqui
        }

        protected override void SetupReturnToOrigin()
        {
            if (gameObject.GetComponent<ShopTriggerCollider>() != null)
            {
                ShopTriggerCollider.OnPlayerEntered += Verification;
            }
        }

        private void Verification()
        {
            Debug.Log("Verificando!");
        }
    }
}