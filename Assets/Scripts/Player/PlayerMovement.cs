using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    public Animator animatorController;
    public float moveSpeed = 5;
    Vector3 moveInput;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if(DialogueManager.isChatting == false)
        {
        GatherInput();
        }

    }

    private void GatherInput()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            //Input de movimento do player 
            moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            //Corrige movimento pela visao isometrica
            var moveFixed = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0)).MultiplyPoint3x4(moveInput);
            characterController.Move(moveFixed * Time.deltaTime * moveSpeed);
            //animacao de corrida
            animatorController.SetBool("run", true);
            //rotacao durante movimento do player
            var relative = (transform.position + moveFixed) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);
            transform.rotation = rot;
        }
        else
        {
            //parar animacao de corrida
            animatorController.SetBool("run", false);
        }
    }
}
