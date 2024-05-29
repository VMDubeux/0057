using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBeginState : State
{
    [SerializeField] PartyManager partyManager;

    [SerializeField] EncounterSystem encounterSystem;

    [SerializeField] PlayerUnit _playerUnit;

    [SerializeField] float accumulatedExperience = 0;

    public override IEnumerator Enter()
    {
        machine.CurrentUnit = null;
        while (machine.CurrentUnit == null && machine.Units.Count > 0)
        {
            machine.CurrentUnit = machine.Units.Dequeue();
            if (machine.CurrentUnit.GetStatValue(1) <= 0)
            {
                Debug.LogFormat("Unit {0} tried to play, but is dead", machine.CurrentUnit);
                AccumulatedExperienceForThePlayer(machine.CurrentUnit.gameObject.GetComponent<Unit>().expToGive); // Envia o valor de experiencia para o método de acúmulo, durante a batalha.
                print(accumulatedExperience);
                machine.CurrentUnit = null;
            }
            else
            {
                machine.Units.Enqueue(machine.CurrentUnit);
            }
        }

        if (_playerUnit == null)
        {
            _playerUnit = machine.CurrentUnit as PlayerUnit;
        }

        yield return null;
        if (machine.Units.Count == 1 || _playerUnit.HP <= 0)
        {
            if (_playerUnit.HP > 0)
            {
                partyManager = GameObject.Find("PartyManager").GetComponent<PartyManager>();
                partyManager.SetExperience(accumulatedExperience); // Envio do quantitativo acumulado de experiência para o player, mediante uso do método constante no script Party Manager.
                encounterSystem = FindAnyObjectByType<EncounterSystem>(FindObjectsInactive.Include).GetComponent<EncounterSystem>();
                encounterSystem.prefab.GetComponent<Unit>().hasFought = true;
                encounterSystem.battleActive = false;
            }
            StartCoroutine(WaitThenChangeState<EndBattleState>());
        }
        else
        {
            StartCoroutine(WaitThenChangeState<RecoveryState>());
        }
    }

    private void AccumulatedExperienceForThePlayer(float exp) // Acumula a experiencia durante a batalha para, em caso de vitória, ser transferida ao jogador.
    {
        accumulatedExperience += exp;
    }
}
