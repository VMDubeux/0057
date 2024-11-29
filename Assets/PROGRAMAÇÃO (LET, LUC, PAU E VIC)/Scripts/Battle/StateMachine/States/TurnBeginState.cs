using System.Collections;
using Main_Folders.Scripts.Managers;
using Main_Folders.Scripts.UI;
using Main_Folders.Scripts.Units;
using UnityEngine;
using UnityEngine.AI;

namespace Main_Folders.Scripts.StateMachine.States
{
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
                    /*if (machine.CurrentUnit.GetStatValue(1) > 0)
                    {
                        machine.CurrentUnit.SetStatValue(1, -1);
                        machine.CurrentUnit.SetStatValue(1, machine.CurrentUnit.GetStatValue(4) - 1);
                    }
                    else
                    {*/
                    Debug.LogFormat("Unit {0} tried to play, but is dead", machine.CurrentUnit);
                    AccumulatedExperienceForThePlayer(machine.CurrentUnit.gameObject.GetComponent<Unit>().expToGive); // Envia o valor de experiencia para o método de acúmulo
                    print(accumulatedExperience);
                    machine.CurrentUnit = null;
                    //}
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
                GameObject player = GameObject.Find("Player");
                encounterSystem = FindAnyObjectByType<EncounterSystem>(FindObjectsInactive.Include).GetComponent<EncounterSystem>();

                if (_playerUnit.HP > 0) // inimigo derrotado
                {
                    partyManager = GameObject.Find("PartyManager").GetComponent<PartyManager>();
                    partyManager.SetExperience(0, accumulatedExperience); // Envio do quantitativo acumulado de experiência para o player
                    encounterSystem.prefab.GetComponent<Unit>().hasFought = true;

                    // Localiza o player utilizando a classe PlayerMovement
                    GameObject playerPos = FindFirstObjectByType<PlayerMovement>().gameObject;

                    // Desabilita o NavMeshAgent antes de alterar a posição diretamente
                    NavMeshAgent agent = player.GetComponent<NavMeshAgent>();
                    agent.enabled = false;

                    // Altera a posição diretamente
                    player.transform.position = playerPos.transform.position;

                    // Reabilita o NavMeshAgent e define o destino para garantir coerência na navegação
                    agent.enabled = true;
                    agent.ResetPath(); // Limpa qualquer destino pendente antes de definir um novo
                    agent.Warp(playerPos.transform.position); // Teletransporta sem cálculos de rota
                    PlayerMovement.isMovementBlocked = false;

                    encounterSystem.prefab.GetComponent<EnemyMovementStates>().SwitchStates(EnemyMovementStates.State.Dead);
                }
                else // player derrotado
                {
                    encounterSystem.prefab.GetComponent<EnemyMovementStates>().SwitchStates(EnemyMovementStates.State.Idle); // Status pós batalha perdida
                    // player retorna ao respawnPoint
                    GameObject respawnPoint = FindFirstObjectByType<RespawnPoint>(FindObjectsInactive.Include).gameObject;

                    NavMeshAgent agent = player.GetComponent<NavMeshAgent>();

                    // Desabilita temporariamente para evitar conflito
                    agent.enabled = false;
                    player.transform.position = respawnPoint.transform.position;
                    agent.enabled = true;

                    // Define o destino final após reposicionar
                    agent.SetDestination(respawnPoint.transform.position);
                    PlayerMovement.isMovementBlocked = false;

                    encounterSystem.prefab.GetComponent<QuestLacaio>().isDialogueFinished = false;
                    yield return new WaitForSeconds(7.5f); // Tempo de espera para a animação de derrota GLOW UP
                }

                encounterSystem.prefab.GetComponent<QuestLacaio>().isDialogueStarted = false;
                encounterSystem.battleActive = false;
                encounterSystem.prefab.GetComponent<EncounterDefinition>().isBattleStarted = false;

                StartCoroutine(WaitThenChangeState<EndBattleState>());
            }
            else
            {
                StartCoroutine(WaitThenChangeState<RecoveryState>());
            }
        }

        private void AccumulatedExperienceForThePlayer(float exp)
        {
            accumulatedExperience += exp;
        }
    }
}
