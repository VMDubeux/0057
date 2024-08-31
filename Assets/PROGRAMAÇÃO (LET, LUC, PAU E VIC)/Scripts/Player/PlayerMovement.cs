using Main_Folders.Scripts.Managers;
using UnityEngine;
using UnityEngine.AI; // Necess�rio para trabalhar com NavMeshAgent
using UnityEngine.EventSystems; // Necess�rio para verificar intera��es com UI

public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent; // Usando NavMeshAgent em vez de Rigidbody
    private Animator animatorController;
    private PartyManager partyManager;
    private bool isMoving;

    [SerializeField] private GameObject brute;
    [SerializeField] private GameObject batato;

    [SerializeField] private LayerMask walkableLayer; // Camada que define o que � caminh�vel

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animatorController = GetComponentInChildren<Animator>();
        partyManager = FindAnyObjectByType<PartyManager>();

        // Configura o NavMeshAgent
        navMeshAgent.updateRotation = false; // Para controlar a rota��o manualmente
    }

    void Update()
    {
        if (!DialogueManager.isChatting)
        {
            HandleInput();
            MoveToTarget();
            partyManager.ChangeExpSliderValue();
        }
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0)) // Detecta clique do mouse
        {
            // Verifica se o clique foi em um elemento da UI
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return; // N�o faz nada se o clique foi sobre a UI
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Verifica se o clique do mouse est� em uma �rea caminh�vel
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, walkableLayer))
            {
                // Define o destino do NavMeshAgent para o ponto clicado
                navMeshAgent.SetDestination(hit.point);
                isMoving = true; // Inicia a movimenta��o
            }
            else
            {
                // Caso n�o esteja em uma �rea caminh�vel, define a movimenta��o como falsa
                isMoving = false;
            }
        }
    }

    private void MoveToTarget()
    {
        if (isMoving)
        {
            // Atualiza a anima��o
            animatorController.SetBool("run", true);

            // Verifica se o personagem chegou ao destino
            if (Vector3.Distance(transform.position, navMeshAgent.destination) < 0.1f)
            {
                isMoving = false;
                animatorController.SetBool("run", false);
            }

            // Controla a rota��o manualmente para que o personagem se alinhe com a dire��o do movimento
            Vector3 direction = navMeshAgent.velocity.normalized;
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, navMeshAgent.angularSpeed * Time.deltaTime);
            }
        }
        else
        {
            animatorController.SetBool("run", false);
        }
    }

    public void GiveDripToPlayer()
    {
        brute.SetActive(true);
        batato.SetActive(false);

        animatorController = brute.GetComponent<Animator>();

        partyManager.ChosenDrip(brute, batato);
    }
}
