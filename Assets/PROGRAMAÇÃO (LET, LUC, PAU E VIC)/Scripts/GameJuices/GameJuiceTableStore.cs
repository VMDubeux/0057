using System.Collections;
using UnityEngine;

namespace Assets.PROGRAMAÇÃO__LET__LUC__PAU_E_VIC_.Scripts.GameJuices
{
    public class GameJuiceTableStore : global::GameJuices
    {
        [SerializeField] private GameObject canvasStore;

        protected override void Start()
        {
            canvasStore = FindFirstObjectByType<CanvasStore>(FindObjectsInactive.Include).gameObject;
            canvasStore.SetActive(false);
        }

        protected override void HandleTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !wasOpen)
            {
                CanvasGameJuices.transform.GetChild(0).gameObject.SetActive(true);

                isInside = true;
            }
        }

        protected override void HandleTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                CanvasGameJuices.transform.GetChild(0).gameObject.SetActive(false);
                isInside = false;

                // Sempre volta � origem ao sair do trigger
                GetComponent<Animator>().SetBool("Trigger", false);
                wasOpen = false;

                // Remove the subscription to the event
                //ShopTriggerCollider.OnPlayerEntered -= Verification;
            }
        }

        protected override IEnumerator IsInside()
        {
            if (isInside && Input.GetKeyDown(KeyCode.E))
            {
                HandleButtonPress();
            }

            yield return null;
        }

        protected override void HandleButtonPress()
        {
            GetComponent<Animator>().SetBool("Trigger", true);
            wasOpen = true;
            CanvasGameJuices.SetActive(false);
            //ShopUserInterface.OnPlayerEntered += Verification;
            StartCoroutine(OpenStoreCanvas());
        }

        internal override void AddRandomItemToInventory()
        {
            // N�o � necess�rio implementar nada aqui
        }

        protected override void SetupReturnToOrigin()
        {
            // Não precisa
        }

        private IEnumerator OpenStoreCanvas()
        {
            yield return new WaitForSeconds(2.5f);
            canvasStore.SetActive(true);
            while (true)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    canvasStore.SetActive(false);
                    break;
                }
            }
        }
    }
}