using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DriplessMovement : MonoBehaviour
{
    public enum State
    {
        Idle,
        Patrol
    }

    [Header("Idle Settings")]
    public float idleTime = 12f; // Tempo de idle em segundos

    [Header("Patrol Settings")]
    protected int _waypointIndex;
    public Waypoint[] _waypoints;
    protected NavMeshAgent _agent;
    [SerializeField] protected GameObject _targetPos;
    [SerializeField] protected float _distanceToTarget;
    protected Animator _animator;
    protected State _currentState;
    private Vector3 _startPos;
    private float rotationSpeed = 100;

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

            Vector3 direction = _agent.destination - transform.position;
            direction.y = 0; // Mantém no plano horizontal

            if (direction.magnitude > 0.1f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-direction), Time.deltaTime * rotationSpeed);
            }
            _agent.speed = 1;

            return;
        }
    }

    private void InitialSetup()
    {
        _startPos = transform.position;
        _waypointIndex = 0;
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private IEnumerator HandleIdle()
    {
        Debug.Log("Entering Idle state.");
        _animator.SetBool("run", false);
        int random = Random.Range(1, 4);
        _animator.SetInteger("IdleIndex", random);
        yield return new WaitForSeconds(idleTime);
        Debug.Log($"Batato {gameObject.name}: Idle time completed. Switching to Patrol.");
        SwitchStates(State.Patrol);
    }

    private void HandlePatrol()
    {
        if (_waypoints.Length == 0)
        {
            SwitchStates(State.Idle);
        }

        foreach (var waypoint in _waypoints)
        {
            waypoint.gameObject.SetActive(false);
        }

        _waypointIndex = Random.Range(0, _waypoints.Length);
        var selectedWaypoint = _waypoints[_waypointIndex];
        _targetPos = selectedWaypoint.gameObject;

        _targetPos.SetActive(true);

        _agent.destination = _targetPos.transform.position;
        _animator.SetBool("run", true);

        Debug.Log($"Batato {gameObject.name} is patrolling to {_targetPos.name}");
    }

    public void SwitchStates(State state)
    {
        Debug.Log($"Batato {gameObject.name} is switching state to " + state);
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
        }
    }
}
