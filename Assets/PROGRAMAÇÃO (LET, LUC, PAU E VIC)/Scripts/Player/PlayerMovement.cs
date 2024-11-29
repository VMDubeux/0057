using Main_Folders.Scripts.Managers;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animatorController;
    private PartyManager partyManager;
    private bool isMoving;
    private float originalSpeed; // Armazena a velocidade original do NavMeshAgent

    [SerializeField] private GameObject brute;
    [SerializeField] private GameObject bruteVisual;
    [SerializeField] private GameObject batato;
    [SerializeField] private GameObject batatoVisual;

    [SerializeField] private LayerMask walkableLayer;

    // Flag para bloquear a movimentação
    public static bool isMovementBlocked = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animatorController = GetComponentInChildren<Animator>();
        partyManager = FindAnyObjectByType<PartyManager>();
        originalSpeed = navMeshAgent.speed; // Armazena a velocidade original ao iniciar

        navMeshAgent.updateRotation = false;
        StartCoroutine(StartDrip());
    }

    private IEnumerator StartDrip()
    {
        yield return new WaitForSeconds(2);
        partyManager.ChosenDrip(batato, brute, batatoVisual);
    }

    void Update()
    {
        Debug.Log($"Está bloqueado ? {isMovementBlocked}");

        // Bloqueia a movimentação durante o diálogo ou combate
        if (isMovementBlocked)
        {
            navMeshAgent.ResetPath(); // Limpa qualquer destino pendente
            StopMovement();
            return;
        }

        if (!DialogueManager.isChatting)
        {
            HandleInput();
            AdjustSpeed();
            MoveToTarget();
            partyManager.ChangeExpSliderValue();
        }
    }

    private void HandleInput()
    {
        if (Input.GetMouseButton(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, walkableLayer))
            {
                navMeshAgent.SetDestination(hit.point);
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
        }
    }

    private void AdjustSpeed()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            navMeshAgent.speed = originalSpeed * 2; // Dobra a velocidade ao pressionar Shift
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            navMeshAgent.speed = originalSpeed; // Retorna à velocidade original ao soltar Shift
        }
    }

    private void MoveToTarget()
    {
        if (isMoving)
        {
            animatorController.SetBool("run", true);

            if (Vector3.Distance(transform.position, navMeshAgent.destination) < 0.1f)
            {
                isMoving = false;
                int random = Random.Range(1, 4);
                animatorController.SetInteger("Idle", random);
                animatorController.SetBool("run", false);
            }

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

    private void StopMovement()
    {
        navMeshAgent.SetDestination(transform.position); // Para o agente no local atual
        animatorController.SetBool("run", false); // Interrompe a animação de movimento
        animatorController.SetInteger("Idle", 1); // Interrompe a animação de movimento
        isMoving = false; // Reseta o estado de movimento
    }

    public void GiveDripToPlayer()
    {
        brute.SetActive(true);
        batato.SetActive(false);

        animatorController = brute.GetComponent<Animator>();

        partyManager.ChosenDrip(brute, batato, bruteVisual);
    }
}
