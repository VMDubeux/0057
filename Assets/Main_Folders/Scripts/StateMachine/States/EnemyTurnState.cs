using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class EnemyTurnState : State
{
    [SerializeField] private int Amount;

    [SerializeField] PlayerUnit _playerUnit;

    [SerializeField] BattleVisuals currentUnit;

    private void Update()
    {
        if (_playerUnit == null)
        {
            _playerUnit = machine.CurrentUnit as PlayerUnit;
        }

        if (currentUnit == null && !machine.CurrentUnit.CompareTag("Player"))
        {
            currentUnit = machine.CurrentUnit;
            Amount = currentUnit.Strength;
        }
    }

    public override IEnumerator Enter()
    {
        ModifiedValues modifiedValues = new ModifiedValues(Amount);
        ApplyModifier(modifiedValues, ModifierTags.DoAttackDamage, StateMachine.Instance.CurrentUnit);
        ApplyModifier(modifiedValues, ModifierTags.TakeAttackDamage, _playerUnit);

        int block = _playerUnit.GetStatValue(3);
        int leftoverBlock = Mathf.Max(0, block - modifiedValues.FinalValue);
        _playerUnit.SetStatValue(3, leftoverBlock);

        int currentHP = _playerUnit.GetStatValue(1);
        int leftoverDamage = Mathf.Max(0, modifiedValues.FinalValue - block);
        _playerUnit.SetStatValue(1, Mathf.Max(0, currentHP - leftoverDamage));

        if (_playerUnit.GetStatValue(1) <= 0)
        {
            _playerUnit.Modify[(int)ModifierTags.WhenUnitDies](null);
        }

        /*Debug.LogFormat("Enemy Unit {0} Attacked: Unit {1} HP went from {2} to {3}; block went from {4} to {5} ",
                currentUnit.name, _playerUnit.name, currentHP, _playerUnit.GetStatValue(1), block, leftoverBlock);*/
        yield return null;
        StartCoroutine(WaitThenChangeState<TurnBeginState>());
    }

    void ApplyModifier(ModifiedValues modifiedValues, ModifierTags tag, BattleVisuals unit)
    {
        TagModifier modifier = unit.Modify[(int)tag];
        if (modifier != null)
        {
            modifier(modifiedValues);
        }
    }
    /*void ApplyModifier2(ModifiedValues modifiedValues, ModifierTags tag, PlayerUnit unit)
    {
        TagModifier modifier = unit.Modify[(int)tag];
        if (modifier != null)
        {
            modifier(modifiedValues);
        }
    }*/
}
