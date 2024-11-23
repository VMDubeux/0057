using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomMovement : EnemyMovementStates
{
    protected override void HandlePatrol()
    {
        // Verifica se o inimigo já lutou ou se o diálogo está em andamento
        if (unitComponent != null && (unitComponent.hasFought || isDialogueInProgress))
        {
            // Se o inimigo lutou ou está em diálogo, ele não deve se mover
            return;
        }

        if (_waypoints.Length == 0)
        {
            SwitchStates(State.Idle);
        }

        foreach (var waypoint in _waypoints)
        {
            waypoint.gameObject.SetActive(false);
        }

        base._waypointIndex = Random.Range(0, _waypoints.Length);
        var selectedWaypoint = _waypoints[base._waypointIndex];
        _targetPos = selectedWaypoint.gameObject;

        _targetPos.SetActive(true);

        _agent.destination = _targetPos.transform.position;
        _animator.SetBool("IsWalking", true);

        Debug.Log($"Patrolling to {_targetPos.name}");
    }
}
