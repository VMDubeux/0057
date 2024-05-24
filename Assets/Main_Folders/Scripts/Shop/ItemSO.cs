using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class ItemSO
{
    public ItemType itemType;

    public static int GetId(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.CartaComum: return 1;
            case ItemType.PerfumePeq: return 2;
            case ItemType.PerfumeMed: return 3;
            case ItemType.PerfumeGrd: return 4;
        }
    }

    public static string GetName(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.CartaComum: return "Carta Comum";
            case ItemType.PerfumePeq: return "Perfume Pequeno";
            case ItemType.PerfumeMed: return "Perfume Médio";
            case ItemType.PerfumeGrd: return "Perfume Grande";
        }
    }

    public static int GetCost(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.CartaComum: return 20;
            case ItemType.PerfumePeq: return 2;
            case ItemType.PerfumeMed: return 5;
            case ItemType.PerfumeGrd: return 10;
        }
    }

    public static Sprite GetSprite(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.CartaComum: return GameAssets.i.CartaComum;
            case ItemType.PerfumePeq: return GameAssets.i.PerfumePeq;
            case ItemType.PerfumeMed: return GameAssets.i.PerfumeMed;
            case ItemType.PerfumeGrd: return GameAssets.i.PerfumeGrd;
        }
    }
}

public enum ItemType
{
    CartaComum,
    PerfumePeq,
    PerfumeMed,
    PerfumeGrd,
    None
}
