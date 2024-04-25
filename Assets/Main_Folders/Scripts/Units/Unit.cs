using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public delegate void OnUnit(Unit unit);
public class Unit : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    List<Stat> _stats;
    public OnUnit onUnitClicked = delegate{};
    public OnUnit onUnitTakeTurn = delegate{};
    public TagModifier[] Modify = new TagModifier[(int)ModifierTags.None];
    public virtual IEnumerator Recover(){
        yield return null;
        SetStatValue(StatType.Block, 0);
        onUnitTakeTurn(this);
    }
    [ContextMenu("Generate Stats")]
    void GenerateStats(){
        _stats = new List<Stat>();
        for(int i=0; i<(int)StatType.None; i++){
            Stat stat = new Stat();
            stat.Type = (StatType)i;
            stat.Value = Random.Range(0, 100);
            _stats.Add(stat);
        }
    }
    public void OnPointerClick(PointerEventData eventData){
        onUnitClicked(this);
    }
    public int GetStatValue(StatType type){
        int statValue = _stats[(int)type].Value;
        //modify
        return statValue;
    }
    public void SetStatValue(StatType type, int value){
        //modify
        _stats[(int)type].Value = value;
    }
}
