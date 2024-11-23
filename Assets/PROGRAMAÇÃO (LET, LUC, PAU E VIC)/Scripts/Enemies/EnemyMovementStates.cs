using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyMovementStates : MonoBehaviour
{
    public delegate void StartCombat();
    public event StartCombat OnStartCombat; // Evento para indicar o início do combate

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

    internal bool isDialogueInProgress = false; // Flag para verificar se o diálogo está em andamento

    private void Start()
    {
        InitialSetup();
        SwitchStates(State.Idle);
        unitComponent = gameObject.GetComponent<Unit>();
    }

    private void Update()
    {
        if (unitComponent != null && unitComponent.hasFought == false && !isDialogueInProgress)
        {
            switch (_currentState)
            {
                case State.Patrol:
                    HandlePatrolLogic();
                    break;

                case State.Follow:
                    HandleFollowLogic();
                    break;

                case State.Battle:
                    HandleBattleLogic();
                    break;

                case State.Dead:
                    HandleDeadLogic();
                    break;
            }
        }
        if(unitComponent.hasFought == true)
        {
            SwitchStates(State.Dead);
        }
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

    private void HandlePatrolLogic()
    {
        if (_targetPos != null)
        {
            _distanceToTarget = Vector3.Distance(transform.position, _targetPos.transform.position);
            _agent.speed = 1;

            if (_player != null && gameObject.GetComponent<QuestObjects>().isAvailable)
            {
                if (Vector3.Distance(transform.position, _player.transform.position) < 6 && !isDialogueInProgress)
                {
                    SwitchStates(State.Follow);
                }
            }
        }
    }

    private void HandleFollowLogic()
    {
        if (_player == null)
        {
            SwitchStates(State.Idle);
            return;
        }

        _distanceToTarget = Vector3.Distance(transform.position, _player.transform.position);
        _agent.destination = _player.transform.position;
        _agent.speed = 1.25f;

        if (_distanceToTarget > 8)
        {
            SwitchStates(State.Patrol);
            _agent.speed = 1;
        }
        else if (_distanceToTarget <= 2 && !isDialogueInProgress)
        {
            _agent.SetDestination(transform.position);
            SwitchStates(State.Idle); // Para o movimento do inimigo
            StartDialogue(); // Inicia o diálogo
        }
    }

    private void HandleBattleLogic()
    {
        if (OnStartCombat != null)
        {
            SwitchStates(State.Battle);
        }
        else if (gameObject.GetComponent<Unit>().hasFought)
        {
            SwitchStates(State.Dead);
        }
        else
        {
            _animator.SetBool("OnBattle", false);
            transform.position = _startPos;
            SwitchStates(State.Patrol);
        }
    }

    private void HandleDeadLogic()
    {
        gameObject.GetComponent<QuestLacaio>().CompleteQuest();
        _animator.SetTrigger("PlayerWin");
        Debug.Log("Iniciar animação de morte");
        _animator.SetBool("OnBattle", false);
    }

    private IEnumerator HandleBattle()
    {
        _agent.destination += new Vector3(2, 0, 2);

        yield return new WaitForSeconds(1);

        _animator.SetBool("OnBattle", true);
        _agent.speed = 1;
    }

    public void StartCombatLogic()
    {
        OnStartCombat?.Invoke(); // Invoca o evento
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
                HandleFollowLogic();
                break;
            case State.Battle:
                StartCoroutine(HandleBattle());
                break;
            case State.Dead:
                HandleDeadLogic();
                break;
        }
    }

    public void StartDialogue()
    {
        QuestLacaio questLacaio = GetComponent<QuestLacaio>();
        if (questLacaio != null)
        {
            questLacaio.StartDialogue(); // Inicia o diálogo com o lacaio
            isDialogueInProgress = true; // Marca que o diálogo está em andamento
        }
    }

    public void OnDialogueEnded()
    {
        isDialogueInProgress = false; // Marca o fim do diálogo
        StartCombatLogic(); // Inicia o combate após o término do diálogo
    }

    // Método para parar o movimento
    public void StopMovement()
    {
        _agent.isStopped = true; // Impede o movimento do agente
    }
}
