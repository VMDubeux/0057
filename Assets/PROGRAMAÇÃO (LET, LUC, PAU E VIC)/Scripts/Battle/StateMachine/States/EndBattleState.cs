using System.Collections;
using System.Collections.Generic;
using Main_Folders.Scripts.StateMachine.States;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndBattleState : State
{
    [SerializeField] EncounterSystem encounterSystem;

    public override IEnumerator Enter()
    {
        encounterSystem = FindAnyObjectByType<EncounterSystem>(FindObjectsInactive.Include).GetComponent<EncounterSystem>();
        Debug.Log("Battle ended");
        if (encounterSystem.prefab.name == "Miniboss")
        {
            Debug.Log("Battle ended. Venceu Miniboss 1");
            encounterSystem.prefab.GetComponent<EncounterDefinition>().ChamarBatalhaBoss();
            Debug.Log("Battle ended. Venceu Miniboss 2");
            SceneLoader.UnloadBattleScene(7);
            yield break;
        }
        if (encounterSystem.prefab.name == "Boss")
        {
            Debug.Log("Battle ended. Venceu Boss 1");
            SceneLoader.LoadScene(1, SceneLoader.LoadType.Normal); // Chamar vitória, mas no momento chamará menu
            Debug.Log("Battle ended. Venceu Boss 2");
            SceneLoader.UnloadBattleScene(7);
            yield break;
        }

        yield return null;

        SceneLoader.UnloadBattleScene(4);
    }
}