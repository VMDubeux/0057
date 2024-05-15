using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    [SerializeField] private GameObject[] allMember;
    [SerializeField] private List<PartyMember> currentParty;

    [SerializeField] private GameObject defaultPartyMember;

    private Vector3 playerPosition;

    private static GameObject instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this.gameObject;
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
