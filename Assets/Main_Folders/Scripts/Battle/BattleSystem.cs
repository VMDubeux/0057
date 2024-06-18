using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Main_Folders.Scripts.Managers;
using Main_Folders.Scripts.Visuals;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BattleSystem : MonoBehaviour
{
    /*[SerializeField] private enum BattleState { Start, Selection, Battle, Won, Lost, Run }

    //[Header("Battle State")]
    //[SerializeField] private BattleState state;
    */

    public static BattleSystem Instance;

    [Header("Spawn Points")] [SerializeField]
    private Transform[] partySpawnPoints;

    [SerializeField] private Transform[] enemySpawnPoints;

    [Header("Battlers")] public List<BattleEntities> allBattlers = new List<BattleEntities>();
    public List<BattleEntities> enemyBattlers = new List<BattleEntities>();
    public List<BattleEntities> playerBattlers = new List<BattleEntities>();

    /*
    [Header("UI")]
    [SerializeField] private GameObject[] enemySelectionButtons;
    [SerializeField] private GameObject battleMenu;
    [SerializeField] private GameObject enemySelectionMenu;
    [SerializeField] private TextMeshProUGUI actionText;
    [SerializeField] private GameObject bottomTextPopUp;
    [SerializeField] private TextMeshProUGUI bottomText;
    */

    private PartyManager partyManager;
    private EnemyManager enemyManager;

    [Header("GameOver Moment:")] public GameObject camera;
    public GameObject canvasDeath;
    public GameObject canvasBattle;
    //private int currentPlayer;

    /*
    private const string ACTION_MESSAGE = "'s Action:";
    private const string WIN_MESSAGE = "Your party won the battle";
    private const string LOSE_MESSAGE = "Your party has been defeated";
    private const string SUCCESSFULLY_RAN_MESSAGE = "Your party ran away";
    private const string UNSUCCESSFULLY_RAN_MESSAGE = "Your party failed to run";
    private const int TURN_DURATION = 2;
    private const int RUN_CHANCE = 50;
    private const string OVERWORLD_SCENE = "LEVEL_1";
    */

    private void Start()
    {
        partyManager = GameObject.FindFirstObjectByType<PartyManager>();
        enemyManager = GameObject.FindFirstObjectByType<EnemyManager>();
        //camera = GameObject.FindFirstObjectByType<CinemachineVirtualCamera>().gameObject;
        //canvasDeath = GameObject.FindFirstObjectByType<CanvasDeath>(FindObjectsInactive.Include).gameObject;
        //canvasBattle = GameObject.FindFirstObjectByType<CanvasBattle>(FindObjectsInactive.Include).gameObject;

        CreatePartyEntities();
        CreateEnemyEntities();
        //ShowBattleMenu();
        //DetermineBattleOrder();
    }

    /*
    private IEnumerator BattleRoutine()
    {
        enemySelectionMenu.SetActive(false);
        state = BattleState.Battle;
        bottomTextPopUp.SetActive(true);

        for (int i = 0; i < allBattlers.Count; i++)
        {
            if (state == BattleState.Battle && allBattlers[i].CurrHealth > 0)
            {
                switch (allBattlers[i].BattleAction)
                {
                    case BattleEntities.Action.Attack:
                        yield return StartCoroutine(AttackRoutine(i));
                        break;
                    case BattleEntities.Action.Run:
                        yield return StartCoroutine(RunRoutine());
                        break;
                    default:
                        Debug.Log("Error - incorrect battle action");
                        break;
                }
            }
        }

        RemoveDeadBattlers();

        if (state == BattleState.Battle)
        {
            bottomTextPopUp.SetActive(false);
            currentPlayer = 0;
            ShowBattleMenu();
        }

        yield return null;
    }

    private IEnumerator AttackRoutine(int i)
    {
        if (allBattlers[i].IsPlayer == true)
        {
            BattleEntities currAttacker = allBattlers[i];
            if (allBattlers[currAttacker.Target].CurrHealth <= 0)
            {
                currAttacker.SetTarget(GetRandomEnemy());
            }
            BattleEntities currTarget = allBattlers[currAttacker.Target];
            AttackAction(currAttacker, currTarget);
            yield return new WaitForSeconds(TURN_DURATION);

            if (currTarget.CurrHealth <= 0)
            {
                bottomText.text = string.Format("{0} defeated {1}", currAttacker.Name, currTarget.Name);
                yield return new WaitForSeconds(TURN_DURATION);
                enemyBattlers.Remove(currTarget);
                allBattlers.Remove(currTarget);

                if (enemyBattlers.Count <= 0)
                {
                    state = BattleState.Won;
                    bottomText.text = WIN_MESSAGE;
                    yield return new WaitForSeconds(TURN_DURATION);
                    SceneManager.LoadScene(OVERWORLD_SCENE);
                }
            }
        }

        if (i < allBattlers.Count && allBattlers[i].IsPlayer == false)
        {
            BattleEntities currAttacker = allBattlers[i];
            currAttacker.SetTarget(GetRandomPartyMember());
            BattleEntities currTarget = allBattlers[currAttacker.Target];

            AttackAction(currAttacker, currTarget);
            yield return new WaitForSeconds(TURN_DURATION);

            if (currTarget.CurrHealth <= 0)
            {
                bottomText.text = string.Format("{0} defeated {1}", currAttacker.Name, currTarget.Name);
                yield return new WaitForSeconds(TURN_DURATION);
                playerBattlers.Remove(currTarget);

                if (playerBattlers.Count <= 0)
                {
                    state = BattleState.Lost;
                    bottomText.text = LOSE_MESSAGE;
                    yield return new WaitForSeconds(TURN_DURATION);
                    Debug.Log("Game over");
                }
            }
        }
    }



    private IEnumerator RunRoutine()
    {
        if (state == BattleState.Battle)
        {
            if (Random.Range(1, 101) >= RUN_CHANCE)
            {
                bottomText.text = SUCCESSFULLY_RAN_MESSAGE;
                state = BattleState.Run;
                allBattlers.Clear();
                yield return new WaitForSeconds(TURN_DURATION);
                SceneManager.LoadScene(OVERWORLD_SCENE);
                yield break;
            }
            else
            {
                bottomText.text = UNSUCCESSFULLY_RAN_MESSAGE;
                yield return new WaitForSeconds(TURN_DURATION);
            }
        }
    }

    */


    private void CreatePartyEntities()
    {
        List<PartyMember> currentParty = new List<PartyMember>();
        currentParty = partyManager.GetCurrentParty();
        int serialNumber = 0;

        for (int i = 0; i < currentParty.Count; i++)
        {
            BattleEntities tempEntity = new BattleEntities();

            tempEntity.SetEntityValues(currentParty[i].HP,
                currentParty[i].MaxHP,
                currentParty[i].Block,
                currentParty[i].Strength,
                currentParty[i].Level,
                true,
                serialNumber);

            serialNumber++;

            tempEntity.SetEntityName(currentParty[i].MemberName + serialNumber);

            BattleVisuals tempBattleVisuals = Instantiate(currentParty[i].MemberBattleVisualPrefab,
                    partySpawnPoints[i].position, Quaternion.identity, GameObject.Find("Units/Player").transform)
                .GetComponent<BattleVisuals>();

            tempBattleVisuals.name = currentParty[i].MemberName;

            tempBattleVisuals.SetStartingValues(currentParty[i].MaxHP,
                currentParty[i].MaxHP, currentParty[i].Block, currentParty[i].Strength, currentParty[i].Level,
                serialNumber, tempEntity);

            tempEntity.BattleVisuals = tempBattleVisuals;

            allBattlers.Add(tempEntity);
            playerBattlers.Add(tempEntity);
        }
    }

    private void CreateEnemyEntities()
    {
        List<Enemy> currentEnemies = new List<Enemy>();
        currentEnemies = enemyManager.GetCurrentEnemies();
        int serialNumber = 0;

        for (int i = 0; i < currentEnemies.Count; i++)
        {
            BattleEntities tempEntity = new BattleEntities();

            tempEntity.SetEntityValues(currentEnemies[i].HP,
                currentEnemies[i].MaxHP,
                currentEnemies[i].Block,
                currentEnemies[i].Strength,
                currentEnemies[i].Level,
                false,
                serialNumber);

            serialNumber++;

            tempEntity.SetEntityName(currentEnemies[i].EnemyName + serialNumber);

            BattleVisuals tempBattleVisuals = Instantiate(currentEnemies[i].EnemyVisualPrefab,
                    enemySpawnPoints[i].position, Quaternion.identity, GameObject.Find("Units/Enemies").transform)
                .GetComponent<BattleVisuals>();

            tempBattleVisuals.name = currentEnemies[i].EnemyName;

            tempBattleVisuals.SetStartingValues(currentEnemies[i].MaxHP,
                currentEnemies[i].MaxHP, currentEnemies[i].Block, currentEnemies[i].Strength, currentEnemies[i].Level,
                serialNumber, tempEntity);

            tempEntity.BattleVisuals = tempBattleVisuals;

            allBattlers.Add(tempEntity);
            enemyBattlers.Add(tempEntity);
        }
    }

    /*    public void ShowBattleMenu()
        {
            actionText.text = playerBattlers[currentPlayer].Name + ACTION_MESSAGE;
            battleMenu.SetActive(true);
        }


        public void ShowEnemySelectionMenu()
        {
            battleMenu.SetActive(false);
            SetEnemySelectionButtons();
            enemySelectionMenu.SetActive(true);
        }


    private void SetEnemySelectionButtons()
    {
        for (int i = 0; i < enemySelectionButtons.Length; i++)
        {
            enemySelectionButtons[i].SetActive(false);
        }

        for (int j = 0; j < enemyBattlers.Count; j++)
        {
            enemySelectionButtons[j].SetActive(true);
            enemySelectionButtons[j].GetComponentInChildren<TextMeshProUGUI>().text = enemyBattlers[j].Name;
        }
    }
      */

    /*
    public void SelectEnemy(int currentEnemy)
    {
        BattleEntities currentPlayerEntity = playerBattlers[0];
        currentPlayerEntity.SetTarget(allBattlers.IndexOf(enemyBattlers[currentEnemy]));

        currentPlayerEntity.BattleAction = BattleEntities.Action.Attack;
        currentPlayer++;

        if (currentPlayer >= playerBattlers.Count)
        {
            StartCoroutine(BattleRoutine());
        }
        else
        {
            enemySelectionMenu.SetActive(false);
            ShowBattleMenu();
        }
}
*/

    /*private void AttackAction(BattleEntities currAttacker, BattleEntities currTarget)
    {
        int damage = currAttacker.Strength;
        currAttacker.BattleVisuals.PlayAttackAnimation();
        currTarget.HP -= damage;
        currTarget.BattleVisuals.PlayHitAnimation();
        currTarget.UpdateUI();
        //bottomText.text = string.Format("{0} attacks {1} for {2} damage", currAttacker.Name, currTarget.Name, damage);
        SaveHealth();
    }*/

    /*private int GetRandomPartyMember()
    {
        List<int> partyMembers = new();

        for (int i = 0; i < allBattlers.Count; i++)
        {
            if (allBattlers[i].IsPlayer == true)
            {
                partyMembers.Add(i);
            }
        }

        return partyMembers[Random.Range(0, partyMembers.Count)];
    }

    private int GetRandomEnemy()
    {
        List<int> enemies = new();

        for (int i = 0; i < allBattlers.Count; i++)
        {
            if (allBattlers[i].IsPlayer == false)
            {
                enemies.Add(i);
            }
        }

        return enemies[Random.Range(0, enemies.Count)];
    }

    private void SaveHealth()
    {
        for (int i = 0; i < playerBattlers.Count; i++)
        {
            partyManager.SaveHealth(i, playerBattlers[i].HP);
        }
    }

    public void RemoveDeadBattlers()
    {
        for (int i = 0; i < allBattlers.Count; i++)
        {
            if (allBattlers[i].HP <= 0)
            {
                allBattlers.RemoveAt(i);
            }
        }
    }*/

    /*
    private void DetermineBattleOrder()
    {
        allBattlers.Sort((bi1, bi2) => -bi1.Initiative.CompareTo(bi2.Initiative));
    }

    public void SelectRunAction()
    {
        state = BattleState.Selection;

        BattleEntities currentPlayerEntity = playerBattlers[currentPlayer];

        currentPlayerEntity.BattleAction = BattleEntities.Action.Run;

        battleMenu.SetActive(false);
        currentPlayer++;

        if (currentPlayer >= playerBattlers.Count)
        {
            StartCoroutine(BattleRoutine());
        }
        else
        {
            enemySelectionMenu.SetActive(false);
            ShowBattleMenu();
        }
    }

     */

    private void Update()
    {
        if (playerBattlers[0].HP <= 0)
        {
            StartCoroutine(AnimationCam());
        }
    }

    private IEnumerator AnimationCam()
    {
        playerBattlers[0].BattleVisuals.gameObject.GetComponent<SelectableUnit>().enabled = false; // Na morte, desativa o script em que o mouse destaca o personagem
        canvasBattle.SetActive(false); // Na morte, desativa o canvas da batalha (deck, etc.)
        playerBattlers[0].BattleVisuals.gameObject.transform.GetChild(1).gameObject.SetActive(false); // Na morte, desativa o canvas da barra de vida.
        camera.GetComponent<Animator>().enabled = true;
        camera.GetComponent<Animator>().Play("DeathCam");
        yield return new WaitForSeconds(5);
        canvasDeath.SetActive(true);
    }
}


