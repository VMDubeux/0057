using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public delegate void OnUnit(Unit unit);
public class Unit : MonoBehaviour, IPointerClickHandler
{
    public List<Stat> stats;
    public bool isEnemy = false;
    public const float UpdateRate = 0.5f;
    public OnUnit onUnitClicked = delegate{};
    public OnUnit onUnitTakeTurn = delegate{};
    public TagModifier[] Modify = new TagModifier[(int)ModifierTags.None];
    public virtual IEnumerator Recover(){
        yield return null;
        SetStatValue(StatType.Block, 0);
        onUnitTakeTurn(this);
    }
    public virtual IEnumerator EnemyDamage()
    {
        yield return null;
        onUnitTakeTurn(this);
    }
    [ContextMenu("Generate Stats")]
    void GenerateStats(){
        stats = new List<Stat>();
        for(int i=0; i<(int)StatType.None; i++){
            Stat stat = new Stat();
            stat.Type = (StatType)i;
            stat.Value = Random.Range(0, 100);
            stats.Add(stat);
        }
    }
    public void OnPointerClick(PointerEventData eventData){
        onUnitClicked(this);
    }
    public int GetStatValue(StatType type){
        int statValue = stats[(int)type].Value;
        //modify
        return statValue;
    }
    public void SetStatValue(StatType type, int value){
        //modify
        stats[(int)type].Value = value;
    }
}
