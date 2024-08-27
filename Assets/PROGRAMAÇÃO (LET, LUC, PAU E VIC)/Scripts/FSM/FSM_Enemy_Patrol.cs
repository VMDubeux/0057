using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_Enemy_Patrol : StateMachineBehaviour
{
    private GameObject Player = null;
    private GameObject[] Waypoints = null;
    private int WaypointEscolhido = 0;
    private NavMeshAgent Agente;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player = GameObject.Find("Player");

        Waypoints = GameObject.FindGameObjectsWithTag("Waypoint");

        Agente = animator.GetComponent<NavMeshAgent>();

        if (Waypoints != null)
            WaypointEscolhido = Random.Range(0, Waypoints.Length);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Waypoints != null)
        {
            Agente.destination = Waypoints[WaypointEscolhido].transform.position;

        if(Vector3.Distance(animator.transform.position, Waypoints[WaypointEscolhido].transform.position) < .5f)
            {
                WaypointEscolhido = Random.Range(0,Waypoints.Length);
            }

        if(Player != null)
            {
                float distance = Vector3.Distance(animator.transform.position, Player.transform.position);


                animator.SetFloat("Distance", distance);
                
            }


        }



    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
