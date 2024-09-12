using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemies"))
        {
            Debug.Log("Entrou!");

            EnemyMovementStates movementState = other.GetComponent<EnemyMovementStates>();

            if (movementState != null)
                movementState.SwitchStates(EnemyMovementStates.State.Idle);
        }
    }
}
