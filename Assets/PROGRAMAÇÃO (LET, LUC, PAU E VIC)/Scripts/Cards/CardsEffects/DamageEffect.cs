using System.Collections;
using System.Collections.Generic;
using Main_Folders.Scripts.StateMachine.States;
using Main_Folders.Scripts.Visuals;
using UnityEngine;

namespace Main_Folders.Scripts.Cards.CardsEffects
{
    public class DamageEffect : CardEffect
    {
        public int Amount;
        public override IEnumerator Apply(List<object> targets)
        {
            foreach (Object o in targets)
            {
                BattleVisuals unit = o as BattleVisuals;

                ModifiedValues modifiedValues = new ModifiedValues(Amount);
                ApplyModifier(modifiedValues, ModifierTags.DoAttackDamage, global::StateMachine.Instance.CurrentUnit);
                ApplyModifier(modifiedValues, ModifierTags.TakeAttackDamage, unit);

                int block = unit.GetStatValue(3);
                int leftoverBlock = Mathf.Max(0, block - modifiedValues.FinalValue);
                unit.SetStatValue(3, leftoverBlock);

                int currentHP = unit.GetStatValue(1);
                int leftoverDamage = Mathf.Max(0, modifiedValues.FinalValue - block);
                unit.SetStatValue(1, Mathf.Max(0, currentHP - leftoverDamage));

                Debug.LogFormat("Unit {0} HP went from {1} to {2}; block went from {3} to {4} ",
                    unit.name, currentHP, unit.GetStatValue(1), block, leftoverBlock);
                yield return null;
                if (unit.GetStatValue(1) <= 0)
                {
                    unit.Modify[(int)ModifierTags.WhenUnitDies](null);

                    if (targets.Count == 1) // Com isso, o jogo retorna para o level anterior quando o player � morto em batalha de maneira autom�tica.
                    {
                        global::StateMachine.Instance.ChangeState<TurnBeginState>();
                    }
                }
            }
            GameObject.Find("PlayerBattleVisual").GetComponent<PoseAnimation>().AttackPose();

        }

        void ApplyModifier(ModifiedValues modifiedValues, ModifierTags tag, BattleVisuals unit)
        {
            TagModifier modifier = unit.Modify[(int)tag];
            if (modifier != null)
            {
                modifier(modifiedValues);
            }
        }
    }
}
