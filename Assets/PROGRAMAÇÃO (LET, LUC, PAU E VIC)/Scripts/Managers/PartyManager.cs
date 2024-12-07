using System.Collections.Generic;
using System.ComponentModel;
using Main_Folders.Scripts.Player;
using Main_Folders.Scripts.Units;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Main_Folders.Scripts.Managers
{
    public class PartyManager : MonoBehaviour
    {
        public static PartyManager Instance;

        [SerializeField]
        [Tooltip("Todos os prefabs de batalha dos personagens jogáveis")]
        public GameObject[] allMember;

        [SerializeField] private List<PartyMember> currentParty;

        [SerializeField] private Vector3 playerPosition;

        [SerializeField] internal GameObject[] dripChosen = new GameObject[2];

        [Header("Player Definitions: ")]
        internal int playerMaxLevel = 5;
        internal int playerMaxExp = 30;

        private int playerLevel;

        private TextMeshProUGUI playerLevelText;

        [SerializeField] internal float playerExp;

        [SerializeField] private Slider sliderExp;

        [SerializeField] private GameJuices gameJuices;

        [SerializeField] private SkillPoints skillPoints;

        /*[Header("Player Character Prefab:")] [Tooltip("Drag the player's prefab")] [SerializeField]
        private GameObject player;

        [Tooltip("Inform the player's starting position")] [SerializeField]
        private Vector3 startPosition;

        [Tooltip("Inform the initial rotation of the player")] [SerializeField]
        private Vector3 startRotation;
        */

        private void Awake()
        {
            AddMemberToPartyByName(allMember[0].name);
            gameJuices = gameObject.GetComponent<GameJuices>();
            skillPoints = FindFirstObjectByType<SkillPoints>(FindObjectsInactive.Include).GetComponent<SkillPoints>();
        }

        public void AddMemberToPartyByName(string memberName)
        {
            for (int i = 0; i < allMember.Length; i++)
            {
                if (allMember[i].name == memberName)
                {
                    PartyMember newPartyMember = new PartyMember();
                    newPartyMember.MemberName = allMember[i].name;
                    //newPartyMember.Level = allMember[i].StartingLevel;
                    newPartyMember.HP = allMember[i].GetComponent<Unit>()._stats[1].Value;
                    newPartyMember.MaxHP = newPartyMember.HP;
                    newPartyMember.Block = allMember[i].GetComponent<Unit>()._stats[2].Value;
                    newPartyMember.Strength = allMember[i].GetComponent<Unit>()._stats[3].Value;
                    newPartyMember.Hand = allMember[i].GetComponent<PlayerUnit>().DrawAmount;
                    newPartyMember.Energy = allMember[i].GetComponent<PlayerUnit>().MaxEnergy;
                    newPartyMember.MemberBattleVisualPrefab = allMember[i].gameObject;
                    newPartyMember.MemberOverworldVisualPrefab =
                        allMember[i].GetComponent<Unit>().OverworldVisualPrefab;

                    currentParty.Add(newPartyMember);
                }
            }
        }

        public void SetStatsValues(int stat, int value)
        {
            switch (stat)
            {
                case 0:
                    currentParty[0].MaxHP += value;
                    currentParty[0].HP = currentParty[0].MaxHP;
                    break;
                case 1:
                    currentParty[0].Block += value;
                    break;
                case 2:
                    currentParty[0].Strength += value;
                    break;
                case 3:
                    if (currentParty[0].Hand < 7)
                        currentParty[0].Hand += value;
                    break;
                case 4:
                    if (currentParty[0].Energy < 6)
                        currentParty[0].Energy += value;
                    break;
            }
        }

        public List<PartyMember> GetCurrentParty()
        {
            List<PartyMember> aliveParty = new List<PartyMember>();
            aliveParty = currentParty;
            for (int i = 0; i < aliveParty.Count; i++)
            {
                if (aliveParty[i].HP <= 0)
                {
                    aliveParty.RemoveAt(i);
                }
            }

            return aliveParty;
        }

        public void SaveHealth(int partyMember, int health)
        {
            currentParty[partyMember].HP = health;
        }

        public void SetPosition(Vector3 position)
        {
            playerPosition = position;
        }

        public Vector3 GetPosition()
        {
            return playerPosition;
        }

        public int GetLevel()
        {
            return playerLevel;
        }

        public void SetExperience(int partyMember, float exp)
        {
            playerExp += exp;
            currentParty[partyMember].CurrExp = playerExp;
        }

        public float GetExperience()
        {
            return playerLevel;
        }

        public void ChangeExpSliderValue()
        {
            var hud = FindFirstObjectByType<CanvasHUD>(FindObjectsInactive.Include).transform;

            if (SceneManager.GetActiveScene().name != "LEVEL_BATTLE" &&
                SceneManager.GetActiveScene().name != "LEVEL_INTRO_NEW")
            {
                sliderExp = hud.GetComponentInChildren<Slider>();
                sliderExp.maxValue = playerMaxExp;
                Debug.Log($"Slider Max Value: {sliderExp.maxValue}");
                sliderExp.value = playerExp;

                if (playerExp >= playerMaxExp)
                {
                    if (playerLevel < playerMaxLevel)
                        playerLevel++;

                    playerExp = 0;
                    sliderExp.value = playerExp;
                    gameJuices.AddRandomItemToInventory();
                    skillPoints.LevelUp();
                }

                playerLevelText = hud.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
                playerLevelText.text = "LVL " + playerLevel.ToString();
            }
        }

        public void ChosenDrip(GameObject chosen, GameObject replaced, GameObject visual)
        {
            dripChosen[0] = chosen;
            dripChosen[1] = replaced;
            allMember[0] = visual;
            currentParty.Clear();
            AddMemberToPartyByName(visual.name);
        }

        private void OnGUI()
        {
            ChangeExpSliderValue();
        }
    }

    [System.Serializable]
    public class PartyMember
    {
        public string MemberName;
        public int Level;
        public int HP;
        public int MaxHP;
        public int Block;
        public int Strength;
        public int Hand;
        public int Energy;
        public float CurrExp;

        //public int MaxExp;
        public GameObject MemberBattleVisualPrefab; //what will be displayed in battle scene
        public GameObject MemberOverworldVisualPrefab; //what will be displayed in overworld scene
    }
}