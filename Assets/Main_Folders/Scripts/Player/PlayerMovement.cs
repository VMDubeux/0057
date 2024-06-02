using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animatorController;
    private PartyManager partyManager;
    public float moveSpeed = 5;
    public Vector3 moveInput;

    [SerializeField] private GameObject brute;
    [SerializeField] private GameObject batato;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animatorController = GetComponentInChildren<Animator>();
        partyManager = FindFirstObjectByType<PartyManager>();

        if (partyManager.GetPosition() != Vector3.zero)
        {
            transform.position = partyManager.GetPosition();
        }
    }

    void Update()
    {
        if (DialogueManager.isChatting == false)
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

    public void GiveDripToPlayer()
    {
        brute.SetActive(true);
        batato.SetActive(false);

        animatorController = brute.GetComponent<Animator>();

    }
}
