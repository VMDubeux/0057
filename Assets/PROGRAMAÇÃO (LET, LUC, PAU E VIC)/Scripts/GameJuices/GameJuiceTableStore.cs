using System.Collections;
using UnityEngine;

namespace Assets.PROGRAMAÇÃO__LET__LUC__PAU_E_VIC_.Scripts.GameJuices
{
    public class GameJuiceTableStore : global::GameJuices
    {
        [SerializeField] private GameObject canvasStore; // Referência ao Canvas.
        private Animator animator; // Referência ao Animator.
        private bool isAnimating = false; // Controle para evitar sobreposição de animações.
        private bool isExiting = false; // Controle para animação de saída.

        protected override void Start()
        {
            animator = GetComponent<Animator>();

            // Localiza o Canvas e desativa inicialmente.
            canvasStore = FindFirstObjectByType<CanvasStore>(FindObjectsInactive.Include)?.gameObject;
            if (canvasStore != null)
            {
                canvasStore.SetActive(false);
            }
        }

        protected override void HandleTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !wasOpen && !isExiting)
            {
                CanvasGameJuices.transform.GetChild(0).gameObject.SetActive(true);
                isInside = true;
            }
        }

        protected override void HandleTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                isInside = false;
                StartCoroutine(HandleExitNoAnimation());
            }
        }

        private IEnumerator HandleExitNoAnimation() 
        {
            isExiting = true;

            // Toca a animação de saída.
            animator.SetBool("Trigger", false);

            CanvasGameJuices.transform.GetChild(0).gameObject.SetActive(false);

            // Reseta estados para permitir reaproximação.
            wasOpen = false;
            isAnimating = false;
            isExiting = false;

            yield return null;
        }

        private IEnumerator HandleExitAnimation()
        {
            isExiting = true;

            // Toca a animação de saída.
            animator.SetBool("Trigger", false);

            // Aguarda o término da animação de saída.
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

            // Reseta estados para permitir reaproximação.
            wasOpen = false;
            isAnimating = false;
            isExiting = false;
        }

        protected override IEnumerator IsInside()
        {
            // Verifica entrada do jogador e interações dentro do trigger.
            if (isInside && Input.GetKeyDown(KeyCode.E) && !isAnimating)
            {
                HandleButtonPress();
            }

            yield return null;
        }

        protected override void HandleButtonPress()
        {
            animator.SetBool("Trigger", true);
            wasOpen = true;
            isAnimating = true;
            CanvasGameJuices.transform.GetChild(0).gameObject.SetActive(false);
            StartCoroutine(OpenStoreCanvas());
        }

        private IEnumerator OpenStoreCanvas()
        {
            // Aguarda a conclusão da animação antes de ativar o Canvas.
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

            if (canvasStore != null)
            {
                canvasStore.SetActive(true);
            }

            // Espera até que o jogador pressione a tecla Q para fechar o Canvas.
            while (true)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    canvasStore.SetActive(false);
                    StartCoroutine(HandleExitAnimation());
                    break;
                }
                yield return null;
            }
        }

        internal override void AddRandomItemToInventory()
        {
            // Não necessário neste caso.
        }

        protected override void SetupReturnToOrigin()
        {
            // Não necessário neste caso.
        }
    }
}