[System.Serializable]
public class BattleEntities
{
    //public enum Action { Attack, Run }
    //public Action BattleAction;

    public string Name; // Index 0 (BattleVisual SetStatValue)
    public int HP; // Index 1 (BattleVisual SetStatValue)
    public int MaxHP; // Index 2 (BattleVisual SetStatValue)
    public int Block; // Index 3 (BattleVisual SetStatValue)
    public int Strength; // Index 4 (BattleVisual SetStatValue)
    public int Level; // Index 5 (BattleVisual SetStatValue)
    public BattleVisuals BattleVisuals; // Index 6 (BattleVisual SetStatValue)
    public bool IsPlayer; // Index 7 (BattleVisual SetStatValue)
    public int Target; // Index 8 (BattleVisual SetStatValue)
    public int SerialNumber; // Index 9 (BattleVisual SetStatValue)

    public void SetEntityValues(int hp, int maxHP,
        int block, int strength, int level, bool isPlayer, int serialNumber)
    {
        HP = hp;
        MaxHP = maxHP;
        Block = block;
        Strength = strength;
        Level = level;
        IsPlayer = isPlayer;
        SerialNumber = serialNumber;
    }

    public void SetEntityName(string name)
    {
        Name = name;
    }

    public void SetTarget(int target)
    {
        Target = target;
    }

    /*public void UpdateUI()
    {
        BattleVisuals.ChangeHealth(HP);
    }*/
}