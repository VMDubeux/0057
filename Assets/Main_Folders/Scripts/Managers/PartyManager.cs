using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class PartyManager : MonoBehaviour
{
    public static PartyManager Instance;

    [SerializeField] private GameObject[] allMember;
    [SerializeField] private List<PartyMember> currentParty;

    [SerializeField] private GameObject defaultPartyMember;

    private Vector3 playerPosition;

    [Header("Player Definitions: ")]
    public int playerMaxLevel;
    public int playerMaxExp;

    private int playerLevel;

    private TextMeshProUGUI playerLevelText;

    private float playerExp;

    private Slider sliderExp;

    private GameObject partyManager;

    private void Awake()
    {
        if (partyManager != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            partyManager = this.gameObject;
            AddMemberToPartyByName(defaultPartyMember.name);
        }

        DontDestroyOnLoad(this.gameObject);
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
                newPartyMember.MemberOverworldVisualPrefab = allMember[i].GetComponent<Unit>().OverworldVisualPrefab;

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

    public void SetExperience(float exp)
    {
        playerExp += exp;
    }

    public float GetExperience()
    {
        return playerLevel;
    }

    public void ChangeExpSliderValue()
    {
        if (SceneManager.GetActiveScene().name != "LEVEL_BATTLE" && SceneManager.GetActiveScene().name != "LEVEL_INTRO")
        {
            sliderExp = GameObject.Find("Canvas (HUD)/Image/Slider").GetComponent<Slider>();
            sliderExp.maxValue = playerMaxExp;
            sliderExp.value = playerExp;

            if (playerExp >= playerMaxExp)
            {
                if (playerLevel < playerMaxLevel)
                    playerLevel++;

                playerExp = 0;
                sliderExp.value = playerExp;
            }

            playerLevelText = GameObject.Find("Canvas (HUD)/Image/Text (TMP) LVL").GetComponent<TextMeshProUGUI>();
            playerLevelText.text = "LVL " + playerLevel.ToString();
        }
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
    public int CurrExp;
    public int MaxExp;
    public GameObject MemberBattleVisualPrefab; //what will be displayed in battle scene
    public GameObject MemberOverworldVisualPrefab; //what will be displayed in overworld scene
}
