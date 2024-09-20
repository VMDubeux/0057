using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;

public abstract class EnemyMovementStates : MonoBehaviour
{
    public delegate void StartCombat();
    public static event StartCombat OnStartCombat;

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

    private void Start()
    {
        InitialSetup();
        SwitchStates(State.Idle);
    }

    private void Update()
    {
        if (_targetPos != null && _currentState == State.Patrol)
        {
            _distanceToTarget = Vector3.Distance(transform.position, _targetPos.transform.position);
            _agent.speed = 1;

            if (_player != null)
            {
                if (Vector3.Distance(transform.position, _player.transform.position) < 6)
                {
                    SwitchStates(State.Follow);
                }
            }

            return;
        }

        if (_player != null && _currentState == State.Follow)
        {
            _distanceToTarget = Vector3.Distance(transform.position, _player.transform.position);
            _agent.destination = _player.transform.position;
            _agent.speed = 1.25f;

            if (_distanceToTarget > 8)
            {
                SwitchStates(State.Patrol);
                _agent.speed = 1;
            }


            if (OnStartCombat != null)
            {
                SwitchStates(State.Battle);
            }

            return;
        }

        if (OnStartCombat == null && _currentState == State.Battle)
        {
            if (gameObject.GetComponent<Unit>().hasFought == true)
            {
                SwitchStates(State.Dead);
            }
            else
            {
                _animator.SetBool("OnBattle", false);
                gameObject.transform.position = _startPos;
                SwitchStates(State.Patrol);
            }
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

    protected void HandleFollow()
    {
        if (_player == null)
        {
            SwitchStates(State.Idle);
            return;
        }

        _animator.SetBool("IsWalking", true);
    }

    protected IEnumerator HandleBattle()
    {
        _agent.destination += new Vector3(2, 0, 2);

        yield return new WaitForSeconds(1);

        _animator.SetBool("OnBattle", true);

        _agent.speed = 1;
    }

    protected void HandleDead()
    {
        _animator.SetTrigger("PlayerWin");
        Debug.Log("Iniciar animação de morte");
        _animator.SetBool("OnBattle", false);
        //Destroy(gameObject);
        //Substituir por animação de morte depois
        // Inserir um delegate para somente destruir o objeto ou tocar a animação após o drop da carta
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
                HandleFollow();
                break;
            case State.Battle:
                StartCoroutine(HandleBattle());
                break;
            case State.Dead:
                HandleDead();
                break;
        }
    }
}
