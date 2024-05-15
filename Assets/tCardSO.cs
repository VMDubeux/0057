using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "teste de batalha", fileName = "carta")]
public class tCardSO : ScriptableObject
{
    public string cardName = "nome";
    public int damage = 0;
    public int shield = 0;
    public bool damageAll = false;
}
