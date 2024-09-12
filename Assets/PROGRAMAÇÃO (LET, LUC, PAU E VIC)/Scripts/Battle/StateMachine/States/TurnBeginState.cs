using System.Collections;
using Main_Folders.Scripts.Managers;
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
                    Debug.LogFormat("Unit {0} tried to play, but is dead", machine.CurrentUnit);
                    AccumulatedExperienceForThePlayer(machine.CurrentUnit.gameObject.GetComponent<Unit>()
                        .expToGive); // Envia o valor de experiencia para o m�todo de ac�mulo, durante a batalha.
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
                GameObject player = GameObject.Find("Player");
                if (_playerUnit.HP > 0) //inimigo derrotado
                {
                    partyManager = GameObject.Find("PartyManager").GetComponent<PartyManager>();
                    partyManager.SetExperience(0,
                        accumulatedExperience); // Envio do quantitativo acumulado de experi�ncia para o player, mediante uso do m�todo constante no script Party Manager.
                    encounterSystem = FindAnyObjectByType<EncounterSystem>(FindObjectsInactive.Include)
                        .GetComponent<EncounterSystem>();
                    encounterSystem.prefab.GetComponent<Unit>().hasFought = true;
                    encounterSystem.battleActive = false;
                    //dropar carta
                    References.Instance.CurrentEnemyBattle.GetComponent<ItemDrop>().CardDrop();
                }
                else
                {
                    //player retorna ao respawnPoint
                    Transform respawnPoint = FindFirstObjectByType<RespawnPoint>(FindObjectsInactive.Include).transform;
                    player.transform.position = new Vector3(respawnPoint.position.x - 1, player.transform.position.y,
                        respawnPoint.position.z);
                    player.GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
                    yield return new WaitForSeconds(7.5f);
                }

                FindAnyObjectByType<EncounterSystem>(FindObjectsInactive.Include)
                        .GetComponent<EncounterSystem>().battleActive = false;
                EnemyMovementStates.OnStartCombat -= EncounterDefinition.Verification;

                StartCoroutine(WaitThenChangeState<EndBattleState>());
            }
            else
            {
                StartCoroutine(WaitThenChangeState<RecoveryState>());
            }
        }

        private void
            AccumulatedExperienceForThePlayer(
                float exp) // Acumula a experiencia durante a batalha para, em caso de vit�ria, ser transferida ao jogador.
        {
            accumulatedExperience += exp;
        }
    }
}