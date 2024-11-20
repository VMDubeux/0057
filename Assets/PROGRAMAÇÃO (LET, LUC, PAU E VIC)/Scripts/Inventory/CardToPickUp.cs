using Assets.PROGRAMAÇÃO__LET__LUC__PAU_E_VIC_.Scripts.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CardToPickUp;

public class CardToPickUp : MonoBehaviour
{
    public Card CardConvertClass;

    public enum CardType
    {
        BelezaSurreal, Calado, Polimento,
        LindoNaoBelo, Flex, JogadaDeCabelo,
        Musculos, NaNaNiNaNao, NadaDelicado,
        NemPense, OlhaBem, PesoDoNome,
        SacaIsso, SemEstilo, VemCa, None
    }

    public enum CardRarity { ComumCard, MedCard, EspCard, None }

    [SerializeField] internal CardType cardType;
    [SerializeField] internal CardRarity cardRarity;
    [SerializeField] internal string Name;
    [SerializeField] internal Sprite Sprite;
    [SerializeField] internal int InventoryPos;
    [SerializeField] internal int Identity;

    private void Start()
    {
        InventoryPos = GetInventoryPos(cardType);
        Name = GetName(cardType);
        Sprite = GetSprite(cardType);
        if (PlayerPrefs.HasKey(InventoryPos + Name + Identity))
        {
            Destroy(this.gameObject);
        }
    }

    void PickUp()
    {
        CardInventoryManager.Instance.CardPickedUp(gameObject.GetComponent<CardToPickUp>());
        DontDestroyOnLoad(gameObject);
        PlayerPrefs.SetString(InventoryPos + Name + Identity, "Picked");
        gameObject.SetActive(false);
    }

    public void DestroyIt()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        PickUp();
    }

    public static int GetInventoryPos(CardType cardType)
    {
        switch (cardType)
        {
            case CardType.None:
            default:
            case CardType.BelezaSurreal: return 0;
            case CardType.Calado: return 1;
            case CardType.Polimento: return 2;
            case CardType.LindoNaoBelo: return 3;
            case CardType.Flex: return 4;
            case CardType.JogadaDeCabelo: return 5;
            case CardType.Musculos: return 6;
            case CardType.NaNaNiNaNao: return 7;
            case CardType.NadaDelicado: return 8;
            case CardType.NemPense: return 9;
            case CardType.OlhaBem: return 10;
            case CardType.PesoDoNome: return 11;
            case CardType.SacaIsso: return 12;
            case CardType.SemEstilo: return 13;
            case CardType.VemCa: return 14;
        }
    }

    public static string GetName(CardType cardType)
    {
        switch (cardType)
        {
            case CardType.None:
            default:
            case CardType.BelezaSurreal: return "Surreal beauty";
            case CardType.Calado: return "Sush!";
            case CardType.Polimento: return "Polishing";
            case CardType.LindoNaoBelo: return "Beautiful? Uber handsome!";
            case CardType.Flex: return "Flex";
            case CardType.JogadaDeCabelo: return "Hair Flip";
            case CardType.Musculos: return "Muscles";
            case CardType.NaNaNiNaNao: return "That's a nope";
            case CardType.NadaDelicado: return "Kinda rough";
            case CardType.NemPense: return "Not a chance";
            case CardType.OlhaBem: return "Look at THIS";
            case CardType.PesoDoNome: return "The power of a name";
            case CardType.SacaIsso: return "Check this out";
            case CardType.SemEstilo: return "Negative aura";
            case CardType.VemCa: return "Come here";
        }
    }

    public static Sprite GetSprite(CardType cardType)
    {
        switch (cardType)
        {
            case CardType.None:
            default:
            case CardType.BelezaSurreal: return GameAssets.i.BelezaSurreal;
            case CardType.Calado: return GameAssets.i.Calado;
            case CardType.Polimento: return GameAssets.i.Polimento;
            case CardType.LindoNaoBelo: return GameAssets.i.LindoNaoBelo;
            case CardType.Flex: return GameAssets.i.Flex;
            case CardType.JogadaDeCabelo: return GameAssets.i.JogadaDeCabelo;
            case CardType.Musculos: return GameAssets.i.Musculos;
            case CardType.NaNaNiNaNao: return GameAssets.i.NaNaNiNaNao;
            case CardType.NadaDelicado: return GameAssets.i.NadaDelicado;
            case CardType.NemPense: return GameAssets.i.NemPense;
            case CardType.OlhaBem: return GameAssets.i.OlhaBem;
            case CardType.PesoDoNome: return GameAssets.i.PesoDoNome;
            case CardType.SacaIsso: return GameAssets.i.SacaIsso;
            case CardType.SemEstilo: return GameAssets.i.SemEstilo;
            case CardType.VemCa: return GameAssets.i.VemCa;
        }
    }

    internal void ExternalSetupCard(CardType cardType) // Avaliar necessidade
    {
        InventoryPos = GetInventoryPos(cardType);
        Name = GetName(cardType);
        Sprite = GetSprite(cardType);
        if (PlayerPrefs.HasKey(InventoryPos + Name + Identity))
        {
            Destroy(this.gameObject);
        }
    }
}