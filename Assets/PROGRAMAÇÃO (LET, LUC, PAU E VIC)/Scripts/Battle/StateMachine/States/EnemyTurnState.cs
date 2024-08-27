using System.Collections;
using System.Collections.Generic;
using Main_Folders.Scripts.StateMachine.States;
using Main_Folders.Scripts.Units;
using Main_Folders.Scripts.Visuals;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
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
    }

    public override IEnumerator Enter()
    {
        if (!machine.CurrentUnit.CompareTag("Player"))
        {
            currentUnit = machine.CurrentUnit;
            EnemySkillChoice();
        }
        //EnemySkillChoice();

        yield return null;
        StartCoroutine(WaitThenChangeState<TurnBeginState>());
    }

    public void EnemySkillChoice()
    {
        currentUnit.GetComponentInChildren<Animator>().SetTrigger("pose");
        Debug.Log("Enemy Pose!");
        int r = Random.Range(0, 9);
        if (currentUnit.HP > currentUnit.MaxHP / 2)
        {
            if (r < 7)
            {
                Attack();
            }
            else
                Shield();
        }
        else
        {
            if (r < 3)
            {
                Attack();
            }
            else
                Shield();
        }
    }

    private void Shield()
    {
        ModifiedValues modifiedValues = new ModifiedValues(Amount);
        ApplyModifier(modifiedValues, ModifierTags.GainBlock, StateMachine.Instance.CurrentUnit);
        ApplyModifier(modifiedValues, ModifierTags.TakeAttackDamage, _playerUnit);


        currentUnit.SetStatValue(3, currentUnit.GetBlockAmount()); // 5 como valor padrão para teste.
    }

    private void Attack()
    {
        Amount = currentUnit.Strength;
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