using System.Collections.Generic;
using System.ComponentModel;
using Main_Folders.Scripts.Player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Main_Folders.Scripts.Managers
{
    public class PartyManager : MonoBehaviour
    {
        [SerializeField] [Tooltip("Todos os prefabs de batalha dos personagens jogáveis")]
        public GameObject[] allMember;

        [SerializeField] private List<PartyMember> currentParty;

        [SerializeField] private Vector3 playerPosition;

        [SerializeField] internal GameObject[] dripChosen = new GameObject[2];

        [Header("Player Definitions: ")] public int playerMaxLevel;
        public int playerMaxExp;

        private int playerLevel;

        private TextMeshProUGUI playerLevelText;

        private float playerExp;

        private Slider sliderExp;

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
                    newPartyMember.MemberBattleVisualPrefab = allMember[i].gameObject;
                    newPartyMember.MemberOverworldVisualPrefab =
                        allMember[i].GetComponent<Unit>().OverworldVisualPrefab;

                    currentParty.Add(newPartyMember);
                }
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
            if (SceneManager.GetActiveScene().name != "LEVEL_BATTLE" &&
                SceneManager.GetActiveScene().name != "LEVEL_INTRO_NEW")
            {
                sliderExp = FindFirstObjectByType<CanvasHUD>(FindObjectsInactive.Include).transform
                    .GetComponentInChildren<Slider>();
                sliderExp.maxValue = playerMaxExp;
                sliderExp.value = playerExp;

                if (playerExp >= playerMaxExp)
                {
                    if (playerLevel < playerMaxLevel)
                        playerLevel++;

                    playerExp = 0;
                    sliderExp.value = playerExp;
                }

                playerLevelText = FindFirstObjectByType<CanvasHUD>(FindObjectsInactive.Include).transform
                    .GetComponentInChildren<TextMeshProUGUI>();
                playerLevelText.text = "LVL " + playerLevel.ToString();
            }
        }

        public void Drips(GameObject dChosen, GameObject dReplaced)
        {
            var player = GameObject.Find("Player");

            if (dripChosen[0] == null)
            {
                player.transform.Find("Dripless_ReB").gameObject.SetActive(true);
                currentParty[0].Drips[0] = player.transform.Find("Dripless_ReB").gameObject;
                player.transform.Find("Brute_ReB").gameObject.SetActive(false);
                currentParty[0].Drips[1] = player.transform.Find("Brute_ReB").gameObject;
                return;
            }

            currentParty[0].Drips[0] = dChosen;
            player.transform.Find(dripChosen[0].name).gameObject.SetActive(true);

            currentParty[0].Drips[1] = dReplaced;
            player.transform.Find(dripChosen[1].name).gameObject.SetActive(false);
        }

        public void ChosenDrip(GameObject chosen, GameObject replaced)
        {
            dripChosen[0] = chosen;
            dripChosen[1] = replaced;

            Drips(chosen, replaced);
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

        public float CurrExp;

        //public int MaxExp;
        public GameObject[] Drips = new GameObject[2];
        public GameObject MemberBattleVisualPrefab; //what will be displayed in battle scene
        public GameObject MemberOverworldVisualPrefab; //what will be displayed in overworld scene
    }
}