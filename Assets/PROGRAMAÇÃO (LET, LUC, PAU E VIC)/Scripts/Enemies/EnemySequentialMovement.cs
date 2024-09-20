using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySequentialMovement : EnemyMovementStates
{
    protected override void HandlePatrol()
    {
        if (_waypoints.Length == 0)
        {
            SwitchStates(State.Idle);
        }

        foreach (var waypoint in _waypoints)
        {
            waypoint.gameObject.SetActive(false);
        }

        var selectedWaypoint = _waypoints[base._waypointIndex];
        _targetPos = selectedWaypoint.gameObject;

        _targetPos.SetActive(true);

        _agent.destination = _targetPos.transform.position;
        _animator.SetBool("IsWalking", true);

        Debug.Log($"Patrolling to {_targetPos.name}");
        
        WaypointIndexReset();
    }

    private void WaypointIndexReset()
    {
        if (base._waypointIndex < _waypoints.Length - 1)
            base._waypointIndex++;
        else
            base._waypointIndex = 0;
    }
}
