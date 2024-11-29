using Main_Folders.Scripts.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyMovementStates : MonoBehaviour
{
    public enum State
    {
        Idle,
        Patrol,
        Follow,
        Battle,
        Dead
    }

    [Header("Idle Settings")]
    public float idleTime = 5f; // Tempo de idle em segundos

    [Header("Patrol Settings")]
    protected int _waypointIndex;
    protected GameObject _player;
    public Waypoint[] _waypoints;
    protected NavMeshAgent _agent;
    [SerializeField] protected GameObject _targetPos;
    [SerializeField] protected float _distanceToTarget;
    protected Animator _animator;
    protected State _currentState;
    private Vector3 _startPos;
    protected Unit unitComponent;

    [SerializeField] internal bool isDialogueInProgress = false; // Flag para verificar se o diálogo está em andamento

    private void Start()
    {
        unitComponent = gameObject.GetComponent<Unit>();
        InitialSetup();
        SwitchStates(State.Idle);
    }

    private void InitialSetup()
    {
        _startPos = transform.position;
        _waypointIndex = 0;
        _player = GameObject.Find("Player");
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private IEnumerator HandleIdle()
    {
        Debug.Log("Entering Idle state.");
        _animator.SetBool("IsWalking", false);
        yield return new WaitForSeconds(idleTime);
        Debug.Log("Idle time completed. Switching to Patrol.");
        SwitchStates(State.Patrol);
    }

    protected abstract void HandlePatrol();

    private IEnumerator HandleFollow()
    {
        if (_player == null)
        {
            SwitchStates(State.Idle);
            yield break;
        }

        while (true)
        {
            _distanceToTarget = Vector3.Distance(transform.position, _player.transform.position);

            if (_distanceToTarget <= 8)
            {
                _agent.destination = _player.transform.position;
                _agent.speed = 1.25f;

                if (_distanceToTarget <= 2 && !isDialogueInProgress)
                {
                    StopMovement();
                    Debug.Log("Entering start dialog Idle state.");
                    StartDialogue(); // Inicia o diálogo
                    _animator.SetBool("IsWalking", false);
                    yield break; // Interrompe a execução da corrotina
                }
            }
            else
            {
                SwitchStates(State.Patrol);
                _agent.speed = 1;
                yield break; // Interrompe a execução se a distância for maior que 8
            }

            yield return null; // Pausa até o próximo frame
        }
    }

    private IEnumerator HandleBattle()
    {
        _animator.SetBool("OnBattle", true);

        yield return new WaitForSeconds(3);

        GetComponent<QuestLacaio>().isDialogueFinished = true; // Inicia o combate após a animação, pois essa variável é analisada pelo EncounterDefinition

        _animator.SetBool("OnBattle", false);

        LevelsManager.Instance.isTalking = false;
        GetComponent<EnemyMovementStates>().isDialogueInProgress = false;
    }

    private void HandleDead()
    {
        gameObject.GetComponent<QuestLacaio>().CompleteQuest();
        _animator.SetTrigger("PlayerWin");
        Debug.Log("Iniciar animação de morte");
    }

    public void SwitchStates(State state)
    {
        Debug.Log("Switching state to " + state);
        _currentState = state;
        StopAllCoroutines();
        switch (state)
        {
            case State.Idle:
                StartCoroutine(HandleIdle());
                break;
            case State.Patrol:
                HandlePatrol();
                break;
            case State.Follow:
                StartCoroutine(HandleFollow());
                break;
            case State.Battle:
                StartCoroutine(HandleBattle());
                break;
            case State.Dead:
                HandleDead();
                break;
        }
    }

    public void StartDialogue()
    {
        QuestLacaio questLacaio = GetComponent<QuestLacaio>();
        if (questLacaio != null)
        {
            isDialogueInProgress = true; // Marca que o diálogo está em andamento
            questLacaio.StartDialogue(); // Inicia o diálogo com o lacaio
        }
    }

    public void OnDialogueEnded()
    {
        Debug.Log("DIALOGO ACABOU? ENEMY STATE");
        isDialogueInProgress = false; // Marca o fim do diálogo
        SwitchStates(State.Battle); 
    }

    // Método para parar o movimento
    public void StopMovement()
    {
        _agent.SetDestination(transform.position); // Impede o movimento do agente
    }

    private void Update()
    {
        Debug.Log($"{gameObject.name} + {_currentState}");
    }
}