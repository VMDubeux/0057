using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private int stepsInGrass;
    [SerializeField] private int minStepsToEncounter;
    [SerializeField] private int maxStepsToEncounter;

    private PlayerMovement playerMovement;
    private Vector3 movement;
    private bool movingInGrass;
    private float stepTimer;
    private int stepsToEncouter;
    private PartyManager partyManager;

    private const float TIME_PER_STEP = 0.5f;
    private const string BATTLE_SCENE = "LEVEL_BATTLE";

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        CalculateStepsToNextEncouter();
    }

    private void Start()
    {
        partyManager = GameObject.FindFirstObjectByType<PartyManager>();
        if (partyManager.GetPosition() != Vector3.zero)
        {
            transform.position = partyManager.GetPosition();
        }
    }

    void Update()
    {
        float x = playerMovement.moveInput.x;
        float z = playerMovement.moveInput.z;
        //print($"{x} , {z}");
        movement = new Vector3(x, 0, z).normalized;
    }

    private void FixedUpdate()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, 1, layerMask);
        movingInGrass = collider.Length != 0 && movement != Vector3.zero;

        if (movingInGrass)
        {
            stepTimer += Time.fixedDeltaTime;

            if (stepTimer > TIME_PER_STEP)
            {
                stepsInGrass++;
                stepTimer = 0;

                if (stepsInGrass >= stepsToEncouter)
                {
                    partyManager.SetPosition(transform.position);
                    SceneManager.LoadScene(BATTLE_SCENE);
                    //print("Change scene!");
                }
            }
        }
    }

    private void CalculateStepsToNextEncouter()
    {
        stepsToEncouter = Random.Range(minStepsToEncounter, maxStepsToEncounter);
    }
}