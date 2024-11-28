using System.Collections;
using UnityEngine;

public class EnemyRandomMovement : EnemyMovementStates
{
    protected override void HandlePatrol()
    {
        if (unitComponent != null && (unitComponent.hasFought || isDialogueInProgress))
        {
            return;
        }

        if (_waypoints.Length == 0)
        {
            SwitchStates(State.Idle);
            return;
        }

        foreach (var waypoint in _waypoints)
        {
            waypoint.gameObject.SetActive(false);
        }

        base._waypointIndex = Random.Range(0, _waypoints.Length);
        var selectedWaypoint = _waypoints[base._waypointIndex];
        _targetPos = selectedWaypoint.gameObject;
        _targetPos.SetActive(true);

        _agent.speed = 1;
        _agent.destination = _targetPos.transform.position;
        _animator.SetBool("IsWalking", true);

        Debug.Log($"Patrolling to {_targetPos.name}");

        StartCoroutine(PatrolToTarget());
    }

    private IEnumerator PatrolToTarget()
    {
        while (Vector3.Distance(transform.position, _targetPos.transform.position) > 0.5f)
        {
            if (_player != null && Vector3.Distance(transform.position, _player.transform.position) < 6 && !isDialogueInProgress)
            {
                SwitchStates(State.Follow);
                yield break;
            }
            yield return null;
        }

        SwitchStates(State.Idle);
    }
}
